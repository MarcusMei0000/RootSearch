using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

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
            validator = new Validator();
        }

        private void SetErrorProvidersFalse()
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
        }

        private void FillComboBoxes(string filePref, string fileSuf)
        {
            var list = new List<string>();
            StreamReader sr = File.OpenText(filePref);
            String input;
            while ((input = sr.ReadLine()) != null)
            {
                comboBoxPref0.Items.Add(input);
                comboBoxPref1.Items.Add(input);
                comboBoxPref2.Items.Add(input);
                comboBoxPref3.Items.Add(input);
            }
            // comboBoxPref0.Text = comboBoxPref0.Items[0].ToString();

            sr = File.OpenText(fileSuf);
            while ((input = sr.ReadLine()) != null)
            {
                comboBoxSuf0.Items.Add(input);
                comboBoxSuf1.Items.Add(input);
                comboBoxSuf2.Items.Add(input);
                comboBoxSuf3.Items.Add(input);
                comboBoxSuf4.Items.Add(input);
                comboBoxSuf5.Items.Add(input);
                comboBoxSuf6.Items.Add(input);
                comboBoxSuf7.Items.Add(input);
                comboBoxSuf8.Items.Add(input);
            }
        }

        private void BlockComboBoxes()
        {
            comboBoxPref1.Enabled = false;
            comboBoxPref2.Enabled = false;
            comboBoxPref3.Enabled = false;

            comboBoxSuf1.Enabled = false;
            comboBoxSuf2.Enabled = false;
            comboBoxSuf3.Enabled = false;
            comboBoxSuf4.Enabled = false;
            comboBoxSuf5.Enabled = false;
            comboBoxSuf6.Enabled = false;
            comboBoxSuf7.Enabled = false;
            comboBoxSuf8.Enabled = false;            
        }

        private void SetSelectedIndex()
        {
            comboBoxPref0.SelectedIndex = 0;
            comboBoxPref1.SelectedIndex = 0;
            comboBoxPref2.SelectedIndex = 0;
            comboBoxPref3.SelectedIndex = 0;

            comboBoxSuf0.SelectedIndex = 0;
            comboBoxSuf1.SelectedIndex = 0;
            comboBoxSuf2.SelectedIndex = 0;
            comboBoxSuf3.SelectedIndex = 0;
            comboBoxSuf4.SelectedIndex = 0;
            comboBoxSuf5.SelectedIndex = 0;
            comboBoxSuf6.SelectedIndex = 0;
            comboBoxSuf7.SelectedIndex = 0;
            comboBoxSuf8.SelectedIndex = 0;
        }

        //написать достатие из TextBoxов
        private void ReadTextBoxes()
        {

        }


        private void ValidatePrefixes()
        {
            string textBox = "textBoxPref";
            //4 или 9 объектов, которые должны быть заполнены подряд (без пробелов)
            //
        }

        /*дб функция, которая при заполнении постепенно разрешает вводить в комбобоксы 
          и проверяет их все на правильность (т.е. не -1 индекс)
          что из этого более юзерфрэндли?
        */
        private void Form1_Load(object sender, EventArgs e)
        {
            SetErrorProvidersFalse();
            Parser parser = new Parser(filePath);

            FillComboBoxes(filePathPref, filePathSuf);
            SetSelectedIndex();
            BlockComboBoxes();            

            //string[] pref = { "пер", "от"};
            //string[] suf = { "И", "тел", "ьн" };

            string[] pref = null;
            string[] suf = { "ьн" };

            List<string> list = new List<string>();

            //parser.Test(pref, suf, out list);
            parser.MainTask(pref, suf);
        }


        private void buttonInput_Click(object sender, EventArgs e)
        {
            string[] prefixes = validator.PrefixValidate(textBoxPref0.Text);
            string[] suffixes = validator.SuffixValidate(textBoxSuf0.Text);

            HashSet<string> words;

            //if (prefixes != null)
            //words = parser.RootsForPrefixSet();

            //if (suffixes != null)
            //words = parser.RootsForPrefixSet();

            //Parser parser = new Parser(filePath);
            //parser.Test();

        }

        private void ValidateSuffixes()
        {

        }

        private void comboBoxPref0_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxPref1.Enabled = true;
        }
    }
}
