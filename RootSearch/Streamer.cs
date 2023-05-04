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
           
    }
}
