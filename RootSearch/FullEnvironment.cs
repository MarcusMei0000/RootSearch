using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    public class Pair
    {
        public List<string> prefixes;
        public List<string> suffixies;

        public Pair()
        {
            prefixes = new List<string>();
            suffixies = new List<string>();
        }

        public Pair(List<string> prefixes, List<string> suffixies)
        {
            this.prefixes = prefixes;
            this.suffixies = suffixies;
        }
    }

    //обязательное условие даже если левая или правая части аффиксального окружения отсутствуют делать пустое присвоение ="";
    public class FullEnvironment
    {
        public string root;
        public List<Pair> affixEnvironment;

        public FullEnvironment() 
        {
            affixEnvironment = new List<Pair>();
        }

        public FullEnvironment(string root)
        {
            this.root = root;
            affixEnvironment = new List<Pair>();
        }

        public void AddEnvironment(Word w)
        {
            affixEnvironment.Add(new Pair(w.prefixes, w.suffixes));
        }
        
        public override string ToString()
        {
            string s = Environment.NewLine + root + Environment.NewLine + "-------------------------" + Environment.NewLine;
            for (int i = 0; i < affixEnvironment.Count; i++)
            {
                if (affixEnvironment[i].prefixes != null)
                {
                    foreach (string pref in affixEnvironment[i].prefixes)
                        s += pref + " ";
                }

                s += "| ";

                if (affixEnvironment[i].suffixies != null)
                {
                    foreach (string suf in affixEnvironment[i].suffixies)
                        s += suf + " ";
                }
                s += Environment.NewLine;
            }
            s += "-------------------------" + Environment.NewLine;
            return s;
        }
    }
}
