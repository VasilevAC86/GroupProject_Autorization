using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_Autorization
{
    internal class FSWork // Класс для работы с файловой системой
    {
        static public bool IsDBExists(string path) // Проверка существования БД с  пользователями
        {
            if (File.Exists(path)) return true;
            return false;
        }
    }
}
