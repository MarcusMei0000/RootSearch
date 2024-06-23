using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Класс описывает слово: лист префиксов, корень, лист суффиксов, окончание, само слово, транскрипция.
   Конструкторы, что классифицирует, ToString() и ToStringRoot(). */
namespace RootSearch
{
    public class Word
    {
        public List<string> prefixes;
        string root;
        public List<string> suffixes;
        string ending;
        string word;
        string transcription;

        public String Root { get { return root; } }

        public Word(string w, string t, List<string> p, string r, List<string> s, string e = null)
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
        
        public bool HasRoot(string root)
        {
            return this.root == root;
        }

        public bool IsNoAffix()
        {
            return prefixes == null && suffixes == null;
        }
       
        /*Аналогично суффиксам, но с конца, т. к. надо сравнивать, начиная от корня (ближе к корню)*/
        public bool IsClassifiedPreffixes(List<string> givenPrefixes)
        {
            /*if (prefixes == null && givenPrefixes == null)
                return true;

            if (prefixes == null || givenPrefixes == null)
                return false;*/

            if (prefixes == null && givenPrefixes != null)
                return false;

            if (givenPrefixes == null)
                return true;

            if (givenPrefixes.Count > prefixes.Count) 
                return false;

            int j = prefixes.Count - 1;
            for (int i = givenPrefixes.Count - 1; i >= 0; i--)
            {
                if (givenPrefixes[i] != prefixes[j])
                    return false;
                j--;
            }

            return true;
        }

        public bool IsClassifiedPreffixesStrict(List<string> givenPrefixes)
        {
            if (prefixes == null && givenPrefixes != null)
                return false;

            if (prefixes != null && givenPrefixes == null)
                return false;

            if (prefixes == null && givenPrefixes == null)
                return true;

            if (givenPrefixes.Count != prefixes.Count)
                return false;

            int j = prefixes.Count - 1;
            for (int i = givenPrefixes.Count - 1; i >= 0; i--)
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
        public bool IsClassifiedSuffixes(List<string> givenSuffixes)
        {
            /*if (suffixes == null && givenSuffixes == null)
                return true;

            if (suffixes == null || givenSuffixes == null)
                return false;*/

            if (suffixes == null && givenSuffixes != null)
                return false;

            if (givenSuffixes == null)
                return true;

            if (givenSuffixes.Count > suffixes.Count)
                return false;

            for (int i = 0; i < givenSuffixes.Count; i++)
                if (givenSuffixes[i] != suffixes[i])
                    return false;

            return true;
        }

        public bool IsClassifiedSuffixesStrict(List<string> givenSuffixes)
        {
            if (suffixes == null && givenSuffixes != null)
                return false;

            if (suffixes != null && givenSuffixes == null)
                return false;

            if (suffixes == null && givenSuffixes == null)
                return true;

            if (givenSuffixes.Count != suffixes.Count)
                return false;

            for (int i = 0; i < givenSuffixes.Count; i++)
                if (givenSuffixes[i] != suffixes[i])
                    return false;

            return true;
        }

        public bool IsClassified(List<string> pref, List<string> suf)
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