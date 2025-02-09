using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_Autorization
{
    internal class Program
    {
        public static bool _passing = false; // Переменная для сообщения серверу о допуске пользователя в чат        
        static void Main(string[] args)
        {
            FSWork.Making_DB(); // Проверка наличия БД с пользователями и созданиё её при отсутствии
            // Пока нет сервера, никнейм и пароль получаем из консоли
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введите никнейм пользователя -> ");
            Console.ForegroundColor = ConsoleColor.White;
            string nickname = Console.ReadLine(); // В эту переменную надо передать никнейм от подключающегося пользователя
            // Проводим проверку наличия пользователя в БД с последующей его регистрацией/авторизацией
            _passing = DBQuerys.PassUser(nickname); // True - допуск пользователя в чат разрешён, false - нет
        }          
    }
}
