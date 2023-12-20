using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootSearch
{
    /*
     Префиксы – Позиция корня – суффиксы – (флексия1;флексия1 с энклитикой; флексия2…) – количество корней с таким окружением – список корней с таким окружением. 
     Мой вариант: Корень;префиксы (через пробел);суффиксы (через пробел);кол-во слов с таким окружением 
     */
    public class StatisticsAffixEnvironment
    {
        static StreamReader streamReader;
        const string FILE_PATH = "Words.txt";
        static string folderName = AppDomain.CurrentDomain.BaseDirectory;
    }
}
