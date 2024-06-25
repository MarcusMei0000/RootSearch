using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    public class ExtractRoot
    {
        static StreamReader streamReader;
        const string FILE_PATH = "Words.txt";
        static string folderName = AppDomain.CurrentDomain.BaseDirectory;

        Parser parser;

        public ExtractRoot()
        {
            parser = new Parser(FILE_PATH, folderName);
        }

        public static IEnumerable<string> AllAvailableRootSet()
        {
            List<string> set = new List<string>();

            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            // int count = 0;
            //  && count < 100
            while ((s = streamReader.ReadLine()) != null)
            {
                if (!s.Contains("+"))
                {
                    word = Parser.ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                    set.Add(word.Root);

                    while (remainder != null)
                    {
                        word = Parser.ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                        set.Add(word.Root);
                    }
                }
                //count++;
            }

            set.Sort();
            IEnumerable<string> tmpIEnumerable = set.Distinct();

            return tmpIEnumerable;
        }

        public static void MainExtractRoot()
        {
            streamReader = new StreamReader(Properties.Resources.Words_str, Encoding.Default);
            //streamReader = new StreamReader(FILE_PATH, Encoding.Default);
            StreamWriter testStreamWriter = new StreamWriter(folderName + "\\" + "testRoot", false);

            var set = ExtractRoot.AllAvailableRootSet();
            Streamer.Print(set, testStreamWriter);

            testStreamWriter.Close();
            streamReader.Close();
        }
    }
}
