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
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit;
using MahApps.Metro.Controls;

namespace FunctionCreator_New
{
    /// <summary>
    /// ObfuscateWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ObfuscateWindow : MetroWindow
    {
        public ObfuscateWindow()
        {
            InitializeComponent();
        }

        private void btn_file_Click(object sender, RoutedEventArgs e)
        {
            var openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "JavaScriptファイル(*.js)|*.js";
            var result = (bool)openfiledialog.ShowDialog();

            if (result)
            {
                te_code.Load(openfiledialog.FileName);

                te_obfuscated.Text = Obfuscate_js.Obfuscate(te_code.Text);
            }
        }

        private void btn_obfuscate_Click(object sender, RoutedEventArgs e)
        {
            te_obfuscated.Text = Obfuscate_js.Obfuscate(te_code.Text);
        }

        private void btn_copy_Click(object sender, RoutedEventArgs e)
        {
            te_obfuscated.Copy();
        }

        private void te_code_TextChanged(object sender, EventArgs e)
        {
            var tmp = (TextEditor)sender;

            if (tmp.Text != string.Empty)
            {
                btn_obfuscate.IsEnabled = true;
            }
            else
            {
                btn_obfuscate.IsEnabled = false;
            }
        }
    }
}
