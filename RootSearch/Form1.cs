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

namespace RootSearch
{
    public partial class Form1 : Form
    {
        private Validator validator;
        const string filePath = "Words.txt";
        public Form1()
        {
            InitializeComponent();
            validator = new Validator();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            errorProviderPrefix.SetError(textBoxPref0, "");
            errorProviderSuffix.SetError(textBoxSuf0, "");

            Parser parser = new Parser(filePath);
            string[] pref = { "пер", "от"};
            string[] suf = { "И", "тел", "ьн" };


           // string[] pref = null;
           // string[] suf = { "ьн" };

            parser.Test(pref, suf);
        }

        //написать достатие из TextBoxов
        private void ReadTextBoxes()
        {
            
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

        private void ValidatePrefixes()
        {
            string textBox = "textBoxPref";
            //4 или 9 объектов, которые должны быть заполнены подряд (без пробелов)
            //
        }

        private void ValidateSuffixes()
        {

        }
    }
}
