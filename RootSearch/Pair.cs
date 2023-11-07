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

        public Pair(Word w)
        {
            this.prefixes = w.prefixes;
            this.suffixies = w.suffixes;
        }
        /*
        public override bool Equals(object obj)
        {
            if (obj != null && obj is Pair)
            {
                Pair other = (Pair)obj;
                return other.prefixes == prefixes && other.suffixies == suffixies;
            }

            return false;
        }*/

        public override string ToString()
        {
            string strPref = "";
            if (prefixes != null)
            {
                foreach (string pref in prefixes)
                    strPref += pref + " ";
            }

            string strSuf = "";

            if (suffixies != null)
            {
                foreach (string suf in suffixies)
                    strSuf += suf + " ";
            }


            return String.Format("{0, 10} | {1, -10}", strPref, strSuf);
        }
    }

}
