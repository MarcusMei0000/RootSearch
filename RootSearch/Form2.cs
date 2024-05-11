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
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(HashSet<string> affixSet)
        {
            InitializeComponent();
            List<List<string>> preparedSet = CreatePreparedAffixSet(affixSet);
            CreateTree(preparedSet);

        }

        public static List<List<string>> CreatePreparedAffixSet(HashSet<string> affixSet)
        {
            List<List<string>> list = new List<List<string>>();

            Pair pair;
            foreach (var affix in affixSet)
            {
                pair = Pair.FromString(affix);
                if (!pair.IsNoSuf())
                    list.Add(pair.suffixies);
            }

            return list;
        }
        private TreeNode IfTreeContains(TreeView tree, string str)
        {
            for (int i = 0; i < tree.Nodes.Count - 1; i++)
            {
                if (tree.Nodes[i].Text == str)
                    return tree.Nodes[i];
            }

            return null;
        }

        private TreeNode IfNodeContains(TreeNode node, string str)
        {
            for (int i = 0; i <= node.Nodes.Count - 1; i++)
            {
                if (node.Nodes[i].Text == str)
                    return node.Nodes[i];
            }

            return null;
        }

        public void CreateTree(List<List<string>> affixSet)
        {            
            foreach (var affixEnviroment in affixSet)
            {
                affixEnviroment.RemoveAll(x => x == "");
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
                    else if (tmpNode != null) {
                        tmpNode.Nodes.Add("&");
                    }
                    else
                    {
                        prevNode.Nodes.Add("&");//!!!
                    }
                }
            }
        }

        private TreeNode CreateBrunch(List<string> affixes)
        {
            TreeNode brunchRoot = new TreeNode(affixes[0]);
            TreeNode prevNode = new TreeNode(affixes[affixes.Count - 1]);
            TreeNode curNode;

            for (int i = affixes.Count - 1; i > 0; i--)
            {
                curNode = new TreeNode(affixes[i - 1]);
                curNode.Nodes.Add(prevNode);

                prevNode = curNode;
            }

            return prevNode;
        }



        public void CreateTree2(List<List<string>> affixSet)
        {
            TreeNode root = new TreeNode("Start");
            foreach (var affixEnviroment in affixSet)
            {
                affixEnviroment.RemoveAll(x => x == "");
                if (!treeView1.Nodes.ContainsKey(affixEnviroment[0]))
                {
                    //TreeNode brunchRoot = new TreeNode(affixEnviroment[0]);
                    if (affixEnviroment.Count() > 1)
                    {
                        TreeNode brunchNode = new TreeNode(affixEnviroment[0]);
                        CreateBrunch2(affixEnviroment, brunchNode);
                        treeView1.Nodes.Add(brunchNode);
                    }
                }

            }
        }

        private void CreateBrunch2(List<string> affixes, TreeNode nodeToAddTo)
        {
            TreeNode node;
            List<string> newAffixes;
            foreach (string affix in affixes)
            {
                node = new TreeNode(affix);
                newAffixes = affixes.GetRange(1, affixes.Count - 1);
                if (newAffixes.Count != 0)
                {
                    CreateBrunch2(newAffixes, node);
                }
                nodeToAddTo.Nodes.Add(node);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1.ExpandAll();
        }
    }
}
