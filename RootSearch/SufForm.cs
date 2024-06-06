using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RootSearch
{
    public partial class SufForm : UniForm
    {
        const string FILE_PATH_SUFS = "suffixes.txt";
        List<string> AFFIX_SET = new List<string>();
        FormTimer f = new FormTimer();
        public SufForm()
        {
            InitializeComponent();
        }

        public SufForm(List<string> affixSet)
        {
            InitializeComponent();
            AFFIX_SET = affixSet;
        }

        private void SufForm_Load(object sender, EventArgs e)
        {
            List<List<string>> preparedSet = CreatePreparedAffixSetTurbo(AFFIX_SET);

            this.SuspendLayout();
            CreateTreeTurbo(preparedSet);
            EditTree(treeView1);
            this.ResumeLayout();

            inputCombobox.Font = new Font("Microsoft Sans Serif", 11);
            inputCombobox.DropDownHeight = 300;
            FillCombobox(Properties.Resources.suffixes);
            inputCombobox.SelectedIndex = 0;
            expandAllButton.Focus();

            labelHelper.Text = "Нажмите 1 раз ЛКМ по узлу, чтобы открыть следующие аффиксы или скрыть их."
            + Environment.NewLine + "Нажмите дважды ЛКМ по узлу, чтобы раскрыть всё поддерево (все аффиксальные цепочки)."
            + Environment.NewLine + "Выберите из выпадающего списка первый аффикс, чтобы осуществить автоматическую прокрутку и раскрытие поддерева. "
            + Environment.NewLine + "Осторожно!!! Раскрытие всех узлов дерева (всех аффиксальных цепочек) может занимать до 5 минут!!!";

        }


        private void FillCombobox(string fileName)
        {
            StreamReader sr = new StreamReader(Properties.Resources.suffixes);
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



            //Streamer.Print(tmpIEnumerable, new StreamWriter("все суффиксальные окружения в алфавитном порядке.txt"));

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
                    //affixEnviroment.GetRange(i+1, affixEnviroment.Count);
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

        private void expandAllButton_Click(object sender, EventArgs e)
        {

            Thread th = new Thread(Countdown);
            th.Start();
            treeView1.ExpandAll();
            int a = 0;
            /*
            //5:50:09
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            treeView1.ExpandAll();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            MessageBox.Show(elapsedTime);
            */
        }

        private void Countdown()
        {
            f = new FormTimer();
            f.Show();
            TimeSpan ts = new TimeSpan(0, 5, 50);
            while (ts > TimeSpan.Zero)
            {
                f.labelTimer.Text = ts.ToString();
                f.Update();
                Task.Delay(1000).Wait();
                ts -= TimeSpan.FromSeconds(1);
            }
            //Thread.Yield();
            f.Close();
            f.Dispose();
        }
    }
}
