﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

/*Описывает все аффиксалльные окружения 1 корня.
 Добавление окружения.
 Стринг для всего листа (с повторениями)
 Стринг для сета: окружение и количество раз которые оно встречается (без повторений)*/
namespace RootSearch
{
    /*Обязательное условие: 
      даже если левая или правая части аффиксального окружения отсутствуют,
      делать пустое присвоение = "";  
      Чтобы оставалась согласованность между левой и правой частью аффиксального окружения.*/

    public class FullEnvironment
    {
        public string root;
        
        public List<Pair> affixEnvironment;

        public Dictionary<string, int> dictionary; //аффиксальное окружение в виже строки и его частота

        public List<string> affixEnviromentString;
        public FullEnvironment() 
        {
            affixEnvironment = new List<Pair>();
        }

        public FullEnvironment(string root)
        {
            this.root = root;
            affixEnvironment = new List<Pair>();
            dictionary = new Dictionary<string, int>();
            affixEnviromentString = new List<string>();
        }

        public void AddEnvironment(Word w)
        {
            affixEnvironment.Add(new Pair(w.prefixes, w.suffixes));
        }

        public void OrderBy()
        {
            List<string> tmp = new List<string>();
            List<string> tmp2 = new List<string>();
            foreach (var env in affixEnvironment)
            {
                string s = env.ToString().Trim(' ');
                if (env.suffixies == null)
                {
                    affixEnviromentString.Add(s);
                }
                else if (env.prefixes == null)
                {
                    tmp.Add(s);
                }
                else { tmp2.Add(s); }
            }
            affixEnviromentString = 
                affixEnviromentString.OrderBy(str => str.Count(f => f == ' ')).ThenBy(str => str)
                .Distinct().ToList();             

            affixEnviromentString.AddRange(tmp.OrderBy(str => str.Count(f => f == ' ')).ThenBy(str => str)
                .Distinct().ToList());

            affixEnviromentString.AddRange(tmp2.OrderBy(str => str.Count(f => f == ' ')).ThenBy(str => str)
                .Distinct().ToList());

        }


        //Для смены вывода Pair, т.е. 1го аффиксального окружения, необходимо убрать true из ToString()
        public void ToStringSet()
        {
            Dictionary<string, int> output = new Dictionary<string, int>();

            foreach(Pair pair in affixEnvironment)
            {
                string tmp = pair.ToString(true);
                int count = 0;
                if (output.TryGetValue(tmp, out count))
                {
                    output[tmp] = ++count;
                }
                else
                {
                    output.Add(tmp, 1);
                }
            }
            dictionary = output.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        //root; количество слов всего; количество окружений всего; их перечисление; dictionary (c/без кол-ва)
        public string ToStringFull()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(root + ";" + affixEnvironment.Count + ";" + dictionary.Count + ";");
            //foreach (var record in dictionary)
            foreach (var record in affixEnviromentString)
            {
                //sb.Append(record.Key);
                sb.Append(record);
                sb.Append(";");
            }
            return sb.ToString();
        }

        public string ToStringWithCount()
        {
            string s = Environment.NewLine + root + "   " + dictionary.Count + "(" + affixEnvironment.Count +")" + Environment.NewLine + "-------------------------" + Environment.NewLine;
            foreach (var record in dictionary)
            {
                s += String.Format("{0,-40} {1, 1}", record.Key, record.Value) + Environment.NewLine;
            }
            s += "-------------------------" + Environment.NewLine;
            return s;
        }
        public string ToStringWithCount2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine + root + "   " + dictionary.Count + "(" + affixEnvironment.Count + ")" + Environment.NewLine + "-------------------------" + Environment.NewLine);
            foreach (var record in dictionary)
            {
                sb.AppendFormat("{0,-40} {1, 1}", record.Key, record.Value);
                sb.Append(Environment.NewLine);
            }
            sb.Append("-------------------------" + Environment.NewLine);
            return sb.ToString();
        }

        public override string ToString()
        {
            string s = Environment.NewLine + root + Environment.NewLine + "-------------------------" + Environment.NewLine;
            for (int i = 0; i < affixEnvironment.Count; i++)
            {
                s += affixEnvironment[i].ToString() + Environment.NewLine;

               // s += "                 " + counts[i];
            }
            s += "-------------------------" + Environment.NewLine;
            return s;
        }

        //override
        public string ToString2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine + root + Environment.NewLine + "-------------------------" + Environment.NewLine);
            for (int i = 0; i < affixEnvironment.Count; i++)
            {
                sb.Append(affixEnvironment[i].ToString());
                sb.Append(Environment.NewLine);
            }
            sb.Append("-------------------------" + Environment.NewLine);
            return sb.ToString();
        }
    }
}
