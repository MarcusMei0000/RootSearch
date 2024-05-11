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

             int count = 0;
              
            while ((s = streamReader.ReadLine()) != null && count < 1000)
            {
                word = Parser.ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                set.Add(new Pair(word).ToString());


                while (remainder != null)
                {
                    word = Parser.ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    set.Add(new Pair(word).ToString());
                }
                count++;
            }
           // StreamWriter streamWriter = new StreamWriter("!testEnviromentAffix", false);
           // Streamer.Print(set, streamWriter);
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
            streamReader = new StreamReader(FILE_PATH, Encoding.Default);

            return FindAllAvailableAffixEnvironments();
        }

        public static void Main(List<string> prefixes = null, List<string> suffixies = null)
        {
            string outputPath = Streamer.CreateFileName(prefixes, suffixies, folderName, "test");

            StreamWriter streamWriter;

            streamReader = new StreamReader(FILE_PATH, Encoding.Default);
            streamWriter = new StreamWriter(outputPath, false);

            //List<string> roots = new List<string> { "берг" };            

            HashSet<string> affixEnvironments = new HashSet<string>();

            affixEnvironments = FindAllAvailableAffixEnvironments();

            //CreateXLS(affixEnvironments);

            Streamer.Print(affixEnvironments, streamWriter);

            streamWriter.Close();
            streamReader.Close();
        }
    }
}
