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
using System.Security.Permissions;

namespace RootSearch
{
    internal class Parser
    {
        StreamReader streamReader;

        const string OUTPUT_YES = "сочетающиеся";
        const string OUTPUT_NO = "несочетающиеся";
        const string EXTENSION = ".txt";
        const string PROCLITIC_PATH = "proclictic.txt";
        const string ECLITIC_PATH = "eclictic.txt";

        string filePath = "";
        string folderName = "";

        List<string> proclitic = new List<string>();
        List<string> eclitic = new List<string>();

        public Parser(string filePath, string folderName)
        {
            this.filePath = filePath;
            this.folderName = folderName;
            proclitic = Streamer.CreateListFromFile(PROCLITIC_PATH);
            //eclitic = CreateListFromFile(ECLITIC_PATH);
        }

        //игнорировать экликтику?

        private bool IsProclitic(Word w)
        {
            return proclitic.Contains(w.Root);
        }

        private bool IsNoInputAffix(string[] pref, string[] suf)
        {
            return pref == null && suf == null;
        }

        private bool IsEclitic(Word w)
        {
            return eclitic.Contains(w.Root);
        }


        //либо в отдельную функцию, либо флаг
        private List<string> ParseFile(string[] prefixes, string[] suffixies, out List<string> setNoComplimentary)
        {
            List<string> setYes = new List<string>();
            List<string> setNo = new List<string>();
            String remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                if (word != null)
                {
                    //&& !IsEclictic(word)
                    if (IsNoInputAffix(prefixes, suffixies))
                    {
                        if (word.IsNoAffix())
                            setYes.Add(word.ToStringRoot());

                        else
                        {
                            setNo.Add(word.ToStringRoot());
                        }
                    }
                    else
                    {
                        if (word.IsClassifiedPreffixes(prefixes) && word.IsClassifiedSuffixes(suffixies) && !IsProclitic(word))
                        {
                            setYes.Add(word.ToStringRoot());
                        }
                        else
                        {
                            setNo.Add(word.ToStringRoot());
                        }
                    }
                }

                /**
                 * word  = ParseString
                 * if(word != null){
                 *  isReminderFirst = true;
                 *  while reminder!=null {
                 *      if(!isReminderFirst){
                 *          word = ParseString          
                 *      }
                 *      isReminderFirst = false;
                 *      
                 *  }
                 * }
                 */

                while (remainder != null)
                {
                    word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    if (word != null)
                    {
                        if (IsNoInputAffix(prefixes, suffixies))
                        {
                            if (word.IsNoAffix())
                                setYes.Add(word.ToStringRoot());

                            else
                            {
                                setNo.Add(word.ToStringRoot());
                            }
                        }
                        else
                        {
                            if (word.IsClassifiedPreffixes(prefixes) && word.IsClassifiedSuffixes(suffixies) && !IsProclitic(word))
                            {
                                setYes.Add(word.ToStringRoot());
                            }
                            else
                            {
                                setNo.Add(word.ToStringRoot());
                            }
                        }
                    }
                }
            }
            remainder = null;

            setNoComplimentary = setNo;
            return setYes;
        }

        public string[] CreateMainFiles(string[] prefixes, string[] suffixies)
        {
            string[] filePathes = new string[2];
            filePathes[0] = Streamer.CreateFileName(prefixes, suffixies, OUTPUT_YES, folderName);
            filePathes[1] = Streamer.CreateFileName(prefixes, suffixies, OUTPUT_NO, folderName);

            StreamWriter streamWriterYes, streamWriterNo;

            streamReader = new StreamReader(filePath, Encoding.Default);
            streamWriterYes = new StreamWriter(filePathes[0], false);
            streamWriterNo = new StreamWriter(filePathes[1], false);

            List<string> setNoComplimantery = new List<string>();
            List<string> setYesComplimentary = ParseFile(prefixes, suffixies, out setNoComplimantery);

            Streamer.Print(setYesComplimentary, streamWriterYes);
            Streamer.Print(setNoComplimantery, streamWriterNo);

            streamWriterYes.Close();
            streamWriterNo.Close();
            streamReader.Close();

            return filePathes;
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

        //Нахождение суффиксов
        private string[] ParseWordSuffix(string word, out string remainder, out string root)
        {
            string[] suffixes = null;

            root = word.Remove(word.IndexOf('='), word.Length - word.IndexOf('='));
            remainder = word.Remove(0, word.IndexOf('=') + 1);
            suffixes = remainder.Split('=');

            return suffixes;
        }


        //Создаёт слово с окончанием или без в зависимости от суффикса
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

            if (suffixes != null || word.IndexOf('_') == -1)
                return new Word(fullWord, transcripton, prefixes, root, suffixes);

            return
                new Word(fullWord, transcripton, prefixes, root, suffixes, word.Substring(word.IndexOf('_'), word.Length - word.IndexOf('_')));
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
            else if (part.Contains(' '))
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
    }
}
/*logs
 * 
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
 * 

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
 * 
 *         public void OldTest(string[] prefixes, string[] suffixies)
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