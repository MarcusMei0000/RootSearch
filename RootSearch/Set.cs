﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

/*Работа с множествами корней.*/
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

        //Возвращает множество, являющиеся пересечением корней
        public static HashSet<string> FindSetRootIntersection(string[] fileNames)
        {
            List<HashSet<string>> sets = Streamer.CreateListOfRootSets(fileNames);

            IEnumerable<string> tmp = sets[0];
            for (int i = 1; i < sets.Count; i++)
            {
                tmp = tmp.AsEnumerable().Intersect(sets[i]);
            }

            return tmp.ToHashSet();
        }


        //Компоратор строк с учетом регистра внутри
        static int Compare(string l, string r)
        {
            var commonResult = l.Zip(r, Compare).SkipWhile(n => n == 0).FirstOrDefault();
            return (commonResult != 0) ? commonResult : l.Length.CompareTo(r.Length);
        }

        static int Compare(char l, char r)
        {
            var caseResult = char.IsUpper(r).CompareTo(char.IsUpper(l));
            return caseResult != 0 ? caseResult : l.CompareTo(r);
        }


        //TODO: нужен ли нам Set, чтобы строки не повторялись или обойдёмся List(?)
        //Возвращает множество строк с пересекающимися корнями, которое готово для печати в файл
        public static List<string> CreateSetIntersection(string[] fileNames)
        {
            List<string> result = new List<string>();

            var listOfIEnumerable = Streamer.CreateListOfIEnumerable(fileNames);
            var rootSet = FindSetRootIntersection(fileNames);

            foreach (var list in listOfIEnumerable)
            {
                foreach (var str in list)
                {
                    if (Streamer.HasRootInSet(str, rootSet))
                        result.Add(str);
                }
            }
            result.Sort(Compare);

            return result;
        }       

        public static string FindMainSetIntersection(string[] fileNames, string folderName)
        {
            string outputPath = folderName + "\\" + Streamer.CreateFileNameForSet(fileNames);
            StreamWriter stream = new StreamWriter(outputPath, false);
            var set = CreateSetIntersection(fileNames);
            Streamer.Print(set, stream);

            return outputPath;
        }
    }
}