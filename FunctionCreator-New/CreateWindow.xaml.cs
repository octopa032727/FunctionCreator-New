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
using MahApps.Metro.Controls;

namespace FunctionCreator_New
{
    /// <summary>
    /// CreateWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CreateWindow : MetroWindow
    {
        public CreateWindow()
        {
            InitializeComponent();
        }

        private void tb_funcname_TextChanged(object sender, TextChangedEventArgs e) => CheckEmpty();

        private void tb_args_TextChanged(object sender, TextChangedEventArgs e) => CheckEmpty();

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            var editwindow = new EditWindow();

            editwindow.mode = true;
            editwindow.beforecode = "\t";
            editwindow.funcname = tb_funcname.Text;
            editwindow.args = tb_args.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //関数の情報を出力できるようにする
            editwindow.menu_tools.ToolTip = $"関数名: {editwindow.funcname}\n引数リスト: {string.Join(",", editwindow.args)}";
            editwindow.Show();

            Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            var mainwindow = new MainWindow();
            mainwindow.Show();

            Close();
        }

        //テキストボックスに入力されてるかチェック
        private void CheckEmpty()
        {
            if (tb_funcname.Text != string.Empty && tb_args.Text != string.Empty)
            {
                btn_next.IsEnabled = true;
            }
            else
            {
                btn_next.IsEnabled = false;
            }
        }
    }
}
