using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace GroupProject_Autorization
{
    internal class DBWork // Класс для работы с БД пользователей
    {
        static private string _dbName = "Users.db"; // Название файла с БД пользователей
        static private string _path = $"Data Source={_dbName};"; // Путь к файлу с БД
        // Метод создания БД в файле _dbName
        static public void MakeDB() 
        {
            string password = "123"; // Пароль для первого тестового пользователя
            string create_table_users = "CREATE TABLE IF NOT EXISTS Users " +
                " (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                " nickname VARCHAR, password VARCHAR);"; // Создаём таблицу в БД
            string init_data_user = "INSERT INTO Users (nickname, password)" +
                $" VALUES ('Ёжик Боря', '{password.GetHashCode()}');"; // Заполняем таблицу тестовыми данными
            SQLiteConnection conn = new SQLiteConnection(_path); // Устанавливаем соединение с БД
            SQLiteCommand cmd_create_table = conn.CreateCommand();
            SQLiteCommand cmd_init_data = conn.CreateCommand();
            cmd_create_table.CommandText = create_table_users;
            cmd_init_data.CommandText = init_data_user; 
            conn.Open();
            cmd_create_table.ExecuteNonQuery();
            cmd_init_data.ExecuteNonQuery();
            conn.Close();
        }
        // Метод проверки наличия пользователя в БД
        static public bool CheckUser(string query)
        {            
            using (SQLiteConnection conn = new SQLiteConnection(_path)) // Подключаемся к БД
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                conn.Open();
                var reader = cmd.ExecuteReader(); // Переменная для хранения результата запроса search_nickname
                if (reader.HasRows) // Если найден пользователь c nickname, то возвращаем true
                    while (reader.Read())
                        if (!reader.IsDBNull(0))
                            return true;
            }
            return false; // Если в БД нет пользователя nickname
        }
        // Метод занесения записи о новом пользователе в БД 
        static public void AddUser(string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_path))
            {
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}