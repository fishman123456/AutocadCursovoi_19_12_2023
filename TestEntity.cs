using AutocadCursovoi_19_12_2023;

using Autodesk.AutoCAD.Runtime;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

[assembly: CommandClass(typeof(ACADCommands.TestEntity))]

namespace ACADCommands
{
    public class TestEntity
    {
        [CommandMethod("ListENTITYtest")]

        public void entFirst()
        {
            MessageBox.Show("работаем");

            using (ClassEntity db = new ClassEntity())
            {
                ClassAttrB classAttrObj = new ClassAttrB();
                ClassAttrB classAttrObj1 = new ClassAttrB();
                db.Attrs.Add(classAttrObj);
                db.Attrs.Add(classAttrObj1);
                db.SaveChanges();
            }



        }
        
    }
}
