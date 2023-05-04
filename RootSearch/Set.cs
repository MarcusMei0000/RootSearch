using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    internal class Set
    {
        public Set() {
        
        }

        private static List<IEnumerable<string>> CreateSets(List<string> fileNames)
        {
            List<IEnumerable<string>> sets = new List<IEnumerable<string>>();

            foreach (string file in fileNames)
            {
                sets.Add(Streamer.CreateIEnumerableFromFile(file));
            }

            return sets;
        }

        public static IEnumerable<string> FindIntersection(List<string> fileNames)
        {
            List<IEnumerable<string>> sets = CreateSets(fileNames);

            var tmp = sets[0];
            for (int i = 1; i < sets.Count; i++)
            {
                tmp = tmp.Intersect(sets[i]);
            }

            return tmp;
        }

        public static void TestMainSetFunction()
        {
            List<string> fileNames = new List<string>() { "а+тел_сочетающиеся.txt", "а_сочетающиеся.txt" };
            
            StreamWriter stream = new StreamWriter("res.txt");

            var set = FindIntersection(fileNames);

            Streamer.Print(set, stream);
        }
    }
}
