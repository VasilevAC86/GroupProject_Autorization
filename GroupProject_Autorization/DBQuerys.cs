using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject_Autorization
{
    internal static class DBQuerys // Класс для запросов в БД
    {
        static public bool PassUser(string nickname) // Метод для допуска пользователя в чат (true - допуск, false - недопуск)
        {
            bool result = false;
            if (DBWork.CheckUser($"SELECT id FROM Users WHERE nickname = '{nickname}';")) // Если пользователь есть в БД            
            {
                result = Autorization(nickname); // Авторизация пользователя
                Console.WriteLine();
            }
            else // Если пользователя нет в БД
            {
                // Тут надо отправить пользователю запрос на регистрацию
                Console.WriteLine($"Пользователя {nickname} ещё нет в базе данных!\nЗарегистрироваться?\n" +
                    $"Нажмите '1' для выхода или любую другую клавишу для регистрации.");
                Console_Text("Ваш выбор -> ", ConsoleColor.Yellow);
                char choice = Console.ReadKey().KeyChar; // В переменную записываем выбор пользователя
                Console.WriteLine();
                if (choice != '1')
                {
                    Console_Text("Введите пароль для регистрации -> ", ConsoleColor.Yellow);
                    string password = Console.ReadLine(); // В эту переменную надо записать пароль от регистрируещегося пользователя
                    DBWork.AddUser($"INSERT INTO Users (nickname, password) VALUES ('{nickname}', '{password.GetHashCode()}');");
                    Console_Text($"Пользователь {nickname} зарегистрирован в системе!", ConsoleColor.Green);
                    Console.WriteLine();
                    result = Autorization(nickname);
                }
            }
            return result;
        }
        static bool Autorization(string nickname) // Процедура авторизации пользователя
        {
            Console.WriteLine($"Пользователь {nickname} есть в базе данных!");
            // Тут надо отправить запрос пользователю на ввод пароля
            Console_Text("Введите пароль -> ", ConsoleColor.Yellow);
            string password = Console.ReadLine(); // В эту переменную надо записать пароль от подключающегося пользователя
            if (DBWork.CheckUser($"SELECT id FROM Users WHERE password = '{password.GetHashCode()}' AND nickname = '{nickname}';"))
            {
                Console_Text("Авторизация завершена успешно!", ConsoleColor.Green);
                Console.WriteLine();
                return true;
            }
            Console_Text("Неверный пароль!", ConsoleColor.Red); // ОБЛОМ, ПОЛЬЗОВАТЕЛЬ НЕ ПОДКЛЮЧИЛСЯ В ЧАТ
            Console.WriteLine();
            return false;
        }
        static void Console_Text(string str, ConsoleColor color) // Процедура консольного общения с пользователем
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
