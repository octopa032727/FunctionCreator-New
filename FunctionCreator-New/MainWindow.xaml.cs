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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace FunctionCreator_New
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static readonly string filepath = @"FunctionCreator-data\backimage.txt";

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

        private async void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            var openfiledialog = new OpenFileDialog();
            openfiledialog.Filter = "JavaScriptファイル(*.js)|*.js";
            var result = (bool)openfiledialog.ShowDialog();

            if (result)
            {
                var progress = await this.ShowProgressAsync("読み込み中", "しばらくお待ちください...");

                var editwindow = new EditWindow();

                if (File.Exists(filepath))
                {
                    var imagebrush = new ImageBrush(await GetImage(new Uri(File.ReadAllText(filepath))));
                    progress.SetProgress(0.5);
                    imagebrush.Opacity = 0.8;

                    editwindow.te_code.Background = imagebrush;
                }
                progress.SetProgress(1);
                await progress.CloseAsync();

                editwindow.filename = openfiledialog.FileName;
                editwindow.mode = false;
                editwindow.mi_overwrite.IsEnabled = true;
                editwindow.te_code.Load(openfiledialog.FileName);
                editwindow.beforecode = editwindow.te_code.Text;
                editwindow.Title = $"Edit: {openfiledialog.FileName}";
                editwindow.Show();

                Close();
            }
        }

        private async void btn_obfuscate_Click(object sender, RoutedEventArgs e)
        {
            var progress = await this.ShowProgressAsync("読み込み中", "しばらくお待ちください...");

            var obfuscatewindow = new ObfuscateWindow();

            if (File.Exists(filepath))
            {
                var imagebrush = new ImageBrush(await GetImage(new Uri(File.ReadAllText(filepath))));
                progress.SetProgress(0.5);
                imagebrush.Opacity = 0.8;

                obfuscatewindow.te_code.Background = imagebrush;
                obfuscatewindow.te_obfuscated.Background = imagebrush;
            }
            progress.SetProgress(1);
            await progress.CloseAsync();

            obfuscatewindow.Show();

            Close();
        }

        private async void btn_changeback_Click(object sender, RoutedEventArgs e)
        {
            var progress = await this.ShowProgressAsync("読み込み中", "しばらくお待ちください...");

            var changebackgroundwindow = new ChangeBackgroundWindow();

            if (File.Exists(filepath))
            {
                var imagebrush = new ImageBrush(await GetImage(new Uri(File.ReadAllText(filepath))));
                progress.SetProgress(0.5);
                imagebrush.Opacity = 0.8;

                changebackgroundwindow.te_image.Background = imagebrush;
            }
            progress.SetProgress(1);
            await progress.CloseAsync();

            changebackgroundwindow.Show();

            Close();
        }

        //画像をダウンロード(非同期)
        public static Task<BitmapImage> GetImage(Uri uri)
        {
            return Task.Run(() =>
            {
                var client = new WebClient();
                try
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(client.DownloadData(uri));
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
                catch (Exception) { }
                finally
                {
                    client.Dispose();
                }

                return null;
            });
        }
    }
}
