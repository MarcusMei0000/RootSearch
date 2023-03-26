using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    internal class Validator
    {
        
        public string[] PrefixValidate(string s)
        {
            //какие символы допустимы
            //и SetError
            //проверка, что последовательно заполнины textBox
            //new string[] { s }; если ничего нет, то null
            return null;
        }

        public string[] SuffixValidate(string s)
        {
            //какие символы допустим
            //и SetError
            //вызов суффиксов
            //проверка, что последовательно заполнины textBox
            //new string[] { s }; если ничего нет, то null
            return null;
        }

        //если будет подгружаться файл и выпадающий список, то это не нужно
        private bool isPrefixExist(string prefix)
        {
            //залезли в базу проверили
            return true;
            //return false; и errorSet
        }

        //если будет подгружаться файл и выпадающий список, то это не нужно
        private bool isSuffixExist(string prefix)
        {
            //залезли в базу проверили
            return true;
            //return false; и errorSet
        }
    }
}
