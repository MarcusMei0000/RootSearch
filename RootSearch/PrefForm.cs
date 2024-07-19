using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Reflection.Assembly;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Drawing;

namespace RootSearch
{
    public partial class PrefForm : UniForm
    {
        const string FILE_PATH_GROWING = "дерево развёртывания";
        const string FILE_PATH_PREFS = "prefixes.txt";
        string folderName = AppDomain.CurrentDomain.BaseDirectory;

        string[] AFFIX_SET_COLLECTION;
        List<string> AFFIX_SET = new List<string>();
        List<string> AFFIX_SET_ORDER = new List<string>();
        FormTimer f = new FormTimer();

        Parser parser;

        string BUTTON_TEXT = "Создать выборку" + Environment.NewLine + "по выбранному первому аффиксу (поддереву)";

        public PrefForm()
        {
            InitializeComponent();
            parser = new Parser(Properties.Resources.Words_str, folderName);
        }

        public PrefForm(List<string> affixSet)
        {
            InitializeComponent();
            AFFIX_SET = affixSet;
            buttonCreateSubtree.Text = BUTTON_TEXT;
            parser = new Parser(Properties.Resources.Words_str, folderName);
        }

        private void PrefForm_Load(object sender, EventArgs e)
        {
            List<List<string>> preparedSet = CreatePreparedAffixSetTurbo(AFFIX_SET);

            this.SuspendLayout();
            CreateTreeTurbo(preparedSet);
            EditTree(treeView1);
            this.ResumeLayout();

            //PrintTree(treeView1);
            PrepareComboboxes();
            expandAllButton.Focus();

            labelHelper.Text = "Нажмите 1 раз ЛКМ по узлу, чтобы открыть следующие аффиксы или скрыть их."
                + Environment.NewLine + "Нажмите дважды ЛКМ по узлу, чтобы раскрыть всё поддерево (все аффиксальные цепочки)."
                + Environment.NewLine + "Выберите из выпадающего списка первый аффикс, чтобы осуществить автоматическую прокрутку и раскрытие поддерева. "
                + Environment.NewLine + "После выбора примыкающего к корню аффикса нажмите на кнопку, чтобы осуществить вывод поддерева с примерами. "
                + Environment.NewLine + "Осторожно!!! Раскрытие всех узлов дерева (всех аффиксальных цепочек) может занимать до 5 минут!!!"
                + Environment.NewLine + "Осторожно!!! Формирование поддерева с примерами может занимать до 5 минут!!!";
            //PrintTree(treeView1);
        }

        private void PrepareComboboxes()
        {
            inputCombobox.Font = new Font("Microsoft Sans Serif", 11);
            inputCombobox.DropDownHeight = 300;
            FillCombobox(Properties.Resources.prefixes_str);
            //FillCombobox(FILE_PATH_PREFS);
            inputCombobox.SelectedIndex = 0;
        }
        private void FillCombobox(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            String input;
            List<string> prefixList = new List<string>();

            while ((input = sr.ReadLine()) != null && input != "" && input != "\n" && input != "\r" && input != "\r\n")
            {
                prefixList.Add(input);
            }

            AFFIX_SET_COLLECTION = new string[prefixList.Count];
            prefixList.CopyTo(AFFIX_SET_COLLECTION);
            inputCombobox.Items.AddRange(prefixList.ToArray());
            prefixList.Sort();
            AFFIX_SET_ORDER = prefixList;
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
                //tmp.Add(END_SYMBOL);
                tmp.Reverse();
                list.Add(tmp);
            }

            //Streamer.Print(tmpIEnumerable, new StreamWriter("все приставочные окружения в алфавитном порядке.txt"));

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
            //Thread th = new Thread(Countdown);
            //th.Start();
            treeView1.ExpandAll();
            //int a = 0;
        }

        /*
                private void Func()
                {
                    myTimer.Interval = 1000;//это твой интервал 1000 милисек = 1секунд
                    myTimer.Elapsed += new ElapsedEventHandler(Start_Game);//тут подписываемся на событие Start_Game(и каждую секунду будет отрабатывать наше событие в нем уже и твори то угодно)
                    myTimer.Start();//запускаем таймер
                }

            private void Start_Game(object sender, ElapsedEventArgs e)
            {
                this.BeginInvoke(new Action(() =>
                {
                    int count = 42;
                    if (count != 0)
                    {
                        f.labelTimer.Text = count--.ToString();
                        f.Update();
                    }
                    else
                    {
                        myTimer.Stop();

                    }

                }));

            }*/

        /*
          Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            MessageBox.Show(elapsedTime);
         */


        private void Countdown()
        {
            f = new FormTimer();
            f.Show();
            TimeSpan ts = new TimeSpan(0, 0, 12);
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

        private void inputCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text == inputCombobox.Text)
                {
                    node.ExpandAll();
                    treeView1.SelectedNode = node;
                    break;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                inputCombobox.Items.Clear();
                inputCombobox.Items.AddRange(AFFIX_SET_ORDER.ToArray());
            }
            else
            {
                inputCombobox.Items.Clear();
                inputCombobox.Items.AddRange(AFFIX_SET_COLLECTION);
            }
        }


        private static void DoSubTreeParse(List<string> affixes)
        {
            
        }

        private void buttonCreateSubtree_Click(object sender, EventArgs e)
        {
            string rootOfSubTree = treeView1.SelectedNode.Text;
            StringBuilder sb = new StringBuilder();
            string outputPath = Streamer.CreateFileName(new List<string> { rootOfSubTree }, new List<string> { FILE_PATH_GROWING }, folderName);
            StreamWriter streamWriter = new StreamWriter(outputPath, false);

            //RecursiveTreeTraversal(rootOfSubTree, sb, streamWriter);            

            var enviroments =
                AFFIX_SET.FindAll(enviroment => enviroment.EndsWith(" " + rootOfSubTree));

            enviroments.Add(rootOfSubTree);
            enviroments.Sort();
            var res = enviroments.OrderBy(str => str.Count(f => f == ' ')).ThenBy(str => str).Distinct();
           

            foreach (string enviroment in res)
            {
                List<string> affixes = enviroment.Split(new char[] {' '} ).ToList();
                parser = new Parser(Properties.Resources.Words_str, folderName);
                List<string> setYesComplimentary = parser.ParseFileWithAffixStrict(affixes, null, true);

                foreach (string s in setYesComplimentary)
                    streamWriter.WriteLine(s);
            }
            streamWriter.Close();

            //Streamer.Print(res, streamWriter);

            textBoxOutput.Text = "";
            textBoxOutput.Text = outputPath;

            int a = 0;
        }
    }
}