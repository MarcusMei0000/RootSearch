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
    public partial class UniForm : Form
    {
        public const string END_SYMBOL = "#";
        public static char SEPARATOR = '√';

        public static Color[] TREE_NODE_COLORS = {
            Color.Black,
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Magenta,
            Color.Indigo,
            Color.DarkOrchid,
            Color.DarkBlue,
            Color.RoyalBlue,
            Color.DodgerBlue
        };

        public UniForm()
        {
            InitializeComponent();
        }

        public void EditTree(TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                RecursiveTreeTraversal(node);
            }
        }

        private void RecursiveTreeTraversal(TreeNode treeNode)
        {

            foreach (TreeNode node in treeNode.Nodes)
            {
                node.ForeColor = TREE_NODE_COLORS[node.Level];
                RecursiveTreeTraversal(node);
            }
        }

        public void PrintTree(TreeView treeView)
        {
            StringBuilder sb = new StringBuilder();
            StreamWriter streamWriter = new StreamWriter("TREETEST.txt", false);
            for (int i=0; i < 15; i++)
          //  foreach (TreeNode node in treeView.Nodes)
            {
                RecursiveTreeTraversal(treeView.Nodes[i], sb, streamWriter);
            }

        }

        private void RecursiveTreeTraversal(TreeNode treeNode, StringBuilder sb, StreamWriter streamWriter)
        {
            sb.Append(treeNode.ToString());
            foreach (TreeNode node in treeNode.Nodes)
            {
                sb.Append(node.Text);
                sb.Append(" ");
                RecursiveTreeTraversal(node);
                streamWriter.WriteLine(sb.ToString());
            }
        }

        public TreeNode IfNodeContains(TreeNode node, string str)
        {
            for (int i = 0; i <= node.Nodes.Count - 1; i++)
            {
                if (node.Nodes[i].Text == str)
                    return node.Nodes[i];
            }

            return null;
        }

        public TreeNode CreateBrunch(List<string> affixes)
        {
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
    }
}
