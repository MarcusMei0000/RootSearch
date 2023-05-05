using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;

namespace RootSearch
{
    public partial class Form1 : Form
    {
        Validator validator;

        const string FILE_PATH = "Words.txt";
        const string FILE_PATH_PREF = "prefix.txt";
        const string FILE_PATH_SUF = "suffix.txt";

        string[] fileNamesForSets = null;

        string labelText = "Текущая папка для сохранения" + Environment.NewLine;
        string folderName = AppDomain.CurrentDomain.BaseDirectory;

        public Form1()
        {
            InitializeComponent();
            InitializeComboboxes(ref comboBoxesPref, 4, 50);
            InitializeComboboxes(ref comboBoxesSuf, 9, 150);
            validator = new Validator();
            labelFilePath.Text = labelText + folderName;
        }

        private void InitializeComboboxes(ref List<System.Windows.Forms.ComboBox> comboBoxes, int size, int position)
        {
            comboBoxes = new List<System.Windows.Forms.ComboBox>();
            for (int i = 0; i < size; i++)
            {
                comboBoxes.Add(new System.Windows.Forms.ComboBox());
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
                j++;                
            }
        }

        private void FillComboBoxes(string filePref, string fileSuf)
        {
            StreamReader sr = File.OpenText(filePref);
            String input;
            while ((input = sr.ReadLine()) != null)
            {
                foreach (var combo in comboBoxesPref)
                    combo.Items.Add(input);
            }

            sr = File.OpenText(fileSuf);
            while ((input = sr.ReadLine()) != null)
            {
                foreach (var combo in comboBoxesSuf)
                    combo.Items.Add(input);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            FillComboBoxes(FILE_PATH_PREF, FILE_PATH_SUF);
            SetSelectedIndex();
            BlockComboBoxes();
            SetEvents();
        }

        private void ColorComboboboxesWhite()
        {
            foreach (var combo in comboBoxesPref)
                combo.BackColor = Color.White;

            foreach (var combo in comboBoxesSuf)
                combo.BackColor = Color.White;
        }

        //??? подумать где вызывать конструктор парсера
        private void buttonInput_Click(object sender, EventArgs e)
        {
            ColorComboboboxesWhite();
            bool IsPrefValid = IsComboboxesValid(comboBoxesPref);
            bool IsSufValid = IsComboboxesValid(comboBoxesSuf);

            if (IsPrefValid && IsSufValid)
            {
                string[] prefixes = ReadComboBoxes(comboBoxesPref);
                string[] suffixes = ReadComboBoxes(comboBoxesSuf);

                Parser parser = new Parser(FILE_PATH, folderName);
                string[] filePathes = parser.CreateMainFiles(prefixes, suffixes);

                textBoxOutput.Text = "";
                foreach (string s in filePathes)
                    textBoxOutput.Text += s + Environment.NewLine;
            }
            else
            {
                MessageBox.Show("Некорректный ввод данных!");
            }
        }


        //пользователь обязан выбрать, а не вводить
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

        //запрет на пропуск пустых
        private bool IsComboboxesValid(List<System.Windows.Forms.ComboBox> comboBoxes)
        {
            bool isValid = IsComboboxesFilled(comboBoxes);

            //пустые сзади
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

        private string[] ReadComboBoxes(List<System.Windows.Forms.ComboBox> comboBoxes)
        {
            List<string> affixes = new List<string>();

            foreach (var combo in comboBoxes)
            {
                if (combo.SelectedIndex != 0)
                    affixes.Add(combo.Text);
            }

            if (affixes.Count == 0)
                return null;

            return affixes.ToArray();
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

                //где это запускать?
                Set.TestMainSetFunction(fileNamesForSets, folderName);
                textBoxOutput.Text = "";
                textBoxOutput.Text += folderName + "\\res.txt";
            }
        }
    }
}

//logs

/*private bool IsComboboxesValid2(List<System.Windows.Forms.ComboBox> comboBoxes)
{
    bool isValid = true;
    foreach (var combo in comboBoxes)
    {
        if (combo.SelectedIndex == -1)
        {
            combo.BackColor = Color.RosyBrown;
            isValid = false;
        }
    }

    int i = 0;
    //верно заполнены
    while (i < comboBoxes.Count)
    {
        if (comboBoxes[i].SelectedIndex != 0)
            i++;
        else break;
    }


    //должны быть пусты
    while (i < comboBoxes.Count)
    {
        if (comboBoxes[i].SelectedIndex == 0)
            i++;
        else break;
    }

    //ситуация когда 1 или 2 в середине попустили?
    if (i != comboBoxes.Count - 1)
    {
        while (i < comboBoxes.Count)
        {
            if (comboBoxes[i].SelectedIndex != 0)
            {
                comboBoxes[i - 1].BackColor = Color.RosyBrown;
                isValid = false;
            }
            i++;
        }
    }

    return isValid;
}
/*int j = i;
    //должны быть пусты
    while(j < comboBoxes.Count)
    {
        if (comboBoxes[i].SelectedIndex == 0)
            j++;
        else break;
    }

    if (j != comboBoxes.Count - 1)
    {
        while (i < j) {
            if (comboBoxes[i].SelectedIndex == 0)
            {
                comboBoxes[i].BackColor = Color.RosyBrown;
                isValid = false;
            }
            i++;
        }
    }*/
/* public static IEnumerable<Control> GetAllControls(Control root)
        {
            var stack = new Stack<Control>();
            stack.Push(root);

            while (stack.Any())
            {
                var next = stack.Pop();
                foreach (Control child in next.Controls)
                    stack.Push(child);

                yield return next;
            }
        }*/

/*  for (int i = 0; i < 9; i++)
    {
        this.comboBoxesSuf.Add(new System.Windows.Forms.ComboBox());
    }

    j = 0;
    foreach (var combo in comboBoxesSuf)
    {
        combo.Location = new System.Drawing.Point(10 + j * 90, 100);
        combo.Name = "ComboS" + j.ToString();
        combo.Size = new System.Drawing.Size(60, 23);
        comboBoxesSuf[j].TabIndex = j;
        comboBoxesSuf[j].Text = "comboS" + j.ToString();
        Controls.Add(comboBoxesSuf[j]);
        j++;
    }*/

/*private void SetErrorProvidersFalse()
{
    errorProviderPrefix.SetError(textBoxPref0, "");
    errorProviderSuffix.SetError(textBoxSuf0, "");

    errorProviderPrefix.SetError(textBoxPref1, "");
    errorProviderSuffix.SetError(textBoxSuf1, "");

    errorProviderPrefix.SetError(textBoxPref2, "");
    errorProviderSuffix.SetError(textBoxSuf2, "");

    errorProviderPrefix.SetError(textBoxPref3, "");
    errorProviderSuffix.SetError(textBoxSuf3, "");

    errorProviderPrefix.SetError(textBoxSuf4, "");
    errorProviderSuffix.SetError(textBoxSuf5, "");
    errorProviderPrefix.SetError(textBoxSuf6, "");
    errorProviderSuffix.SetError(textBoxSuf7, "");
    errorProviderSuffix.SetError(textBoxSuf8, "");
}*/

/* private string[] ValidateComboBoxes(List<System.Windows.Forms.ComboBox> comboBoxes)
{
    string[] affixes = new string[comboBoxes.Count];

    int i = 0;
    foreach (var combo in comboBoxes)
    {
        if (combo.SelectedIndex != -1)
        {
            if (combo.SelectedIndex != 0)
                affixes[i] = combo.Text;
            else affixes[i] = null;
            i++;
        }
    }

    if (affixes[0] == null)
        return null;

    int j = comboBoxes.Count - 1;
    while (j >= 1 && comboBoxes[j] == null)
        j--;
    List<string> s;

    string[] outp = null;
    Array.Copy(affixes, outp, j);
    return affixes;
}*/
