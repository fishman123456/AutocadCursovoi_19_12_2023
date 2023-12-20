using System.Data.Entity;

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
