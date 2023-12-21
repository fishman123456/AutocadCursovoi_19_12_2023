using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


// строка соединения в App.config
//< connectionStrings >
//  < add name = "classDbContext"               public class classDbContext : DbContext
//  connectionString = "Data Source = fishman\SQLEXPRESS;    сущ подключение
//  Initial Catalog = CodeFirst;                             Имя базы данных
//  Integrated Security = SSPI"                              По умолчанию
//  providerName="System.Data.SqlClient"/>                   По умолчанию
//	</connectionStrings>

// консоль Диспетчера пакетов
// Создаём таблицу с полями
// 1) PM> enable-migrations                                  Создаём миграцию
// 2) PM> add-migration CreateClassAttrB                     Имя таблицы
// 3) PM> update-database                                    Обновляем базу данных

namespace ACADCommands
{
    public class ClassAttrB
    {
        public int id { get; set; }
        public string BlockHandle { get; set; } = "BlockHandle_f";
        public string BlockName { get; set; } = "BlockName_f";
        public string BlockAttrVal { get; set; } = "BlockAttrVal_f";
        public string BlockX { get; set; } = "BlockX_f";
        public string BlockY { get; set; } = "BlockY_f";
        public string BlockZ { get; set; } = "BlockZ_f";
        public string BlockLayer { get; set; } = "BlockLayer_f";
        public ClassAttrB() { }
        public ClassAttrB( string blockHandle, string blockName, 
                           string blockAttrVal, string blockX, 
                           string blockY, 
                           string blockZ, string blockLayer)
        {
            BlockHandle = blockHandle;
            BlockName = blockName;
            BlockAttrVal = blockAttrVal;
            BlockX = blockX;
            BlockY = blockY;
            BlockZ = blockZ;
            BlockLayer = blockLayer;
        }
        public void ent()
        {
            using (var ctx = new classDbContext())
            {
                var stud = new ClassAttrB("1", "2", "3", "4", "5", "6", "8");
                ctx.classAttrBs.Add(stud);
                ctx.SaveChanges();
            }
        }
    }
    public class classDbContext : DbContext
    {
        public DbSet<ClassAttrB> classAttrBs {get;set; }
    }
    // для теста в консольном приложении
    //    internal class EntityAddObject
    //{
    //    //static void Main(string[] args)
    //    //{
    //    //    Console.WriteLine("База создана");
    //    //    using (var ctx = new classDbContext())
    //    //    {
    //    //        var stud = new ClassAttrB("1","2","3","4","5","6","7");
    //    //        ctx.classAttrBs.Add(stud);
    //    //        ctx.SaveChanges();
    //    //    }
    //    //    Console.WriteLine("Запись добавлена");
    //    //    Console.ReadKey();
    //    //}
    //}
}
