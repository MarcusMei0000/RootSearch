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

        private static List<IEnumerable<string>> CreateSets(string[] fileNames)
        {
            List<IEnumerable<string>> sets = new List<IEnumerable<string>>();

            foreach (string file in fileNames)
            {
                sets.Add(Streamer.CreateIEnumerableFromFile(file));
            }

            return sets;
        }

        public static IEnumerable<string> FindIntersection(string[] fileNames)
        {
            List<IEnumerable<string>> sets = CreateSets(fileNames);

            var tmp = sets[0];
            for (int i = 1; i < sets.Count; i++)
            {
                tmp = tmp.Intersect(sets[i]);
            }

            return tmp;
        }

        public static string TestMainSetFunction(string[] fileNames, string folderName)
        {
            //string[] fileNames = new string[] { "а+тел_сочетающиеся.txt", "а_сочетающиеся.txt" };

            string outputPath = folderName + Streamer.CreateFileNameForSet(fileNames);
            StreamWriter stream = new StreamWriter(outputPath, false);

            var set = FindIntersection(fileNames);

            Streamer.Print(set, stream);

            return outputPath;
        }
    }
}
