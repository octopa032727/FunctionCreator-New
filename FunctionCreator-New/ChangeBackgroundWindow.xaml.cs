using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using MahApps.Metro.Controls.Dialogs;

namespace FunctionCreator_New
{
    /// <summary>
    /// ChangeBackgroundWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ChangeBackgroundWindow : MetroWindow
    {
        private string directorypath = "FunctionCreator-data";
        private string filepath = @"FunctionCreator-data\backimage.txt";

        public ChangeBackgroundWindow()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            var mainwindow = new MainWindow();
            mainwindow.Show();

            Close();
        }

        private async void btn_selectfile_Click(object sender, RoutedEventArgs e)
        {
            var openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "使用可能な画像ファイル|*.bmp;*.dib;*.jpeg;*.jpg;*.jpe;*.jfif;*.gif;*.tif;*.tiff;*.png;*.ico|ビットマップファイル|*.bmp;*.dib|JPEGファイル|*.jpeg;*.jpg;*.jpe;*.jfif|GIFファイル|*.gif|TIFFファイル|*.tif;*.tiff|PNGファイル|*.png|ICOファイル|*.ico";
            var result = (bool)openfiledialog.ShowDialog();

            if (result)
            {
                try
                {
                    var progress = await this.ShowProgressAsync("設定中", "しばらくお待ちください...");

                    Directory.CreateDirectory(directorypath);
                    File.WriteAllText(filepath, openfiledialog.FileName);

                    var imagebrush = new ImageBrush((ImageSource)new ImageSourceConverter().ConvertFromString(openfiledialog.FileName));
                    imagebrush.Opacity = 0.8;
                    te_image.Background = imagebrush;

                    await progress.CloseAsync();

                    await this.ShowMessageAsync("完了", "バックグラウンド画像の設定が完了しました。");
                }catch(Exception)
                {
                    await this.ShowMessageAsync("エラー", "設定時にエラーが発生しました。\r\n再度、画像ファイルを確認してください。");
                }
            }
        }

        private void tb_url_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_url.Text != string.Empty) btn_download.IsEnabled = true;
            else btn_download.IsEnabled = false;
        }

        private async void btn_download_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var progress = await this.ShowProgressAsync("設定中", "しばらくお待ちください...");

                var request = (HttpWebRequest)WebRequest.Create(tb_url.Text);
                request.UserAgent = "FucntionCreator-New";
                request.Method = "GET";

                var response = (HttpWebResponse)await request.GetResponseAsync();

                Directory.CreateDirectory(directorypath);
                File.WriteAllText(filepath, tb_url.Text);

                var imagebrush = new ImageBrush((ImageSource)new ImageSourceConverter().ConvertFromString(tb_url.Text));
                imagebrush.Opacity = 0.8;
                te_image.Background = imagebrush;

                await progress.CloseAsync();

                await this.ShowMessageAsync("完了", "バックグラウンド画像の設定が完了しました。");
            }catch(Exception)
            {
                await this.ShowMessageAsync("エラー", "設定時にエラーが発生しました。\r\n再度、URLを確認してください。");
            }
        }
    }
}
