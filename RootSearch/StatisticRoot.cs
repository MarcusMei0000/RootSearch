﻿using System;
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
    public class StatisticRoot
    {
        static StreamReader streamReader;
        const string FILE_PATH = "Words.txt";
        static string folderName = AppDomain.CurrentDomain.BaseDirectory;

        Parser parser;

        public StatisticRoot()
        {
            parser = new Parser(Properties.Resources.Words_str, folderName);
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

            int kounter = 0;

            while ((s = streamReader.ReadLine()) != null)
            {
                kounter++;
                if (kounter % 20000 == 0)
                {
                    int a=0;
                }
                word = Parser.ParseStringIntoWords(s, out remainder, ref fullWord, ref transcription);
                for (int i = 0; i < roots.Count; i++)
                {
                    if (word.HasRoot(roots[i])) //???сейчас там пока есть приколы с проклитикой и экнклитикой, которые почему-то не игнорируются, а так этой строчки быть не должно
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
                        if (word.HasRoot(roots[i]))//сейчас там пока есть приколы с проклитикой и экнклитикой, которые почему-то не игнорируются, а так этой строчки быть не должно
                        {
                            environments[i].AddEnvironment(word);
                            break;
                        }
                    }
                }
            }

            return environments;
        }

        public static void CreateMainFiles()
        {
            string outputPath = Streamer.CreateFileName(null, null, folderName, "StatisticRoot");

            StreamWriter streamWriter;

            //streamReader = new StreamReader(FILE_PATH, Encoding.Default);
            streamReader = new StreamReader(Properties.Resources.Words_str, Encoding.Default);
            streamWriter = new StreamWriter(outputPath, false);

            List<string> list = Streamer.CreateListFromFile(Properties.Resources.root_str);

           // List<string> list = new List<string> { "б>г", "д>", "берг" };

            List<FullEnvironment> fullEnvironments = ParseFile(list);

            //Для смены вывода Pair, т.е. 1го аффиксального окружения, необходимо убрать true из ToString() в ToStringSet() для FullEnvironment
            foreach (var env in fullEnvironments) {
                env.ToStringSet();
                env.OrderBy();
                streamWriter.WriteLine(env.ToStringFull());
               // streamWriter.WriteLine();
            }

           // Print(fullEnvironments, streamWriter);

            streamWriter.Close();
            streamReader.Close();
        }
    }
}
