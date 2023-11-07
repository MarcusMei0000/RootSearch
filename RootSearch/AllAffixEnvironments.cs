using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void Main(List<string> prefixes = null, List<string> suffixies = null)
        {
            string outputPath = Streamer.CreateFileName(prefixes, suffixies, "test", folderName);

            StreamWriter streamWriter;

            streamReader = new StreamReader(FILE_PATH, Encoding.Default);
            streamWriter = new StreamWriter(outputPath, false);

            //List<string> roots = new List<string> { "берг" };            

            HashSet<string> affixEnvironments = new HashSet<string>();
            affixEnvironments = FindAllAvailableAffixEnvironments();
            
            Streamer.Print(affixEnvironments, streamWriter);

            streamWriter.Close();
            streamReader.Close();
        }
    }
}
