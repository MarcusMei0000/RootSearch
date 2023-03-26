﻿using System;
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
        string[] endings;
        string word;
        string transcription;

        public Word(string w, string t, string[] p, string r, string[] s, string[] e = null)
        {
            word = w;
            transcription = t;
            prefixes = p;
            root = r;
            suffixes = s;
            endings = e;
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
            endings = w.endings;
        }

        //переписать, чтобы сравнение начиналось с последнего
        public bool IsClassifiedPreffixes(string[] pref)
        {
            if (prefixes == pref)
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
            if (suffixes == suf)
                return true;
            if (suffixes == null || suf == null) 
                return false;
            bool b = Enumerable.SequenceEqual(suffixes, suf);
            return Enumerable.SequenceEqual(suffixes, suf);
        }

        public string ToStringRoot()
        {
            return root + ";" + word + ";" + transcription;
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

                if (endings != null)
                    str += string.Join(" ", endings);

                str += " | " + word;
            }

            return str;
        }
    }
}
