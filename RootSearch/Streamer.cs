using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    public class Streamer
    {
        const string EXTENSION = ".txt";



        public static List<IEnumerable<string>> CreateListOfIEnumerable(string[] fileNames)
        {
            List<IEnumerable<string>> sets = new List<IEnumerable<string>>();

            foreach (string file in fileNames)
            {
                sets.Add(Streamer.CreateIEnumerableFromFile(file));
            }

            return sets;
        }

        public static List<HashSet<string>> CreateListOfRootSets(string[] fileNames)
        {
            List<HashSet<string>> sets = new List<HashSet<string>>();
            List<string> tmp = new List<string>();

            foreach (string file in fileNames)
            {
                tmp = Streamer.CreateListFromFile(file);
                sets.Add(Streamer.CreateRootSet(tmp));
            }

            return sets;
        }

        public static List<string> CreateListFromFile(string filePath)
        {
            var list = new List<string>();
            StreamReader sr = File.OpenText(filePath);
            String input;

            while ((input = sr.ReadLine()) != null)
            {
                list.Add(input);
            }

            return list;
        }

        public static IEnumerable<string> CreateIEnumerableFromFile(string filePath)
        {
            Collection<string> list = new Collection<string>();
            StreamReader sr = File.OpenText(filePath);
            String input;

            while ((input = sr.ReadLine()) != null)
            {
                list.Add(input);
            }

            return list;
        }

        public static bool HasRoot(string s, HashSet<string> roots)
        {
            return roots.Contains(ExtractRootFromString(s));
        }

        public static string ExtractRootFromString(string s)
        {
            return s.Substring(0, s.IndexOf(";"));
        }

        public static HashSet<string> CreateRootSet(List<string> wordList)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (string s in wordList)
            {
                set.Add(ExtractRootFromString(s));
            }
            return set;
        }

        //подумать как не печатать последний /n
        //или не учитывать при сравнении последнюю строку
        public static void Print(List<string> set, StreamWriter stream)
        {
            foreach (string s in set)
                stream.WriteLine(s);

            stream.Close();
        }

        public static void Print(IEnumerable<string> set, StreamWriter stream)
        {
            foreach (string s in set)
                stream.WriteLine(s);

            stream.Close();
        } 


        // съ+пер_vA+н_сочетающиеся_корни.txt
        // нейминг /aж\ и аж будут одинаковые из-за проблем с путём :(
        // посмотреть с _ чтобы поаккуратней генерировалось
        // перписать всю эту функцию поаккуратнее
        //почему-то нельзя сохранять в корень C:\
        public static string CreateFileName(string[] prefixes, string[] suffixies, string end, string folderName)
        {
            string outp = "";
            string result = "";
            string tmp;

            char c = '\\';
            char d = '/';
            char e = '|';

            List<string> invalidSymbols = new List<string>() { "\\", "/", "|", "<", ">" };

            if (prefixes != null)
            {
                foreach (string s in prefixes)
                {
                    if (s != null && s != "")
                    {
                        /* tmp = s;
                         foreach (var symbol in invalidSymbols)
                         {
                             tmp = tmp.Replace(symbol, String.Empty);
                         }*/

                        tmp = s.Replace(c.ToString(), String.Empty);
                        tmp = tmp.Replace(d.ToString(), String.Empty);
                        tmp = tmp.Replace(e.ToString(), String.Empty);

                        outp += tmp + "+";
                    }
                }
                if (outp != "")
                {
                    outp = outp.Remove(outp.Length - 1);
                    outp += "_";
                }
            }

            if (suffixies != null)
            {
                foreach (string s in suffixies)
                {
                    if (s != null && s != "")
                    {
                        tmp = s.Replace(c.ToString(), String.Empty);
                        tmp = tmp.Replace(d.ToString(), String.Empty);
                        tmp = tmp.Replace(e.ToString(), String.Empty);

                        outp += tmp + "+";
                    }
                }
                if (outp != "")
                {
                    outp = outp.Remove(outp.Length - 1);
                }
            }

            outp += "_" + end;
            result = folderName + '\\' + outp;

            while (File.Exists(result + EXTENSION))
                result += '1';

            return result + EXTENSION;
        }
        
        public static string CreateFileNameForSet(string[] names)
        {
            StringBuilder sb = new StringBuilder();
            string tmp = "";
            foreach(string name in names)
            {
                tmp = name.Remove(0, name.LastIndexOf('\\') + 1);
                sb.Append(tmp.Substring(0, tmp.Length - 4));
                sb.Append('_');
            }

            return sb.ToString() + EXTENSION;
        }
    }
}
