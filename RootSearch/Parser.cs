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
using DocumentFormat.OpenXml.Bibliography;
using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Spreadsheet;

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


        List<char> ROOT_SEPARATORS = new List<char> { '[', '{', '|', ' ' };
        /*Можно преобразовать алгоритм ParseStringIntoWords, 
          написать там цикл, что первый найденный из этого списка, потом case, 
          потом "remainder на этот же цикл"*/
        //спросить у Кости и Дениса - оптимальный алгоритм первого вхождения любого из этого списка
        //и что бы потом адекватно выдавало какой из элементов был первый


        public Parser(string filePath, string folderName)
        {
            this.filePath = filePath;
            this.folderName = folderName;
            proclitic = Streamer.CreateListFromFile(Properties.Resources.proclictic_str);
            enclitic = Streamer.CreateListFromFile(Properties.Resources.enclictic_str);
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
                if (word.IsClassifiedPreffixes(prefixes) && word.IsClassifiedSuffixes(suffixies))
                {
                    setYes.Add(word.ToStringRoot());
                }
                else
                {
                    setNo.Add(word.ToStringRoot());
                }
            }
        }
        private void ClassifyWordStrict(Word word, List<string> prefixes, List<string> suffixies, ref List<string> setYes, ref List<string> setNo)
        {
            if (word != null && !IsProclitic(word) && !IsEnclitic(word))
            {
                if (word.IsClassifiedPreffixesStrict(prefixes) && word.IsClassifiedSuffixesStrict(suffixies))
                {
                    setYes.Add(word.ToStringRoot());
                }
                else
                {
                    setNo.Add(word.ToStringRoot());
                }
            }

        }

        private void ClassifyWord(Word word, List<string> prefixes, List<string> suffixies, ref List<string> setYes, bool isPref)
        {
            if (word != null && !IsProclitic(word) && !IsEnclitic(word))
            {
                if (isPref)
                {
                    if (prefixes == null && word.IsClassifiedSuffixes(suffixies))
                        setYes.Add(word.ToStringEnviromentRoot(isPref));

                    if (word.IsClassifiedPreffixesStrict(prefixes) && suffixies == null)
                        setYes.Add(word.ToStringEnviromentRoot(isPref));
                }
                else
                {
                    if (prefixes == null && word.IsClassifiedSuffixesStrict(suffixies))
                        setYes.Add(word.ToStringEnviromentRoot(isPref));

                    if (word.IsClassifiedPreffixes(prefixes) && suffixies == null)
                        setYes.Add(word.ToStringEnviromentRoot(isPref));
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

        private void ClassifyWord(Word word, string root, ref List<string> setYes, ref List<string> setNo)
        {
            if (word != null && !IsProclitic(word) && !IsEnclitic(word))
            {
                if (word.HasRoot(root))
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
                //if (!s.Contains("+"))
                {
                    word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                    ClassifyWord(word, ref setYes, ref setNo);

                    while (remainder != null)
                    {
                        word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                        ClassifyWord(word, ref setYes, ref setNo);
                    }
                }
            }

            setNoComplimentary = setNo;
            return setYes;
        }

        public List<string> ParseFileWithAffixStrict(List<string> prefixes, List<string> suffixies, bool isPref)
        {
            streamReader = new StreamReader(filePath, Encoding.Default);

            List<string> setYes = new List<string>();
            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                {
                    word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                    
                    ClassifyWord(word, prefixes, suffixies, ref setYes, isPref);

                    while (remainder != null)
                    {
                        word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                        ClassifyWord(word, prefixes, suffixies, ref setYes, isPref);
                    }
                }
            }

            return setYes;
        }

        private List<string> ParseFileWithAffix(List<string> prefixes, List<string> suffixies, out List<string> setNoComplimentary, bool isStrict)
        {
            List<string> setYes = new List<string>();
            List<string> setNo = new List<string>();
            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                //if (!s.Contains("+"))
                {
                    word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                    if (isStrict)
                    {
                        ClassifyWordStrict(word, prefixes, suffixies, ref setYes, ref setNo);
                    }
                    else
                    {
                        ClassifyWord(word, prefixes, suffixies, ref setYes, ref setNo);
                    }

                    while (remainder != null)
                    {
                        word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                        if (isStrict)
                        {
                            ClassifyWordStrict(word, prefixes, suffixies, ref setYes, ref setNo);
                        }
                        else
                        {
                            ClassifyWord(word, prefixes, suffixies, ref setYes, ref setNo);
                        }
                    }
                }
            }

            setNoComplimentary = setNo;
            return setYes;
        }

        private List<string> ClassifyWordsFromFile(List<string> prefixes, List<string> suffixies, out List<string> setNoComplimentary, bool isStrict)
        {
            setNoComplimentary = new List<string>();
            List<string> setYes = new List<string>();

            if (!IsNoInputAffix(prefixes, suffixies))
            {
                return ParseFileWithAffix(prefixes, suffixies, out setNoComplimentary, isStrict);
            }
            else
            {
                return ParseFileWithoutAffix(out setNoComplimentary);
            }
        }

        private List<string> ParseFileByRoot(string root, out List<string> setNoComplimentary)
        {
            List<string> setYes = new List<string>();
            List<string> setNo = new List<string>();
            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                //if (!s.Contains("+"))
                {
                    word = ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                    ClassifyWord(word, root, ref setYes, ref setNo);

                    while (remainder != null)
                    {
                        word = ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                        ClassifyWord(word, root, ref setYes, ref setNo);
                    }
                }
            }

            setNoComplimentary = setNo;
            return setYes;
        }

        private List<string> ClassifyWordsFromFileByRoot(string root, out List<string> setNoComplimentary)
        {
            setNoComplimentary = new List<string>();
            List<string> setYes = new List<string>();

            return ParseFileByRoot(root, out setNoComplimentary);
        }

        public string[] CreateFilesForRoot(string root)
        {
            string[] filePathes = new string[2];
            filePathes[0] = Streamer.CreateFileName(new List<string> { root }, null, folderName);
            filePathes[1] = Streamer.CreateFileName(new List<string> { root }, null, folderName, OUTPUT_NO);

            StreamWriter streamWriterYes, streamWriterNo;

            streamReader = new StreamReader(filePath, Encoding.Default);
            streamWriterYes = new StreamWriter(filePathes[0], false);
            streamWriterNo = new StreamWriter(filePathes[1], false);

            List<string> setNoComplimantery = new List<string>();
            List<string> setYesComplimentary = ClassifyWordsFromFileByRoot(root, out setNoComplimantery);

            Streamer.Print(setYesComplimentary, streamWriterYes);
            Streamer.Print(setNoComplimantery, streamWriterNo);

            streamWriterYes.Close();
            streamWriterNo.Close();
            streamReader.Close();

            return filePathes;
        }


        public string[] CreateMainFiles(List<string> prefixes, List<string> suffixies, bool isStrict)
        {
            string[] filePathes = new string[2];
            filePathes[0] = Streamer.CreateFileName(prefixes, suffixies, folderName);
            filePathes[1] = Streamer.CreateFileName(prefixes, suffixies, folderName, OUTPUT_NO);

            StreamWriter streamWriterYes, streamWriterNo;

            streamReader = new StreamReader(filePath, Encoding.Default);
            streamWriterYes = new StreamWriter(filePathes[0], false);
            streamWriterNo = new StreamWriter(filePathes[1], false);

            List<string> setNoComplimantery = new List<string>();
            List<string> setYesComplimentary = ClassifyWordsFromFile(prefixes, suffixies, out setNoComplimantery, isStrict);

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

            if (part.Contains('['))
            {
                firstRootWord = part.Substring(0, part.IndexOf('['));
                secondRootWord = part.Substring(part.IndexOf('[') + 3, part.Length - part.IndexOf('[') - 3);
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
            else if (part.Contains('{'))
            {
                firstRootWord = part.Substring(0, part.IndexOf('{'));
                secondRootWord = part.Substring(part.IndexOf('{') + 3, part.Length - part.IndexOf('{') - 3);
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
            else if (part.Contains('|'))
            {
                firstRootWord = part.Substring(0, part.IndexOf('|'));
                secondRootWord = part.Substring(part.IndexOf('|') + 1, part.Length - part.IndexOf('|') - 1);
                if (secondRootWord[0] == '_' || secondRootWord[0] == '=')
                    secondRootWord = null;
                return ParsePartWord(firstRootWord, fullWord, transcription);
            }
            else if (part.Contains(' '))
            {
                firstRootWord = part.Substring(0, part.IndexOf(' '));
                secondRootWord = part.Substring(part.IndexOf(' ') + 1, part.Length - part.IndexOf(' ') - 1);
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