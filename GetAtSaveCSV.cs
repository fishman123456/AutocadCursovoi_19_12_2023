using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


[assembly: CommandClass(typeof(ACADCommands.GetAtSaveCSV))]

namespace ACADCommands
{
    public class GetAtSaveCSV
    {
        public static StringBuilder stringBuilder { get; set; } = new StringBuilder();

        // аттрибут для запуска метода считывания атрибутов и координат блока
        [CommandMethod("CSV")]
        public static void ListAttrSaveCSV()
        {
            // проверка по текущей дате
            CheckDateWork.CheckDate();
            // строка для сохранения в csv
            //StringBuilder stringBuilder = new StringBuilder();
            // Открываем для редактирования документ
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            // Открываем базу данных чертежа
            Database db = HostApplicationServices.WorkingDatabase;
            // строка соединения с базой данных
            ed.WriteMessage("\n\nстрока соединения с базой данных\n" +
                "Data Source = Имя сервера;\n" +
                "Initial Catalog = AcadBlock_db;\n" +
                "Integrated Security = SSPI;\n" +
                "TrustServerCertificate = True\n\n");
            Transaction tr = db.TransactionManager.StartTransaction();
            // Start the transaction
            try
            {
                // Build a filter list so that only
                // block references are selected
                TypedValue[] filList = new TypedValue[1] { new TypedValue((int)DxfCode.Start, "INSERT") };
                SelectionFilter filter = new SelectionFilter(filList);
                PromptSelectionOptions opts = new PromptSelectionOptions();
                opts.MessageForAdding = "Выберите блоки: ";
                PromptSelectionResult res = ed.GetSelection(opts, filter);
                // Do nothing if selection is unsuccessful
                if (res.Status != PromptStatus.OK)
                    return;
                SelectionSet selSet = res.Value;
                // добавляем в массив выбранные обьекты
                ObjectId[] idArray = selSet.GetObjectIds();
                // переменная для ко-ва блоков
                int countBlock = 0;
                // перебираем блоки
                foreach (ObjectId blkId in idArray)
                {
                    
                    // ссылки на блоки
                    BlockReference blkRef = (BlockReference)tr.GetObject(blkId, OpenMode.ForRead);
                    // ссылка на таблицу блоков
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(blkRef.BlockTableRecord, OpenMode.ForRead);
                    // берем коллекцию атрибутов по ссылкам на блоки
                    AttributeCollection attCol = blkRef.AttributeCollection;
                    
                    // перебираем аттрибуты
                    foreach (ObjectId blkAttId in attCol)
                    // btr.Dispose();
                    {
                        AttributeReference attRef = (AttributeReference)tr.GetObject(blkAttId, OpenMode.ForRead);
                        // btr.Dispose();
                       
                        //  выводим координаты блока,слой и handle
                        if (attRef.Tag == "ОБОЗНАЧ_КАБЕЛЯ" && attRef.TextString != "")
                        {
                            countBlock++;
                            string str = ("\n" +
                                           //"Handle BlockRef : " + 
                                           blkRef.Handle.ToString() + ";" + // вот нужная фигня - Handle
                                                                            //"Block name: " + 
                                           btr.Name + ";" +
                                           //"Attribute String: " + 
                                           attRef.TextString + ";" +
                                           //"X: " + 
                                           blkRef.Position.X.ToString() + ";" +
                                           //"Y: " + 
                                           blkRef.Position.Y.ToString() + ";" +
                                           //"Z: " + 
                                           blkRef.Position.Z.ToString() + ";" +
                                           //"Layer: " + 
                                           blkRef.Layer.ToString());
                            stringBuilder.Append(str);
                            ed.WriteMessage(str);
                        }
                    }
                }
                tr.Commit();
                ed.WriteMessage($"\nКол-во блоков с аттрибутом = {countBlock}\n{DateTime.Now.ToString()}\n");
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                ed.WriteMessage(("Exception: " + ex.Message));
            }
            finally
            {
                tr.Dispose();
                // запишем в файл
                // диалог с вызовом сохранения в *.csv
                #region
                string sMessageBoxTextc = "Сохранить в базу данных ?";
                string sCaptionc = "Используйте если у вас установлен microsoft sql server";
                MessageBoxButton btnMessageBoxc = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBoxc = MessageBoxImage.Warning;
                MessageBoxResult rsltMessageBoxc = MessageBox.Show(sMessageBoxTextc, sCaptionc, btnMessageBoxc, icnMessageBoxc);
                switch (rsltMessageBoxc)
                {
                    case MessageBoxResult.Yes:
                        SaveCSV saveFileCSV = new SaveCSV();
                        saveFileCSV.saveCSV(stringBuilder.ToString());
                        break;
                    case MessageBoxResult.No:
                        /* ... */
                        break;
                    case MessageBoxResult.Cancel:
                        /* ... */
                        break;
                }
                #endregion
               
                // диалог с вызовом сохранения в базу данных
                #region
                string sMessageBoxText = "Сохранить в базу данных ?";
                string sCaption = "Используйте если у вас установлен microsoft sql server";
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        AddToDataBase addToDataBase = new AddToDataBase();
                        addToDataBase.metodAddDB(stringBuilder.ToString());
                        break;
                    case MessageBoxResult.No:
                        /* ... */
                        break;
                    case MessageBoxResult.Cancel:
                        /* ... */
                        break;
                }
                #endregion
            }
        }
    }
}

