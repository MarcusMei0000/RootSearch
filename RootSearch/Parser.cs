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

/*Общий парсер. На вход аффиксальные окружения. 
  На выход 2 файла с/без подходящими корнями (перечислены все слова, т.е. корни повторяются)
  Много bool-функций и проверок, Classify, парсинг строк*/
namespace RootSearch
{
    public class Parser
    {
        StreamReader streamReader;

        const string OUTPUT_YES = "";
        const string OUTPUT_NO = "_н";
        const string EXTENSION = ".txt";
        const string PROCLITIC_PATH = "proclictic.txt";
        const string ENCLITIC_PATH = "enclictic.txt";

        string filePath = "";
        string folderName = "";

        List<string> proclitic = new List<string>();
        List<string> enclitic = new List<string>();

        public Parser(string filePath, string folderName)
        {
            this.filePath = filePath;
            this.folderName = folderName;
            proclitic = Streamer.CreateListFromFile(PROCLITIC_PATH);
            enclitic = Streamer.CreateListFromFile(ENCLITIC_PATH);
        }

        private bool IsProclitic(Word w)
        {
            return proclitic.Contains(w.Root.ToLower());
        }

        private bool IsNoInputAffix(List<string> pref, List<string> suf)
        {
            return pref == null && suf == null;
        }

        private bool IsEnclitic(Word w)
        {
            return enclitic.Contains(w.Root.ToLower());
        }


        private void ClassifyWord(Word word, List<string> prefixes, List<string> suffixies, ref List<string> setYes, ref List<string> setNo)
        {
            if (word != null && !IsProclitic(word) && !IsEnclitic(word))
            {
                if (word.IsClassifiedPreffixes(prefixes) && word.IsClassifiedSuffixes(suffixies) )
                {
                    setYes.Add(word.ToStringRoot());
                }
                else
                {
                    setNo.Add(word.ToStringRoot());
                }
            }
        }

        private void ClassifyWord(Word word, ref List<string> setYes, ref List<string> setNo)
        {
            if (word != null && !IsProclitic(word) && !IsEnclitic(word))
            {
                if (word.IsNoAffix())
                {
                    setYes.Add(word.ToStringRoot());
                }
                else
                {
                    setNo.Add(word.ToStringRoot());
                }
            }
        }

        private List<string> ParseFileWithoutAffix(out List<string> setNoComplimentary)
        {            
            List<string> setYes = new List<string>();
            List<string> setNo = new List<string>();
            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                ClassifyWord(word, ref setYes, ref setNo);

                while (remainder != null)
                {
                    word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    ClassifyWord(word, ref setYes, ref setNo);
                }
            }

            setNoComplimentary = setNo;
            return setYes;
        }

        private List<string> ParseFileWithAffix(List<string> prefixes, List<string> suffixies, out List<string> setNoComplimentary)
        {
            List<string> setYes = new List<string>();
            List<string> setNo = new List<string>();
            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                ClassifyWord(word, prefixes, suffixies, ref setYes, ref setNo);

                while (remainder != null)
                {
                    word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    ClassifyWord(word, prefixes, suffixies, ref setYes, ref setNo);
                }
            }

            setNoComplimentary = setNo;
            return setYes;
        }        
        
        private List<string> ClassifyWordsFromFile(List<string> prefixes, List<string> suffixies, out List<string> setNoComplimentary)
        {
            setNoComplimentary = new List<string>();
            List<string> setYes = new List<string>();

            if (!IsNoInputAffix(prefixes, suffixies))
            {
                return ParseFileWithAffix(prefixes, suffixies, out setNoComplimentary);
            }
            else
            {
                return ParseFileWithoutAffix(out setNoComplimentary);
            }
        }

        public string[] CreateMainFiles(List<string> prefixes, List<string> suffixies)
        {
            string[] filePathes = new string[2];
            filePathes[0] = Streamer.CreateFileName(prefixes, suffixies, folderName);
            filePathes[1] = Streamer.CreateFileName(prefixes, suffixies, folderName, OUTPUT_NO);

            StreamWriter streamWriterYes, streamWriterNo;

            streamReader = new StreamReader(filePath, Encoding.Default);
            streamWriterYes = new StreamWriter(filePathes[0], false);
            streamWriterNo = new StreamWriter(filePathes[1], false);

            List<string> setNoComplimantery = new List<string>();
            List<string> setYesComplimentary = ClassifyWordsFromFile(prefixes, suffixies, out setNoComplimantery);

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
        private static List<string> ParseWordPrefix(string word, out string remainder, out string root)
        {
            string[] prefixes = null;

            root = word.Remove(0, word.LastIndexOf("-") + 1);
            prefixes = word.Remove(word.LastIndexOf("-"), word.Length - word.LastIndexOf("-")).Split('-');
            remainder = root;

            return prefixes.ToList();
        }

        //Нахождение суффиксов
        private static List<string> ParseWordSuffix(string word, out string remainder, out string root)
        {
            string[] suffixes = null;

            root = word.Remove(word.IndexOf('='), word.Length - word.IndexOf('='));
            remainder = word.Remove(0, word.IndexOf('=') + 1);
            suffixes = remainder.Split('=');

            return suffixes.ToList();
        }


        //Создаёт слово с окончанием или без в зависимости от наличия суффикса
        private static Word ParsePartWord(string word, string fullWord, string transcripton)
        {
            List<string> prefixes = null;
            List<string> suffixes = null;
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

        
        public static Word ParseStringIntoWords(string word, out string secondRootWord, ref string fullWord, ref string transcription)
        {
            string firstRootWord;
            string[] pieces = word.Split(';');
            fullWord = pieces.Length >= 2 ? pieces[1] : fullWord;
            transcription = pieces.Length >= 2 ? pieces[2] : transcription;
            string part = pieces.Length >= 2 ? pieces[2] : pieces[0];

            if (part.Contains('{'))
            {
                firstRootWord = part.Substring(0, part.IndexOf('{'));
                secondRootWord = part.Substring(part.IndexOf('{') + 3, part.Length - part.IndexOf('{') - 3);
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
            else if (part.Contains('['))
            {
                firstRootWord = part.Substring(0, part.IndexOf('['));
                secondRootWord = part.Substring(part.IndexOf('[') + 2, part.Length - part.IndexOf('[') - 2);
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
            else if (part.Contains(' '))
            {
                firstRootWord = part.Substring(0, part.IndexOf(' '));
                secondRootWord = part.Substring(part.IndexOf(' ') + 1, part.Length - part.IndexOf(' ') - 1);
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
            else if (part.Contains('|'))
            {
                firstRootWord = part.Substring(0, part.IndexOf('|'));
                secondRootWord = part.Substring(part.IndexOf('|') + 1, part.Length - part.IndexOf('|') - 1);
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
            else
            {
                firstRootWord = part;
                secondRootWord = null;
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
        }
    }
}