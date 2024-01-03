using Autodesk.AutoCAD.DatabaseServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutocadCursovoi_19_12_2023
{
    /// <summary>
    /// Логика взаимодействия для WPF.xaml
    /// </summary>
    public partial class WPF : Window
    {
        public List<string> stringsdata;
        public WPF(List<string> strings)
        {
            InitializeComponent();
            stringsdata = strings;
        }
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> strings;
            
            foreach (string s in stringsdata)
            {
                grid.Text += s;
            }
        }
        private void grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ////stringsdata path = grid.SelectedItem as MyTable;
            //MessageBox.Show(" ID: " + path.Id + "\n Исполнитель: " + path.Vocalist + "\n Альбом: " + path.Album
            //    + "\n Год: " + path.Year);
        }
    }
}
