using System.Windows;


namespace ACADCommands
{
    public class TestEntity
    {

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
