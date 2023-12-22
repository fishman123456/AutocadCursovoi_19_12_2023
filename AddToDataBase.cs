using Autodesk.AutoCAD.DatabaseServices;

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
    // строка соединения находится в файле 21-12-2023 20:23 - четверг
    // реализованно с помощью ADO NET
    // добавляем данные с чертежа  в базу
    // 1 - метка блока, 2 - имя кабеля, 3-4-5 координаты блока x,y,z  6 - слой в котором находится блок;
    public class AddToDataBase
    {
        public void metodAddDB(string listStrings)
        {
            string connetionString = null;
            string sql = null;
            string sqlCreateTable = null;
            string sqlDropTable = null;
            // строка для соединения с базой данных 
            // для работы Data Source = FISHMAN
            // для дома Data Source = fishman\SQLEXPRESS
            connetionString = @"Data Source = FISHMAN;
                                Initial Catalog = AcadBlock_db;
                                Integrated Security = SSPI;
                                TrustServerCertificate = True";
            //Строка для удаления таблицы
            sqlDropTable = "DROP TABLE DataBlock_t";
            //Строка для создания таблицы
            sqlCreateTable = "CREATE TABLE DataBlock_t" +
                " (Id INT PRIMARY KEY IDENTITY," +
                " Handl_f NVARCHAR(100) NOT NULL," +
                " BlockName_f NVARCHAR(100) NOT NULL," +
                " CableName_f NVARCHAR(100) NOT NULL," +
                " BlockX_f NVARCHAR(100) NOT NULL," +
                " BlockY_f NVARCHAR(100) NOT NULL," +
                " BlockZ_f NVARCHAR(100) NOT NULL," +
                " LayerName_f NVARCHAR(100) NOT NULL)";
            // запрос на вставку в таблицу данных двух столбцов
            sql = "insert into DataBlock_t ([Handl_f], [BlockName_f], [CableName_f], [BlockX_f]," +
                " [BlockY_f], [BlockZ_f], [LayerName_f])" +
                " values(@handl_f, @blockName_f, @cableName, @blockX_f," +
                "@blockY_f, @blockZ_f, @layerName_f )";
            // разделитель по строкам для заполнения списка
            string[] separator = { "\n", "\r" };
            // добавляем данные в список 
            string[] stringFile = listStrings.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            // список для записи подстроки из строки, разделение по точке с запятой
            string[] list;
            // удаляем таблицу
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                try
                {
                    MessageBox.Show("Удаляем таблицу");
                    // открываем соединение 
                    cnn.Open();
                    // создаём таблицу 12-12-2023 13:38
                    using (SqlCommand cmdCreateTable = new SqlCommand(sqlDropTable, cnn))
                    { cmdCreateTable.ExecuteNonQuery(); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Начинаем соединение и создаём запрос на вставку строк
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                try
                {
                    MessageBox.Show("Создаём таблицу");
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
                            // @handl_f, @blockName_f, @blockX_f,
                            // @blockY_f], @blockZ_f, @layerName_f
                            // получение и установка параметров для передачи в sql запрос
                            cmd.Parameters.Add("@handl_f", SqlDbType.NVarChar).Value = list[0].ToString();
                            cmd.Parameters.Add("@blockName_f", SqlDbType.NVarChar).Value = list[1].ToString();
                            cmd.Parameters.Add("@cableName", SqlDbType.NVarChar).Value = list[2].ToString();
                            cmd.Parameters.Add("@blockX_f", SqlDbType.NVarChar).Value = list[3].ToString();
                            cmd.Parameters.Add("@blockY_f", SqlDbType.NVarChar).Value = list[4].ToString();
                            cmd.Parameters.Add("@blockZ_f", SqlDbType.NVarChar).Value = list[5].ToString();
                            cmd.Parameters.Add("@layerName_f", SqlDbType.NVarChar).Value = list[6].ToString();

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