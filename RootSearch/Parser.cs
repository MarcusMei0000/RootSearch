using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Web;
using RootSearch;

namespace RootSearch
{
    internal class Parser
    {
        StreamReader streamReader;
        StreamWriter streamWriterYes, streamWriterNo;
        const string suffixes = "suffixes.txt";
        const string prefixes = "prefixes.txt";

        const string output_yes = "сочетающиеся.txt";
        const string output_no = "несочетающиеся.txt";
        public Parser(string filePath)
        {
            streamReader = new StreamReader(filePath, Encoding.Default);
            streamWriterYes = File.CreateText(output_yes);
            streamWriterNo = File.CreateText(output_no);
        }

        //проблемы с открытием файла могут возникнуть
        public void Test(string[] prefixes, string[] suffixies)
        {
            String remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;
            while ((s = streamReader.ReadLine()) != null)
            {
                word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                if (word != null)
                {
                    if (word.IsClassifiedPreffixes(prefixes) && word.IsClassifiedSuffixes(suffixies))
                        streamWriterYes.WriteLine(word.ToStringRoot());
                    else streamWriterNo.WriteLine(word.ToStringRoot());
                }

                while (remainder != null)
                {
                    word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    if (word != null)
                    {
                        if (word.IsClassifiedPreffixes(prefixes) && word.IsClassifiedSuffixes(suffixies))
                            streamWriterYes.WriteLine(word.ToStringRoot());
                        else streamWriterNo.WriteLine(word.ToStringRoot());
                    }
                }
                remainder = null;
            }
            streamWriterYes.Close();
            streamWriterNo.Close();
            streamReader.Close();
        }

        public void OldTest(string[] prefixes, string[] suffixies)
        {
            String remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;
            while ((s = streamReader.ReadLine()) != null)
            {
                word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                if (word != null)
                {
                        streamWriterYes.WriteLine(word.ToString());
                }

                while (remainder != null)
                {
                    word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    if (word != null)
                            streamWriterYes.WriteLine(word.ToString());
                }
                remainder = null;
            }
        }


        private HashSet<string> CreateSet(out HashSet<string> complementarySet)
        {
            const int N = 1000;
            String secondRootWord = "", fullWord = "", transcription = "";
            complementarySet = new HashSet<string>();
            Word word;
            for (int i = 0; i < N; i++)
            {
                word = ParseStringIntoWords(streamReader.ReadLine(), out secondRootWord, ref fullWord, ref transcription);
            }

            return complementarySet;
        }

        /*
        _ между основой и окончанием
        = между суффиксом и производящей основой (т.е. корнем или суффиксом).
        - между приставкой и корнем/другой приставкой.
        пробел любая граница между словами (полнозначными или служебными типа предлогов, частиц и т.п.)
        + между любыми морфемами (значимыми частями слова).
        {-} тире в словах, пишущихся через дефис (кофёр-самолёт).
        0 нулевая морфема (флексия/глагольный суффикс)
        */

        //Нахождение приставок
        private string[] ParseWordPrefix(string word, out string remainder, out string root)
        {
            string[] prefixes = null;

            root = word.Remove(0, word.LastIndexOf("-") + 1);
            prefixes = word.Remove(word.LastIndexOf("-"), word.Length - word.LastIndexOf("-")).Split('-');
            remainder = root;

            return prefixes;
        }

        //Нахождение суффиксов, корень не обрезается?
        private string[] ParseWordSuffix(string word, out string remainder, out string root)
        {
            string[] suffixes = null;

            root = word.Remove(word.IndexOf('='), word.Length - word.IndexOf('='));
            remainder = word.Remove(0, word.IndexOf('=') + 1);
            suffixes = remainder.Split('=');

            return suffixes;
        }

        private Word ParsePartWord(string word, string fullWord, string transcripton)
        {
            string[] prefixes = null;
            string[] suffixes = null;
            string root = null;
            string remainder = word.IndexOf('_') != -1 ? word.Substring(0, word.IndexOf('_')) : word;

            if (remainder.Contains('-'))
            {
                prefixes = ParseWordPrefix(remainder, out remainder, out root);
            }
            else
                root = remainder;

            if (remainder.Contains('='))
            {
                suffixes = ParseWordSuffix(remainder, out remainder, out root);
            }
            else
                root = remainder;

            return new Word(fullWord, transcripton, prefixes, root, suffixes);
        }

        private Word ParseStringIntoWords(string word, out string secondRootWord, ref string fullWord, ref string transcription)
        {
            string remainder;
            string[] pieces = word.Split(';');
            fullWord = pieces.Length >= 2 ? pieces[1] : fullWord;
            transcription = pieces.Length >= 2 ? pieces[2] : transcription;
            string part = pieces.Length >= 2 ? pieces[2] : pieces[0];

            if (part.Contains('{'))
            {
                remainder = part.Substring(0, part.IndexOf('{'));
                secondRootWord = part.Substring(part.IndexOf('{') + 3, part.Length - part.IndexOf('{') - 3);
                return ParsePartWord(remainder, fullWord, transcription);
            }
            else if (part.Contains('['))
            {
                remainder = part.Substring(0, part.IndexOf('['));
                secondRootWord = part.Substring(part.IndexOf('[') + 2, part.Length - part.IndexOf('[') - 2);
                return ParsePartWord(remainder, fullWord, transcription);
            }
            else if (part.Contains(' ')) //постфиксы
            {
                remainder = part.Substring(0, part.IndexOf(' '));
                secondRootWord = part.Substring(part.IndexOf(' ') + 1, part.Length - part.IndexOf(' ') - 1);
                return ParsePartWord(remainder, fullWord, transcription);
            }
            else
            {
                remainder = part;
                secondRootWord = null;
                return ParsePartWord(remainder, fullWord, transcription);
            }
        }

        private bool Check(string s, char c)
        {
            foreach (char st in s)
            {
                if (st == c) return true;
            }
            return false;
        }

        private void PrintToFile(string[] strings, string filePath)
        {
            streamWriterYes = new StreamWriter(filePath);
            foreach (string s in strings)
            {
                streamWriterYes.WriteLine(s);
            }
        }
    }
}
/*logs
private Word ParseWord(string word, out string secondRootWord)
        {
            string[] prefixes = null;
            string[] suffixes = null;
            string root = null;
            string[] pieces = word.Split(';');
            // if (word.IndexOf(' ') == -1 && word.IndexOf('[') == -1 && word.IndexOf('{') == -1 && word.IndexOf('+') == -1)
            if (!word.Contains(' ') && !word.Contains('[') && !word.Contains('{') && !word.Contains('+'))
            {
                string remainder = pieces[2].IndexOf('_') != -1 ? pieces[2].Substring(0, pieces[2].IndexOf('_')) : pieces[2];
                //если нет приставки
                //Найти '-' с конца, отрезать этот кусок. 1й расплитить на суффиксы, 2й в остаток
                if (remainder.Contains('-'))
                {
                    int t = remainder.LastIndexOf("-");
                    //root = remainder.Remove();
                    root = remainder.Remove(0, remainder.LastIndexOf("-") + 1);
                    prefixes = remainder.Remove(remainder.LastIndexOf("-"), remainder.Length - remainder.LastIndexOf("-")).Split('-'); ;
                    
                    remainder = root;
                }
                else
                    root = remainder;

                if (remainder.Contains('='))
                {
                    root = remainder.Remove(remainder.IndexOf('='), remainder.Length-remainder.IndexOf('='));
                    remainder = remainder.Remove(0, remainder.IndexOf('=')+1);
                    suffixes = remainder.Split('=');
                }
                else 
                    root = remainder;
                // _# сьн  т.е. постфикс ся
                //ебучие нолики в суффиксах
                //добавить все суффиксы и приставки в свои файлы
            }
            else
            {
                string str = pieces[2];
                if (!str.Contains("сьн"))
                    strWrt.WriteLine(str);
                secondRootWord = null;
                return null;
            }
            secondRootWord = null;
            return new Word(pieces[1], prefixes, root, suffixes);
        }


        private Word ParsePartWord(string word, string fullWord)
        {
            string[] prefixes = null;
            string[] suffixes = null;
            string root = null;
            string remainder = word.IndexOf('_') != -1 ? word.Substring(0, word.IndexOf('_')) : word;

            if (remainder.Contains('-'))
            {
                root = remainder.Remove(0, remainder.LastIndexOf("-") + 1);
                 prefixes = remainder.Remove(remainder.LastIndexOf("-"), remainder.Length - remainder.LastIndexOf("-")).Split('-');
                 remainder = root;
                //prefixes = ParseWordPrefix(remainder, out remainder, out root);
            }
            else
    root = remainder;

if (remainder.Contains('=')) // добавить 0 нулевая морфема (флексия/глагольный суффикс)
{
    root = remainder.Remove(remainder.IndexOf('='), remainder.Length - remainder.IndexOf('='));
     remainder = remainder.Remove(0, remainder.IndexOf('=') + 1);
     suffixes = remainder.Split('=');
    //suffixes = ParseWordSuffix(remainder, out remainder, out root);
}
else
    root = remainder;

return new Word(fullWord, prefixes, root, suffixes);
        }
*/