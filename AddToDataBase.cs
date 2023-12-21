using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace ACADCommands
{
    // в классе создать методы которые принимают список
    // и сохраняют в базу данных
    // строка соединения находится в appconfig-нифига 10-12-2023 22:38 воскресенье
    // реализованно с помощью ADO NET
    // добавляем кабельный журнал в базу
    // 1 - имя кабеля; 2 - Откуда; 3 - Куда;
    public class AddToDataBase
    {
        public void metodAddDB(string listStrings)
        {
            string connetionString = null;
            string sql = null;
            string sqlCreateTable = null;
            // строка для соединения с базой данных 
            // для работы Data Source = FISHMAN
            // для дома Data Source = fishman\SQLEXPRESS
            connetionString = @"Data Source = FISHMAN;
                                Initial Catalog = mytest_db;
                                Integrated Security = SSPI;
                                TrustServerCertificate = True";

            //Строка для создания таблицы
            sqlCreateTable = "CREATE TABLE Main_t" +
                " (Id INT PRIMARY KEY IDENTITY," +
                " Cable_f NVARCHAR(100) NOT NULL," +
                " Beg_f NVARCHAR(100) NOT NULL," +
                " End_f NVARCHAR(100) NOT NULL)";
            // запрос на вставку в таблицу данных двух столбцов
            sql = "insert into Main_t ([Cable_f], [Beg_f], [End_f]) values(@cable,@beg,@end)";
            // разделитель по строкам для заполнения списка
            string[] separator = { "\n", "\r" };
            // добавляем данные в список 
            string[] stringFile = listStrings.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            // список для записи подстроки из строки, разделение по точке с запятой
            string[] list;

            // Начинаем соединение и создаём запрос на вставку строк
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                try
                {
                    // открываем соединение 
                    cnn.Open();
                    // создаём таблицу 12-12-2023 13:38
                    using (SqlCommand cmdCreateTable = new SqlCommand(sqlCreateTable, cnn))
                    { cmdCreateTable.ExecuteNonQuery(); }
                    // зазбиваем строку из списка на подстроки
                    foreach (string str in stringFile)
                    {
                        // разделяем строку на подстроки по ";"
                        string[] separatore = { ";", "," };
                        list = (str.Split(separatore, StringSplitOptions.RemoveEmptyEntries));

                        // выполняем команду, записываем строки в базу данных
                        using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        {

                            // получение и установка параметров для передачи в sql запрос
                            cmd.Parameters.Add("@cable", SqlDbType.NVarChar).Value = "55";
                            cmd.Parameters.Add("@beg", SqlDbType.NVarChar).Value = "66";
                            cmd.Parameters.Add("@end", SqlDbType.NVarChar).Value = "44";

                            // Let's ask the db to execute the query
                            // Давайте попросим базу данных выполнить запрос
                            int rowsAdded = cmd.ExecuteNonQuery();
                            if (rowsAdded > 0)
                            { }
                            //MessageBox.Show("Row inserted!!");
                            else
                                // Well this should never really happen
                                // Что ж, на самом деле этого никогда не должно было случиться
                                MessageBox.Show("No row inserted");

                        }
                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    // Мы должны где-то зарегистрировать ошибку,
                    // для этого примера давайте просто покажем сообщение
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }

        }
    }
}