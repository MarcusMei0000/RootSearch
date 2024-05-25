using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RootSearch.TreeAffixNodeCreator;

namespace RootSearch
{
    public partial class SufForm : UniForm
    {     
        public SufForm()
        {
            InitializeComponent();
        }

        public SufForm(List<string> affixSet)
        {
            InitializeComponent();
            List<List<string>> preparedSet = CreatePreparedAffixSetTurbo(affixSet);
            List<List<string>> preparedSet2 = preparedSet.GetRange(0, 50);

            this.SuspendLayout();

            CreateTree(preparedSet2);
            EditTree(treeView1);

            this.ResumeLayout();
        }       

        public static List<List<string>> CreatePreparedAffixSetTurbo(List<string> affixSet)
        {
            for (int i = 0; i < affixSet.Count; i++){
                int separator = affixSet[i].IndexOf(SEPARATOR) + 2;
                int end = affixSet[i].Count();

                affixSet[i] = affixSet[i].Substring(separator, end - separator).TrimEnd(' ');
            }
            affixSet.RemoveAll(x => x == "");
            affixSet.Sort();
            IEnumerable<string> tmpIEnumerable = affixSet.Distinct();

            List<List<string>> list = new List<List<string>>();
            List<string> tmp = new List<string>();
            foreach (var affix in tmpIEnumerable)
            {
                tmp = affix.Split(new char[] { ' ' }).ToList();
                tmp.Add(END_SYMBOL);
                list.Add(tmp);
            }

            return list;
        }
        /*
        public static List<List<string>> CreatePreparedAffixSet(List<string> affixSet)
        {
            List<List<string>> list = new List<List<string>>();

            Pair pair;
            foreach (var affix in affixSet)
            {
                pair = Pair.FromString(affix);
                if (!pair.IsNoSuf())
                {
                    list.Add(pair.suffixies);
                }
            }

            foreach (var affixEnviroment in list)
            {
                affixEnviroment.RemoveAll(x => x == "");
                affixEnviroment.Add(END_SYMBOL);
            }

            return list;
        }
        */

        public void CreateTree(List<List<string>> affixSet)
        {            
            foreach (var affixEnviroment in affixSet)
            {
                TreeNode child = IfTreeContains(treeView1, affixEnviroment[0]);
                if (child == null)
                {
                    TreeNode brunch = CreateBrunch(affixEnviroment);
                    treeView1.Nodes.Add(brunch);
                }
                else
                {                   
                    TreeNode tmpNode = child;
                    TreeNode prevNode = tmpNode;
                    int i = 1;
                    while (i < affixEnviroment.Count)
                    {
                        prevNode = tmpNode;
                        tmpNode = IfNodeContains(tmpNode, affixEnviroment[i]);
                        if (tmpNode == null)
                            break;
                        i++;
                    }

                    if (i != affixEnviroment.Count) //если есть ещё суффиксы для вставки
                    {
                        affixEnviroment.RemoveRange(0, i);
                        TreeNode brunch = CreateBrunch(affixEnviroment);
                        prevNode.Nodes.Add(brunch);
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1.ExpandAll();
        }
    }
}
