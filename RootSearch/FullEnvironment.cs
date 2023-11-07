using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace RootSearch
{
    //обязательное условие даже если левая или правая части аффиксального окружения отсутствуют делать пустое присвоение ="";
    public class FullEnvironment
    {
        public string root;
        
        public List<Pair> affixEnvironment;

        public Dictionary<string, int> dictionary;

        public FullEnvironment() 
        {
            affixEnvironment = new List<Pair>();
        }

        public FullEnvironment(string root)
        {
            this.root = root;
            affixEnvironment = new List<Pair>();
            dictionary = new Dictionary<string, int>();
        }

        public void AddEnvironment(Word w)
        {
            affixEnvironment.Add(new Pair(w.prefixes, w.suffixes));
        }

        public void ToStringSet()
        {
            Dictionary<string, int> output = new Dictionary<string, int>();

            foreach(Pair pair in affixEnvironment)
            {
                string tmp = pair.ToString();
                int count = 0;
                if (output.TryGetValue(tmp, out count))
                {
                    output[tmp] = ++count;
                }
                else
                {
                    output.Add(tmp, 1);
                }
            }
            dictionary = output.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public string ToStringWithCount()
        {
            string s = Environment.NewLine + root + "   " + dictionary.Count + "(" + affixEnvironment.Count +")" + Environment.NewLine + "-------------------------" + Environment.NewLine;
            foreach (var record in dictionary)
            {
                s += String.Format("{0,-40} {1, 1}", record.Key, record.Value) + Environment.NewLine;
            }
            s += "-------------------------" + Environment.NewLine;
            return s;
        }


        public override string ToString()
        {
            string s = Environment.NewLine + root + Environment.NewLine + "-------------------------" + Environment.NewLine;
            for (int i = 0; i < affixEnvironment.Count; i++)
            {
                s += affixEnvironment[i].ToString() + Environment.NewLine;

               // s += "                 " + counts[i];
            }
            s += "-------------------------" + Environment.NewLine;
            return s;
        }
    }
}
