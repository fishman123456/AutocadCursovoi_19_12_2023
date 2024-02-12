using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
// добавлен класс сохранения файла 12-02-2024

namespace AutocadCursovoi_19_12_2023
{
    // при окончании времени работы сохраняем файл в C:\Users\Public\test_Fishman.dwg
    // чтобы не было kill.process
    public class QuickSaveAsDWG
    {
        public void QuickSaveAs()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = doc.Editor;
            Database db = doc.Database;
            // сохраняем в "C:\Users\Fishman\Documents\test.dwg"
            // AC1024 - AutoCAD 2010/2011/2012
            MessageBox.Show("На всякий случай сохраням файл в"+ "\n" + "C:\\Users\\Public\\test_Fishman.dwg");
            db.SaveAs(@"C:\Users\Public\test_Fishman.dwg", DwgVersion.AC1024);
        }
    }
}

//C# library to read/write cad files like dxf/dwg.
////Compatible Dwg/Dxf versions:

////Release 1.1
////    Release 1.2
////    Release 1.4
////    Release 2.0
////    Release 2.10
////    AC1002 - Release 2.5
////    AC1003 - Release 2.6
////    AC1004 - Release 9
////    AC1006 - Release 10
////    AC1009 - Release 11/12 (LT R1/R2)
////    AC1012 - Release 14, 14.01 (LT97/LT98)
////    AC1014 - Release 14, 14.01 (LT97/LT98)
////    AC1015 - AutoCAD 2000/2000i/2002
////    AC1018 - AutoCAD 2004/2005/2006
////    AC1021 - AutoCAD 2007/2008/2009
////    AC1024 - AutoCAD 2010/2011/2012
////    AC1027 - AutoCAD 2013/2014/2015/2016/2017
////    AC1032 - AutoCAD 2018/2019/2020
