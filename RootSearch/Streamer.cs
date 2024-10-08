﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Вспомогательный класс. Читать из файла в IEnumerable, List и Печать 
   CreateListOfRootSets(), HasRootInSet(), CreateRootSet()
   RemoveInvalidSymbols()
   Генерация имен файлов с множествами и их пересечениями*/
namespace RootSearch
{
    public class Streamer
    {
        const string EXTENSION = ".txt";

        static List<string> INVALID_SYMBOLS = new List<string>() { "\\", "/", "|", "<", ">", "\"" };

        public static List<IEnumerable<string>> CreateListOfIEnumerable(string[] fileNames)
        {
            List<IEnumerable<string>> lists = new List<IEnumerable<string>>();

            foreach (string file in fileNames)
            {
                lists.Add(Streamer.CreateIEnumerableFromFile(file));
            }

            return lists;
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

        public static HashSet<string> CreateSetFromFile(string filePath)
        {
            var set = new HashSet<string>();
            StreamReader sr = File.OpenText(filePath);
            String input;

            while ((input = sr.ReadLine()) != null)
            {
                set.Add(input);
            }

            return set;
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

        public static bool HasRootInSet(string s, HashSet<string> roots)
        {
            return roots.Contains(ExtractRootFromString(s));
        }

        public static string ExtractRootFromString(string s)
        {
            return s.Substring(0, s.IndexOf(";"));
        }

        public static HashSet<string> CreateRootSet(List<string> list)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (string str in list)
            {
                set.Add(ExtractRootFromString(str));
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

        public static void Print(List<FullEnvironment> set, StreamWriter stream)
        {
            foreach (var s in set)
                stream.WriteLine(s.ToString());

            stream.Close();
        }

        public static void Print(HashSet<string> set, StreamWriter stream)
        {
            foreach (var s in set)
                stream.WriteLine(s.ToString());

            stream.Close();
        }

        public static string RemoveInvalidSymbols(List<string> affixes)
        {
            string tmp;
            string result = "";
            if (affixes != null)
            {
                foreach (string affix in affixes)
                {
                    if (affix != null && affix != "")
                    {
                        tmp = affix;
                        foreach (var symbol in INVALID_SYMBOLS)
                        {
                            tmp = tmp.Replace(symbol, String.Empty);
                        }

                        result += tmp + "+";
                    }
                }
                if (result != "")
                {
                    result = result.Remove(result.Length - 1);
                }
            }

            return result;
        }

        // съ+пер_vA+н_сочетающиеся_корни.txt
        // нейминг /aж\ и аж будут одинаковые из-за проблем с путём :(
        // посмотреть с _ чтобы поаккуратней генерировалось
        public static string CreateFileName(List<string> prefixes, List<string> suffixies, string folderName, string end = "")
        {
            string result = folderName + '\\' + RemoveInvalidSymbols(prefixes) + '_' + RemoveInvalidSymbols(suffixies) + end;
            
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
            sb.Remove(sb.Length - 1, 1);

            while (File.Exists(sb + EXTENSION))
                sb.Append('1');

            return sb.ToString() + EXTENSION;
        }
    }
}
