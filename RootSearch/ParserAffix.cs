using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Парсер для аффиксов. 
   Нахождение всех (полного) аффиксальных окружений для каждого корня из поданного на вход списка*/
namespace RootSearch
{
    public class ParserAffix
    {
        static StreamReader streamReader;
        const string FILE_PATH = "Words.txt";
        static string folderName = AppDomain.CurrentDomain.BaseDirectory;

        Parser parser;

        public ParserAffix()
        {
            parser = new Parser(FILE_PATH, folderName);
        }

        //можно из List можно сделать Set без повторений и дублей, можно сделать Set с указанием частоты встречаемости для каждого аффиксального окружения
       public static List<FullEnvironment> ParseFile(List<string> roots)
        {
            List<FullEnvironment> environments = new List<FullEnvironment>();
            foreach (var root in roots)
            {
                environments.Add(new FullEnvironment(root));
            }

            string remainder = null, fullWord = null, transcription = null;
            Word word;
            string s;

            while ((s = streamReader.ReadLine()) != null)
            {
                word = Parser.ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                for (int i = 0; i < roots.Count; i++)
                {
                    if (word.HasRoot(roots[i]))
                    {
                        environments[i].AddEnvironment(word);
                        break;
                    }
                }

                while (remainder != null)
                {
                    word = Parser.ParseStringIntoWords(remainder, out remainder, ref fullWord, ref transcription);
                    for (int i = 0; i < roots.Count; i++)
                    {
                        if (word.HasRoot(roots[i]))
                        {
                            environments[i].AddEnvironment(word);
                            break;
                        }
                    }
                }
            }

            return environments;
        }

        public static void CreateMainFiles(List<string> prefixes = null, List<string> suffixies = null)
        {
            string outputPath = Streamer.CreateFileName(prefixes, suffixies, "test", folderName);

            StreamWriter streamWriter;

            streamReader = new StreamReader(FILE_PATH, Encoding.Default);
            streamWriter = new StreamWriter(outputPath, false);

            List<string> roots = new List<string> { "б>г", "д>", "берг" };

            //List<string> roots = new List<string> { "берг" };            

            List<FullEnvironment> fullEnvironments = ParseFile(roots);

            foreach (var env in fullEnvironments) {
                env.ToStringSet();
                streamWriter.WriteLine(env.ToStringWithCount());
            }

           // Print(fullEnvironments, streamWriter);

            streamWriter.Close();
            streamReader.Close();
        }

    }
}
