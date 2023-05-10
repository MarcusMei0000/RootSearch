using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace RootSearch
{
    internal class Set
    {
        List<string> fileNames = new List<string>();

        public Set() { }

        public Set(List<string> fileNames)
        {
            this.fileNames = fileNames;
        }

        private static List<IEnumerable<string>> CreateListOfIEnumerable(string[] fileNames)
        {
            List<IEnumerable<string>> sets = new List<IEnumerable<string>>();

            foreach (string file in fileNames)
            {
                sets.Add(Streamer.CreateIEnumerableFromFile(file));
            }

            return sets;
        }

        private static List<HashSet<string>> CreateListOfRootSets(string[] fileNames)
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

        //Возвращает множество, являющиеся пересечением корней
        public static HashSet<string> FindRootIntersection(string[] fileNames)
        {
            List<HashSet<string>> sets = CreateListOfRootSets(fileNames);

            IEnumerable<string> tmp = sets[0];
            for (int i = 1; i < sets.Count; i++)
            {
                tmp = tmp.AsEnumerable().Intersect(sets[i]);
            }

            return tmp.ToHashSet();
        }
        
        //нужен ли нам Set, чтобы строки не повторялись или обойдёмся List(?)
        //Возвращает множество строк с пересекающимися корнями, которое готово для печати в файл
        //Можно и посортировать перед выходом
        public static List<string> CreateSetIntersection(string[] fileNames)
        {
            List<string> result = new List<string>();

            var list = CreateListOfIEnumerable(fileNames);
            var rootSet = FindRootIntersection(fileNames);

            foreach(var l in list)
            {
                foreach (var s in l)
                {
                    if (Streamer.HasRoot(s, rootSet))
                        result.Add(s);
                }
            }

            return result;
        }       

        public static string TestSetIntersection(string[] fileNames, string folderName)
        {
            string outputPath = folderName + Streamer.CreateFileNameForSet(fileNames);
            StreamWriter stream = new StreamWriter(outputPath, false);

            var set = CreateSetIntersection(fileNames);

            Streamer.Print(set, stream);

            return outputPath;
        }
    }
}

/*
  public static string TestMainSetFunction(string[] fileNames, string folderName)
        {
            //string[] fileNames = new string[] { "а+тел_сочетающиеся.txt", "а_сочетающиеся.txt" };

            string outputPath = folderName + Streamer.CreateFileNameForSet(fileNames);
            StreamWriter stream = new StreamWriter(outputPath, false);

            var set = FindRootIntersection(fileNames);

            Streamer.Print(set, stream);

            return outputPath;
        }

        //Возвращает множество, являющиеся пересечением СТРОК!
        public static IEnumerable<string> FindIntersectionOfIEnumerable(string[] fileNames)
        {
            List<IEnumerable<string>> sets = CreateListOfIEnumerable(fileNames);

            var tmp = sets[0];
            for (int i = 1; i < sets.Count; i++)
            {
                tmp = tmp.Intersect(sets[i]);
            }

            return tmp;
        }


 */