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

namespace RootSearch
{
    public partial class PrefForm : UniForm
    {
        const string FILE_PATH_PREFS = "prefixes.txt";
        List<string> AFFIX_SET = new List<string>();
        public PrefForm()
        {
            InitializeComponent();
        }

        public PrefForm(List<string> affixSet)
        {
            InitializeComponent();
            AFFIX_SET = affixSet;

            List<List<string>> preparedSet = CreatePreparedAffixSetTurbo(AFFIX_SET);
            //List<List<string>> preparedSet2 = preparedSet.GetRange(0, 5500);

            this.SuspendLayout();

            //CreateTree(preparedSet2);
            CreateTreeTurbo(preparedSet);
            EditTree(treeView1);
            this.ResumeLayout();

            inputCombobox.Font = new Font("Microsoft Sans Serif", 11);
            inputCombobox.DropDownHeight = 300;
            FillCombobox(FILE_PATH_PREFS);
            inputCombobox.SelectedIndex = 0;
            expandAllButton.Focus();
        }

        private void SufForm_Load(object sender, EventArgs e)
        {

        }

        private void FillCombobox(string fileName)
        {
            StreamReader sr = File.OpenText(fileName);
            String input;
            List<string> prefixList = new List<string>();

            while ((input = sr.ReadLine()) != null && input != "" && input != "\n" && input != "\r" && input != "\r\n")
            {
                prefixList.Add(input);
            }

            inputCombobox.Items.AddRange(prefixList.ToArray());
        }

        public static List<List<string>> CreatePreparedAffixSetTurbo(List<string> affixSet)
        {
            for (int i = 0; i < affixSet.Count; i++)
            {
                int separator = affixSet[i].IndexOf(SEPARATOR) - 1;
                affixSet[i] = affixSet[i].Substring(0, separator).Trim(' ');
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

        public void CreateTreeTurbo(List<List<string>> affixSet)
        {
            TreeNode root = new TreeNode();
            foreach (var affixEnviroment in affixSet)
            {
                TreeNode tmpNode = root;
                TreeNode prevNode = tmpNode;
                int i = 0;
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

            foreach (TreeNode node in root.Nodes)
            {
                treeView1.Nodes.Add(node);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.IsExpanded)
                e.Node.Collapse();
            else
                e.Node.Expand();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            e.Node.ExpandAll();
        }

        private void expandAllButton_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void inputCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text == inputCombobox.Text)
                {
                    node.ExpandAll();
                    break;
                }
            }
        }
    }
}
