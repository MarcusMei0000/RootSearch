using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using static RootSearch.Pair;

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
            // 
            while ((s = streamReader.ReadLine()) != null && count < 100)
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

            return set;
        }

        public static void CreateXLS(HashSet<string> set)
        {
            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;
  
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
        }


        public static void Main(List<string> prefixes = null, List<string> suffixies = null)
        {
            string outputPath = Streamer.CreateFileName(prefixes, suffixies, "test", folderName);

            StreamWriter streamWriter;

            streamReader = new StreamReader(FILE_PATH, Encoding.Default);
            streamWriter = new StreamWriter(outputPath, false);

            //List<string> roots = new List<string> { "берг" };            

            HashSet<string> affixEnvironments = new HashSet<string>();


            //97563;ПОДДЕЛЫВАТЕЛЬ;подъ-д>=л=ыва=тел_ь;89;122;132;53;54;1652;2507; будем чинить такие вот разрывы суффиксов
            affixEnvironments = FindAllAvailableAffixEnvironments();

            //CreateXLS(affixEnvironments);

            Streamer.Print(affixEnvironments, streamWriter);

            streamWriter.Close();
            streamReader.Close();
        }
    }
}
