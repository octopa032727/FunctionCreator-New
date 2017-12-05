using Microsoft.Win32;
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
using MahApps.Metro.Controls;

namespace FunctionCreator_New
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            var createwindow = new CreateWindow();
            createwindow.Show();

            Close();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "JavaScriptファイル(*.js)|*.js";
            var result = (bool)openfiledialog.ShowDialog();

            if (result)
            {
                var editwindow = new EditWindow();
                editwindow.mi_overwrite.IsEnabled = true;
                editwindow.te_code.Load(openfiledialog.FileName);
                editwindow.Title = $"Edit: {openfiledialog.FileName}";
                editwindow.Show();

                Close();
            }
        }
    }
}
