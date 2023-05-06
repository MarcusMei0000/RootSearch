using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    internal class Word
    {
        string[] prefixes;
        string root;
        string[] suffixes;
        string ending;
        string word;
        string transcription;

        public String Root { get; }

        public Word(string w, string t, string[] p, string r, string[] s, string e = null)
        {
            word = w;
            transcription = t;
            prefixes = p;
            root = r;
            suffixes = s;
            ending = e;
        }

        public Word()
        {
        }

        public Word(Word w)
        {
            word = w.word;
            transcription = w.transcription;
            prefixes = w.prefixes;
            root = w.root;
            suffixes = w.suffixes;
            ending = w.ending;
        }
       
        /*Аналогично суффиксам, но с конца, т. к. надо сравнивать, начиная от корня (ближе к корню)*/
        public bool IsClassifiedPreffixes(string[] givenPrefixes)
        {
            /*if (prefixes == null && givenPrefixes == null)
                return true;

            if (prefixes == null || givenPrefixes == null)
                return false;*/

            if (prefixes == null && givenPrefixes != null)
                return false;

            if (givenPrefixes == null)
                return true;

            if (givenPrefixes.Length > prefixes.Length) 
                return false;

            int j = prefixes.Length - 1;
            for (int i = givenPrefixes.Length - 1; i >= 0; i--)
            {
                if (givenPrefixes[i] != prefixes[j])
                    return false;
                j--;
            }

            return true;
        }

        /*Если суффиксов нигде нет - true
          Если суффиксов у одного есть, у другого нет - false
          Если количество введённых суффиксов больше, чем суффиксов в слове - false
          Если данные суффиксы по очереди совпадают с суффиксами словами - true
        */
        public bool IsClassifiedSuffixes(string[] givenSuffixes)
        {
            /*if (suffixes == null && givenSuffixes == null)
                return true;

            if (suffixes == null || givenSuffixes == null)
                return false;*/

            if (suffixes == null && givenSuffixes != null)
                return false;

            if (givenSuffixes == null)
                return true;

            if (givenSuffixes.Length > suffixes.Length)
                return false;

            for (int i = 0; i < givenSuffixes.Length; i++)
                if (givenSuffixes[i] != suffixes[i])
                    return false;

            return true;
        }

        public bool IsClassified(string[] pref, string[] suf)
        {
            //если приставки уже не подходят, нет смысла проверять дальше
            if (!IsClassifiedPreffixes(pref))
                return false;

            //обе проверки успешно пройдены
            if (IsClassifiedSuffixes(suf))
                return true;

            //вторая проверка не пройдена
            return false;
        }

        //если нет суффиксов давать окончание
        //Можно не хранить _ в окончании, а каждый раз его подставлять, но мне так не нравится
        public string ToStringRoot()
        {
            if (ending == null)
                return root + ";" + word + ";" + transcription;
            else return root + ending + ";" + word + ";" + transcription;
        }

        override public string ToString()
        {
            string str = "";
            if (root != null)
            {
                if (prefixes != null)
                    str += string.Join(" ", prefixes) + " | ";

                str += string.Join(" ", root) + " | ";

                if (suffixes != null)
                    str += string.Join(" ", suffixes) + " | ";

                if (ending != null)
                    str += string.Join(" ", ending);

                str += " | " + word;
            }

            return str;
        }
    }
}

//logs
/*public bool IsClassifiedPreffixes(string[] pref)
        {
           // if (prefixes == pref)
           //     return true;
            if (prefixes == null && pref == null)
               return true;
            if (prefixes == null || pref == null) 
                return false;
            bool b = Enumerable.SequenceEqual(prefixes, pref);
            return Enumerable.SequenceEqual(prefixes, pref);
        }

        //переписать, чтобы сравнение начиналось с первого
        //Если слово например сожержит [пре-от-под], ввели [от-под], то такой корень я считаю сочетающимся с [от-под], верно?
        public bool IsClassifiedSuffixes(string[] suf)
        {
           // if (suffixes == suf)
             //   return true;
            if (suffixes == null || suf == null) 
                return false;
            bool b = Enumerable.SequenceEqual(suffixes, suf);
            return Enumerable.SequenceEqual(suffixes, suf);
        }*/
