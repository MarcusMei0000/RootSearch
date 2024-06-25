using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*Класс описывает 1 аффиксальное окружение (лист приставок и лист соответствующих им суффиксов).
  ToString(), FromString() и конструкторы.*/
namespace RootSearch
{
    public class Pair
    {
        public List<string> prefixes;
        public List<string> suffixies;

        public static char SEPARATOR = '√';

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

        public bool IsNoSuf()
        {
            return suffixies[0] == "";
        }

        public string ToString(bool IsZeroSpace) {
            StringBuilder sb = new StringBuilder();
            if (prefixes != null)
            {
                foreach (string pref in prefixes)
                {
                    sb.Append(pref);
                    sb.Append(" ");
                }
            }
            sb.Append(SEPARATOR);
            sb.Append(" ");
            if (suffixies != null)
            {
                foreach (string suf in suffixies)
                {
                    sb.Append(suf);
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            string strPref = "";
            if (prefixes != null)
            {
                foreach (string pref in prefixes)
                    strPref += pref + " ";
            }
            //else strPref = "           ";

            string strSuf = "";

            if (suffixies != null)
            {
                foreach (string suf in suffixies)
                    strSuf += suf + " ";
            }


            return String.Format("{0, 10} {1} {2, -20}", strPref, SEPARATOR, strSuf);
        }

        //override
        public string ToString2()
        {
            StringBuilder sbPref = new StringBuilder();
            StringBuilder sbSuf = new StringBuilder();
            if (prefixes != null)
            {
                foreach (string pref in prefixes)
                {
                    sbPref.Append(pref);
                    sbPref.Append(" ");
                }
            }

            if (suffixies != null)
            {
                foreach (string suf in suffixies)
                {
                    sbPref.Append(suf);
                    sbPref.Append(" ");
                }
            }            

            return String.Format("{0, 10} {1} {2, -20}", sbPref, SEPARATOR, sbSuf);
        }


        /*Пример:   без прИ по √ ьн 0 н ьн  */
        public static Pair FromString(string str)
        {
            List<string> pref = new List<string>();
            List<string> suf = new List<string>();

            str = str.Trim(' ');

            var output = str.Split(SEPARATOR); //поделили на кусок приставок и кусок суффиксов

            pref = output[0].Split(' ').ToList();
            pref.RemoveAt(pref.Count - 1); //последний всегда пустой или ненужный

            suf = output[1].Split(' ').ToList();
            suf.RemoveAt(0); //первый всегда пустой или ненужный

            while (pref.Count < 4) {
                pref.Add("");

            }
            while (suf.Count < 9)
            {
                suf.Add("");
            }

            return new Pair(pref, suf);
        }

        /*Пример:   без прИ по √ ьн 0 н ьн  */
        public static Pair FromStringResized(string str)
        {
            List<string> pref = new List<string>();
            List<string> suf = new List<string>();

            var output = str.Split(SEPARATOR); //поделили на кусок приставок и кусок суффиксов
            pref = output[0].Trim(' ').Split(' ').ToList();
            suf = output[1].Trim(' ').Split(' ').ToList();
            if (pref.Count == 0 || pref[0] == "")
            {
                pref = null;
            }
            if (suf.Count == 0 || suf[0] == "")
            {
                suf = null;
            }

            return new Pair(pref, suf);
        }
    }
}

//logs
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

/*
       public override bool Equals(object obj)
       {
           Pair other = obj as Pair;
           if (other == null) 
               return false;

           if (this.prefixes == null && other.prefixes != null)
               return false;

           if (this.suffixies == null && other.suffixies != null) 
               return false;

           if (this.prefixes == null && other.prefixes == null) {
               if (this.suffixies == null && other.suffixies == null)
                   return true;
               if (this.suffixies == other.suffixies) 
                   return true;
           }
           else if (this.prefixes == other.prefixes) {
               if (this.suffixies == null && other.suffixies == null)
                   return true;
               if (this.suffixies == other.suffixies)
                   return true;
           }

           return true;

       }*/
