using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using static RootSearch.Pair;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/* Нахождение всех аффиксальных окружений встречающихся в словаре, 
   вывод в текстовый файл и в эксель таблицу*/
namespace RootSearch
{
    public class AllAffixEnvironments
    {
        static StreamReader streamReader;
        const string FILE_PATH = "Words.txt";
        static string folderName = AppDomain.CurrentDomain.BaseDirectory;

        Parser parser;

        public AllAffixEnvironments()
        {
            parser = new Parser(FILE_PATH, folderName);
        }

        private static HashSet<string> FindAllAvailableAffixEnvironments()
        {
            HashSet<string> set = new HashSet<string>();

            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                word = Parser.ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                set.Add(new Pair(word).ToString());

                while (remainder != null)
                {
                    word = Parser.ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    set.Add(new Pair(word).ToString());
                }
            }
            return set;
        }
        /*
        public void test()
        {
            var bound = 10000;
            sw.Start();
            Excel.Range r = sheet.Range[sheet.Cells[2, 1], sheet.Cells[bound, 1]];
            var arr = new int[bound, 1];
            for (var i = 1; i <= bound; i++) arr[i - 1, 0] = i;
            r.Value = arr;
            sw.Stop();

            Console.WriteLine(sw.Elapsed);
            sw.Reset();
            sw.Start();
            for (var i = 1; i <= bound; i++) sheet.Cells[i, 1].Value = i;
            sw.Stop();

            Console.WriteLine(sw.Elapsed);
        }*/

        public static void CreateXLS(HashSet<string> set)
        {
            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            excelApp.ScreenUpdating = false;
            excelApp.Visible = false;
            excelApp.Interactive = false;



            /*
            int i = 1;
            for (int k = 4; k <= 1; k--)
            {
                workSheet.Cells[i, k] = "приставка" + k;
              //  workSheet.Cells[i, k].Interior.Color = 200;
              //  workSheet.Cells[i, k].Offset[0, 1].Interior.Color = 200;
            }

            workSheet.Cells[i, 5] = "";

            for (int k = 6; k <= 4; k++)
            {
                workSheet.Cells[i, k] = "суф" + (k-5);
            }
            */

            int i = 2;
            int j = 1;
            foreach (string elem in set)
            {
                Pair pair = FromString(elem);
                foreach (string pref in pair.prefixes)
                {
                    workSheet.Cells[i, j] = pref;
                    ++j;
                }

                workSheet.Cells[i, j] = "";
                ++j;

                foreach (string suf in pair.suffixies)
                {
                    workSheet.Cells[i, j] = suf;
                    ++j;
                }
                j = 1;
                ++i;
            }
            excelApp.Visible = true;
            excelApp.Interactive = true;
            excelApp.ScreenUpdating = true;
        }

        public static HashSet<string> Test(List<string> prefixes = null, List<string> suffixies = null)
        {
            streamReader = new StreamReader(Properties.Resources.Words_str, Encoding.Default);

            return FindAllAvailableAffixEnvironments();
        }

        private static List<Pair> PrepareAllPairs()
        {
            streamReader = new StreamReader(Properties.Resources.allAffixEnviroment_str, Encoding.Default);
            List<string> allAffixEnviroments = Streamer.CreateListFromFile(Properties.Resources.allAffixEnviroment_str);
            List<Pair> allPairs = new List<Pair>();

            foreach (string env in allAffixEnviroments)
            {
                allPairs.Add(FromStringResized(env));
            }

            return allPairs;
        }

        static public Record[] FindAffixStatistics()
        {
            List<Pair> allPairs = PrepareAllPairs();
            Record[] pairRootCollection = new Record[allPairs.Count];

            for (int i = 0; i < allPairs.Count; i++)
            {
                pairRootCollection[i] = new Record(allPairs[i]);
            }

            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;
            bool IsClassified = false;

            //int count = 0;
            //int limit = 100000;

            streamReader = new StreamReader(Properties.Resources.Words_str, Encoding.Default);
            //&& count < limit
            while ((s = streamReader.ReadLine()) != null)
            {
                //count++;
                if (!s.Contains("+"))
                {
                    word = Parser.ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                    int i = 0;
                    IsClassified = false;
                    while (i < allPairs.Count && IsClassified == false) {
                        if (word.IsClassifiedStrict(allPairs[i]))
                        {
                            pairRootCollection[i].Add(word.Root);
                            IsClassified = true;
                        }
                        i++;
                    }

                    while (remainder != null)
                    {
                        word = Parser.ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                        int j = 0;
                        IsClassified = false;
                        while (j < allPairs.Count && IsClassified == false)
                        {
                            if (word.IsClassifiedStrict(allPairs[j]))
                            {
                                pairRootCollection[j].Add(word.Root);
                                IsClassified = true;
                            }
                            j++;
                        }
                    }
                }
            }

            return pairRootCollection;
        }

        public static void TestAffixEnviromentsStatistics()
        {
            StreamWriter streamWriter = new StreamWriter("TestAffixEnviromentsStatistics.txt", false);
            Record[] res = FindAffixStatistics();
            var result = res.OrderByDescending(record => record.Count());
            //Array.Sort(res);
            //res.Reverse();

            List<string> output = new List<string>();

            foreach(var record in result)
            {
                output.Add(record.ToString2());
            }

            Streamer.Print(output, streamWriter);

            streamWriter.Close();
            streamReader.Close();
        }

        public static void Main(List<string> prefixes = null, List<string> suffixies = null)
        {
            string outputPath = Streamer.CreateFileName(prefixes, suffixies, folderName, "ВСЕ ОКРУЖЕНИЯЮ.txt");

            StreamWriter streamWriter;

            //streamReader = new StreamReader(FILE_PATH, Encoding.Default);
            streamReader = new StreamReader(Properties.Resources.Words_str, Encoding.Default);
            streamWriter = new StreamWriter(outputPath, false);

            HashSet<string> affixEnvironments = new HashSet<string>();

            affixEnvironments = FindAllAvailableAffixEnvironments();

            //CreateXLS(affixEnvironments);

            Streamer.Print(affixEnvironments, streamWriter);

            streamWriter.Close();
            streamReader.Close();            
        }
    }
}
