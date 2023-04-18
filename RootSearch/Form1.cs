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
        private Validator validator;
        const string filePath = "Words.txt";
        const string filePathPref = "prefix.txt";
        const string filePathSuf = "suffix.txt";

        public Form1()
        {
            InitializeComponent();
            InitializeComboboxes(ref comboBoxesPref, 4, 40);
            InitializeComboboxes(ref comboBoxesSuf, 9, 100);
            validator = new Validator();
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
                combo.Location = new System.Drawing.Point(10 + j * 75, position);
                combo.Size = new System.Drawing.Size(65, 25);
                comboBoxes[j].TabIndex = j;
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
                foreach(var combo in comboBoxesPref)
                    combo.Items.Add(input);
            }

            sr = File.OpenText(fileSuf);
            while ((input = sr.ReadLine()) != null)
            {
                foreach(var combo in comboBoxesSuf)
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
        
        //4 или 9 объектов, которые должны быть заполнены подряд (без пробелов)

        /*дб функция, которая при заполнении постепенно разрешает вводить в комбобоксы 
          и проверяет их все на правильность (т.е. не -1 индекс)
          что из этого более юзерфрэндли?
        */

        private void SetEvents()
        {
            foreach (var combo in comboBoxesPref)
                combo.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);

            foreach (var combo in comboBoxesSuf)
                combo.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Parser parser = new Parser(filePath);

            FillComboBoxes(filePathPref, filePathSuf);
            SetSelectedIndex();
            BlockComboBoxes();
            SetEvents();
        }


        private void buttonInput_Click(object sender, EventArgs e)
        {
            string[] prefixes = ValidateComboBoxes(comboBoxesPref);
            string[] suffixes = ValidateComboBoxes(comboBoxesSuf);

            Parser parser = new Parser(filePath);
            string[] filePathes = parser.MainTask(prefixes, suffixes);

            textBoxOutput.Text = "";
            foreach (string s in filePathes)
                textBoxOutput.Text += s + Environment.NewLine;
        }

        private string[] ValidateComboBoxes(List<System.Windows.Forms.ComboBox> comboBoxes)
        {
            List<string> affixes = new List<string>();

            foreach (var combo in comboBoxes)
            {
                if (combo.SelectedIndex != -1)
                {
                    if (combo.SelectedIndex != 0)
                        affixes.Add(combo.Text);
                    else affixes.Add(null);
                }
            }

            if (affixes[0] == null)
                return null;

            int j = comboBoxes.Count - 1;
            while (j >= 1 && affixes[j] == null)
            {
                affixes.RemoveAt(j);
                j--;
            }
            
            return affixes.ToArray();
        }

        //Если пользователь ввёл и стёр или ввёл лабуду, то Index = -1 и ничего не выбрано
        //поэтому надо заставлять его обратно выбирать <пусто>
        //если пользователь ввёл лабуду, то подсвечивать ему красным и говорить, что он дурак*/

        //какие ещё функции надо добавить?

        //поговорить об оценке множеств с Ворониной

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox combo = (System.Windows.Forms.ComboBox)sender;
            int index = comboBoxesPref.IndexOf(combo);
            if (index != -1 && index != comboBoxesPref.Count - 1)
            {
                if (comboBoxesPref[index].SelectedIndex != 0)
                {
                    comboBoxesPref[index + 1].Enabled = true;
                }
                return;
            }

            index = comboBoxesSuf.IndexOf(combo);
            if (index != -1 && index != comboBoxesSuf.Count - 1)
            {
                if (comboBoxesSuf[index].SelectedIndex != 0)
                {
                    comboBoxesSuf[index + 1].Enabled = true;
                }
                return;
            }
        }
    }
}

//logs
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
