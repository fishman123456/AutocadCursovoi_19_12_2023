using ACADCommands;
using AutocadCursovoi_19_12_2023;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ACADCommands
{
    public class ClassEntity : DbContext
    {
        // берем строку из app.config    add name="UserDB"
        public ClassEntity() : base("UserDB") { }
        // правильно обьекты нужно сформировать 19-12-2023 
        public DbSet<ClassAttrB> Attrs { get; set; }
    }
}
