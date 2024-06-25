using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    public class Record : IComparable
    {
        Pair pair;
        public List<string> roots;

        public Record(Pair pair)
        {
            this.pair = pair;
            roots = new List<string>();
        }

        public Record()
        {
            pair = new Pair();
            roots = new List<string>();
        }

        public Record(Pair pair, List<string> roots)
        {
            this.pair = pair;
            this.roots = roots;
        }

        public void Add(string str)
        {
            if (!roots.Contains(str))
            {
                roots.Add(str);
            }
        }

        public int Count()
        {
            return roots.Count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(pair.ToString());
            sb.Append(';');
            sb.Append(roots.Count);
            sb.Append(';');
            sb.AppendLine();

            foreach (string root in roots)
            {
                sb.Append(root);
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public string ToString2()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(pair.ToString());
            sb.Append(';');
            sb.Append(roots.Count);
            sb.Append(';');
            sb.AppendLine();
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public int CompareTo(object o)
        {

            if (o is Record record)
            {
                return roots.Count.CompareTo(record.Count());
               
            }
            else throw new ArgumentException("Некорректное значение параметра");
        }

    }
}
