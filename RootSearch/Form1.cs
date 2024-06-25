using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RootSearch
{
    public partial class Form1 : Form
    {
        const string FILE_PATH = "Words.txt";
        const string FILE_PATH_PREF = "prefix.txt";
        const string FILE_PATH_SUF = "suffix.txt";
        const string FILE_PATH_ALL_AFFIX_ENVIROMENTS = "allAffixEnviroment.txt";
        string HELP_STR = "Поместите ВСЕ txt-файлы в папку Resources с программой. Не редактируйте и не удаляйте их!!!" + Environment.NewLine + "Последовательно ВЫБИРАЙТЕ из выпадающих списков без пропусков. Набор символов существует исключительно для поиска. Сначала нажать на стрелочку выпадающего списка, потом вводить. Затем нажмите кнопку Ввод." 
            + Environment.NewLine + "Ввод целого окружения независим. Выбирайте из выпадающего списка. Значок корня можно скопировать из любой выбранной строки. Затем нажмите кнопку Ввод целого окружения."
            + Environment.NewLine + "В окне Найти пересечение множеств выбирайте файлы с помощью ЛКМ+Shift или ЛКМ+Ctrl.";


        string[] fileNamesForSets = null;

        string labelText = "Текущая папка для сохранения" + Environment.NewLine;
        string folderName = AppDomain.CurrentDomain.BaseDirectory;


        SufForm sufForm = new SufForm();
        PrefForm prefForm = new PrefForm();

        Parser parser;



        public Form1()
        {
            InitializeComponent();
            InitializeComboboxesSuf(ref comboBoxesSuf, 9, 150, ref labelsSuf);
            InitializeComboboxesPref(ref comboBoxesPref, 4, 50, ref labelsPref);

            labelFilePath.Text = labelText + folderName;

            //TODO: test
            //ParserAffix pars = new ParserAffix();
            // StatisticRoot.CreateMainFiles();
            //AllAffixEnvironments.Main();
            //ExtractRoot.MainExtractRoot();
            int a = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const string FILE_PATH_PREFS = "prefixes.txt";
            const string FILE_PATH_SUFS = "suffixes.txt";

            PrepareComboboxForEnviroments();
            PrepareComboboxForRoots();
            FillComboBoxes(Properties.Resources.prefixes_str, Properties.Resources.suffixes_str);
            FillComboBoxForEnviroment(Properties.Resources.allAffixEnviroment_str);
            FillComboBoxForRoot(Properties.Resources.root_str);

            SetSelectedIndex();
            BlockComboBoxes();
            SetEvents();
            this.OpenFilesButton.Visible = true;
            comboboxForEnviroments.SelectedIndex = 0;
            comboboxForRoot.SelectedIndex = 0;
            //helpProvider1.SetHelpString(labelHelper, HELP_STR);
            //helpProvider1.SetShowHelp(labelHelper, true);
            //toolTip1.SetToolTip(labelHelper, HELP_STR);
        }

        //Дописать + проверки аналогичные предыдущим комбобоксам
        private void PrepareComboboxForEnviroments()
        {
            this.comboboxForEnviroments.Visible = true;
            comboboxForEnviroments.Font = new Font("Microsoft Sans Serif", 11);
            comboboxForEnviroments.DropDownHeight = 300;
        }

        private void PrepareComboboxForRoots()
        {
            this.comboboxForRoot.Visible = true;
            comboboxForRoot.Font = new Font("Microsoft Sans Serif", 11);
            comboboxForRoot.DropDownHeight = 300;
        }

        private void InitializeComboboxesSuf(ref List<System.Windows.Forms.ComboBox> comboBoxes, int size, int position, ref List<Label> labels)
        {
            comboBoxes = new List<System.Windows.Forms.ComboBox>();
            labels = new List<Label>();

            for (int i = 0; i < size; i++)
            {
                comboBoxes.Add(new System.Windows.Forms.ComboBox());
                labels.Add(new Label());
            }

            int j = 0;

            foreach (var combo in comboBoxes)
            {
                combo.Location = new System.Drawing.Point(15 + j * 85, position);
                combo.Size = new System.Drawing.Size(80, 200);
                combo.Font = new Font("Microsoft Sans Serif", 11);
                combo.DropDownHeight = 300;
                // comboBoxes[j].TabIndex = j;
                Controls.Add(comboBoxes[j]);

                labels[j].Location = new System.Drawing.Point(45 + j * 85, position + 30);
                labels[j].Size = new System.Drawing.Size(80, 200);
                labels[j].Font = new Font("Microsoft Sans Serif", 11);
                labels[j].Text = (j + 1).ToString();
                Controls.Add(labels[j]);

                j++;
            }
        }

        private void InitializeComboboxesPref(ref List<System.Windows.Forms.ComboBox> comboBoxes, int size, int position, ref List<Label> labels)
        {
            comboBoxes = new List<System.Windows.Forms.ComboBox>();
            labels = new List<Label>();

            for (int i = 0; i < size; i++)
            {
                comboBoxes.Add(new System.Windows.Forms.ComboBox());
                labels.Add(new Label());
            }

            int j = 0;

            //0..3 or 4 = max count of prefix
            foreach (var combo in comboBoxes)
            {
                combo.Location = new System.Drawing.Point(15 + (3 - j) * 85, position);
                combo.Size = new System.Drawing.Size(80, 200);
                combo.Font = new Font("Microsoft Sans Serif", 11);
                combo.DropDownHeight = 300;
                // comboBoxes[j].TabIndex = j;
                Controls.Add(comboBoxes[j]);

                labels[j].Location = new System.Drawing.Point(45 + (3 - j) * 85, position + 30);
                labels[j].Size = new System.Drawing.Size(80, 200);
                labels[j].Font = new Font("Microsoft Sans Serif", 11);
                labels[j].Text = (j + 1).ToString();
                Controls.Add(labels[j]);

                j++;
            }
        }


        private void FillComboBoxForEnviroment(string fileEnv)
        {
            StreamReader sr = File.OpenText(fileEnv);
            String input;
            List<string> enviroments = new List<string>();

            while ((input = sr.ReadLine()) != null && input != "" && input != "\n" && input != "\r" && input != "\r\n")
            {
                enviroments.Add(input.Trim(' '));
            }
            enviroments.Sort();

            //Streamer.Print(enviroments, new StreamWriter("все окружения в алфавитном порядке.txt"));

            comboboxForEnviroments.Items.AddRange(enviroments.ToArray());
        }

        private void FillComboBoxForRoot(string fileRoot)
        {
            StreamReader sr = File.OpenText(fileRoot);
            String input;
            List<string> roots = new List<string>();

            while ((input = sr.ReadLine()) != null && input != "" && input != "\n" && input != "\r" && input != "\r\n")
            {
                roots.Add(input);
            }

            comboboxForRoot.Items.AddRange(roots.ToArray());
        }

        private void FillComboBoxes(string filePref, string fileSuf)
        {
            StreamReader sr;
            String input;

            List<string> prefixList = new List<string>();

            sr = File.OpenText(filePref);
            foreach (var combo in comboBoxesPref)
            {
                while ((input = sr.ReadLine()) != null && input != "" && input != "\n" && input != "\r" && input != "\r\n")
                {
                    prefixList.Add(input);
                }
                combo.Items.AddRange(prefixList.ToArray());
                prefixList = new List<string>();
            }

            sr = File.OpenText(fileSuf);
            foreach (var combo in comboBoxesSuf)
            {
                while ((input = sr.ReadLine()) != null && input != "" && input != "\n" && input != "\r" && input != "\r\n")
                {
                    prefixList.Add(input);
                }
                combo.Items.AddRange(prefixList.ToArray());
                prefixList = new List<string>();
            }
        }

        private void BlockComboBoxes()
        {
            foreach (var combo in comboBoxesPref)
                combo.Enabled = false;

            foreach (var combo in comboBoxesSuf)
                combo.Enabled = false;

            comboBoxesPref[0].Enabled = true;
            comboBoxesSuf[0].Enabled = true;
        }

        private void SetSelectedIndex()
        {
            foreach (var combo in comboBoxesPref)
                combo.SelectedIndex = 0;

            foreach (var combo in comboBoxesSuf)
                combo.SelectedIndex = 0;
        }

        private void SetEvents()
        {
            foreach (var combo in comboBoxesPref)
            {
                combo.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
                combo.Click += new System.EventHandler(this.comboBox_Click);
                combo.DropDown += new System.EventHandler(this.comboBox_DropDown);
                combo.Enter += new System.EventHandler(this.comboBox_Enter);
            }

            foreach (var combo in comboBoxesSuf)
            {
                combo.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
                combo.Click += new System.EventHandler(this.comboBox_Click);
                combo.DropDown += new System.EventHandler(this.comboBox_DropDown);
                combo.Enter += new System.EventHandler(this.comboBox_Enter);
            }
        }

        private void ColorComboboboxesWhite()
        {
            foreach (var combo in comboBoxesPref)
                combo.BackColor = Color.White;

            foreach (var combo in comboBoxesSuf)
                combo.BackColor = Color.White;
        }

        /*Разворачиваем пришедшие преффиксы, потому что они считываются слева направо, 
          а должны рассматриваться по примыканию корню (т.е. наоборот, справа налево).*/
        private void buttonInput_Click(object sender, EventArgs e)
        {
            ColorComboboboxesWhite();
            bool IsPrefValid = IsComboboxesValid(comboBoxesPref);
            bool IsSufValid = IsComboboxesValid(comboBoxesSuf);

            if (IsPrefValid && IsSufValid)
            {
                List<string> prefixes = ReadComboBoxes(comboBoxesPref);
                if (prefixes != null)
                    prefixes.Reverse();

                List<string> suffixes = ReadComboBoxes(comboBoxesSuf);

                //!!!
                parser = new Parser("Words.txt", folderName);
                //parser = new Parser(FILE_PATH, folderName);

                string[] filePathes = parser.CreateMainFiles(prefixes, suffixes, checkBox1.Checked);

                textBoxOutput.Text = "";
                foreach (string s in filePathes)
                    textBoxOutput.Text += s + Environment.NewLine;
            }
            else
            {
                MessageBox.Show("Некорректный ввод данных!");
            }
        }


        //Пользователь обязан выбрать из выпадающих списков, а не вводить.
        private bool IsComboboxesFilled(List<System.Windows.Forms.ComboBox> comboBoxes)
        {
            bool isValid = true;
            foreach (var combo in comboBoxes)
            {
                if (combo.SelectedIndex == -1)
                {
                    combo.BackColor = Color.Red;
                    isValid = false;
                }
            }

            return isValid;
        }

        //Запрет на пропуск пустых списков.
        private bool IsComboboxesValid(List<System.Windows.Forms.ComboBox> comboBoxes)
        {
            bool isValid = IsComboboxesFilled(comboBoxes);

            //В списках сзади должны быть выбраны <пусто>, т.е. пропуск пустыя ячеек сзади
            int i = comboBoxes.Count - 1;
            while (i >= 0 && comboBoxes[i].SelectedIndex == 0)
            {
                i--;
            }

            for (int j = i; j >= 0; j--)
            {
                if (comboBoxes[j].SelectedIndex == 0)
                {
                    comboBoxes[j].BackColor = Color.Red;
                    isValid = false;
                }
            }

            return isValid;
        }

        private List<string> ReadComboBoxes(List<System.Windows.Forms.ComboBox> comboBoxes)
        {
            List<string> affixes = new List<string>();

            foreach (var combo in comboBoxes)
            {
                if (combo.SelectedIndex != 0)
                    affixes.Add(combo.Text);
            }

            if (affixes.Count == 0)
                return null;

            return affixes;
        }

        private void comboBox_DropDown(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox combo = (System.Windows.Forms.ComboBox)sender;
            combo.BackColor = Color.White;
        }

        private void comboBox_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox combo = (System.Windows.Forms.ComboBox)sender;
            combo.BackColor = Color.White;
        }

        private void comboBox_Enter(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox combo = (System.Windows.Forms.ComboBox)sender;
            combo.SelectedIndex = -1;
        }

        //Разблокировка следующего комбобокса: либо приставка, либо суффикс
        //ПЕРЕПИСАТЬ
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox combo = (System.Windows.Forms.ComboBox)sender;

            int index = comboBoxesPref.IndexOf(combo);
            if (index != -1 && index != comboBoxesPref.Count - 1)
            {
                if (comboBoxesPref[index].SelectedIndex != 0 && comboBoxesPref[index].SelectedIndex != -1)
                {
                    comboBoxesPref[index + 1].Enabled = true;
                }
                return;
            }

            index = comboBoxesSuf.IndexOf(combo);
            if (index != -1 && index != comboBoxesSuf.Count - 1)
            {
                if (comboBoxesSuf[index].SelectedIndex != 0 && comboBoxesSuf[index].SelectedIndex != -1)
                {
                    comboBoxesSuf[index + 1].Enabled = true;
                }
                return;
            }
        }

        private void buttonChooseFilePath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
           
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                labelFilePath.Text = labelText + folderName;
            }
        }

        private void OpenFilesButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = folderName;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileNamesForSets = openFileDialog1.FileNames;
                textBoxOutput.Text = "";
                textBoxOutput.Text = Set.FindMainSetIntersection(fileNamesForSets, folderName);
            }
        }

        private void sufFormButton_Click(object sender, EventArgs e)
        {

            List<string> set = Streamer.CreateListFromFile(FILE_PATH_ALL_AFFIX_ENVIROMENTS);
            sufForm = new SufForm(set);
            sufForm.Show();
        }

        private void prefFormButton_Click(object sender, EventArgs e)
        {
            List<string> set = Streamer.CreateListFromFile(FILE_PATH_ALL_AFFIX_ENVIROMENTS);
            prefForm = new PrefForm(set);
            prefForm.Show();
        }

        private void prefFromFocus()
        {
            prefForm.Focus();
        }

        private void sufFormFocus()
        {
            sufForm.Focus();
        }

        private void buttonInputEnviroment_Click(object sender, EventArgs e)
        {            
            if (comboboxForEnviroments.SelectedIndex == -1)
                MessageBox.Show("Некорректный ввод данных!");
            else
            {
                Pair inputPair = Pair.FromStringResized(comboboxForEnviroments.Text);

                parser = new Parser(FILE_PATH, folderName);

                string[] filePathes = parser.CreateMainFiles(inputPair.prefixes, inputPair.suffixies, checkBox2.Checked);

                textBoxOutput.Text = "";
                foreach (string s in filePathes)
                    textBoxOutput.Text += s + Environment.NewLine;
            }            
        }

        private void comboboxForEnviroments_Enter(object sender, EventArgs e)
        {
            comboboxForEnviroments.SelectedIndex = -1;
        }

        private void labelHelper_Click(object sender, EventArgs e)
        {
            MessageBox.Show(HELP_STR);
        }

        private void buttonInputRoot_Click(object sender, EventArgs e)
        {
            if (comboboxForRoot.SelectedIndex == -1)
                MessageBox.Show("Некорректный ввод данных!");
            else
            {
                parser = new Parser(FILE_PATH, folderName);

                string[] filePathes = parser.CreateFilesForRoot(comboboxForRoot.Text);

                textBoxOutput.Text = "";
                foreach (string s in filePathes)
                    textBoxOutput.Text += s + Environment.NewLine;
            }
        }
    }
}