﻿using System;
using System.IO;
using System.Windows.Forms;

namespace ACADCommands
{
    public class SaveCSV
    {
        public async void saveCSV(string text)
        {
            CheckDateWork.CheckDate();
            // открываем диалог для сохранения файла в поток
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.csv)|*.csv|All files(*.*)|*.*";
            // если не сохраняем то выходим
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string path = saveFileDialog1.FileName;
            // сохраняем текст в файл
            try
            {
            // полная перезапись файла 
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                    // асинхронная перезапись  файла
                await writer.WriteAsync(text);
            }
                // добавление в файл
                //using (StreamWriter writer = new StreamWriter(path, true))
                //{
                //    await writer.WriteLineAsync("Addition");
                //    await writer.WriteAsync("4,5");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

