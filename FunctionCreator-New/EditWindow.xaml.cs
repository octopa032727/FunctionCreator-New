using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Search;
using MahApps.Metro.Controls;

namespace FunctionCreator_New
{
    /// <summary>
    /// EditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class EditWindow : MetroWindow
    {
        //入力補完ウィンドウ
        private CompletionWindow completionwindow;
        public string funcname;
        public string[] args;
        public string filename;

        public EditWindow()
        {
            InitializeComponent();

            var options = new TextEditorOptions();
            options.ShowEndOfLine = true;
            options.ShowColumnRuler = true;

            te_code.TextArea.Options = options;

            te_code.TextArea.TextEntered += te_code_TextEntered;
            te_code.TextArea.TextEntering += te_code_TextEntering;
        }

        //新規保存
        private void New_Click(object sender, RoutedEventArgs e)
        {
            var savefiledialog = new SaveFileDialog();
            savefiledialog.Filter = "JavaScriptファイル(*.js)|*.js";
            var result = (bool)savefiledialog.ShowDialog();

            if (result)
            {
                filename = savefiledialog.FileName;
                te_code.Text = $"function {funcname}({string.Join(", ", args)}){{{Environment.NewLine}{te_code.Text}{Environment.NewLine}}}";
                te_code.Save(filename);

                mi_overwrite.IsEnabled = true;

                window_edit.Title = $"Edit: {filename}";
            }
        }

        //上書き
        private void Overwrite_Click(object sender, RoutedEventArgs e) => te_code.Save(filename);

        private void te_code_TextEntered(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text[0] >= 'A' && e.Text[0] <= 'Z') || (e.Text[0] >= 'a' && e.Text[0] <= 'z'))
            {
                if (completionwindow == null)
                {
                    completionwindow = new CompletionWindow(te_code.TextArea);

                    //入力補完対象の文字
                    var data = completionwindow.CompletionList.CompletionData;
                    foreach (MenuItem global in mi_global.Items)
                    {
                        data.Add(new CompletionData(global.Header.ToString())
                        {
                            Content = global.Header,
                            Description = global.ToolTip
                        });
                    }

                    foreach (MenuItem modpe in mi_modpe.Items)
                    {
                        data.Add(new CompletionData($"{mi_modpe.Header}.{modpe.Header}")
                        {
                            Content = $"{mi_modpe.Header}.{modpe.Header}",
                            Description = modpe.ToolTip
                        });
                    }

                    foreach (MenuItem level in mi_level.Items)
                    {
                        data.Add(new CompletionData($"{mi_level.Header}.{level.Header}")
                        {
                            Content = $"{mi_level.Header}.{level.Header}",
                            Description = level.ToolTip
                        });
                    }

                    foreach (MenuItem player in mi_player.Items)
                    {
                        data.Add(new CompletionData($"{mi_player.Header}.{player.Header}")
                        {
                            Content = $"{mi_player.Header}.{player.Header}",
                            Description = player.ToolTip
                        });
                    }

                    foreach (MenuItem entity in mi_entity.Items)
                    {
                        data.Add(new CompletionData($"{mi_entity.Header}.{entity.Header}")
                        {
                            Content = $"{mi_entity.Header}.{entity.Header}",
                            Description = entity.ToolTip
                        });
                    }

                    foreach (MenuItem item in mi_item.Items)
                    {
                        data.Add(new CompletionData($"{mi_item.Header}.{item.Header}")
                        {
                            Content = $"{mi_item.Header}.{item.Header}",
                            Description = item.ToolTip
                        });
                    }

                    foreach (MenuItem block in mi_block.Items)
                    {
                        data.Add(new CompletionData($"{mi_block.Header}.{block.Header}")
                        {
                            Content = $"{mi_block.Header}.{block.Header}",
                            Description = block.ToolTip
                        });
                    }

                    foreach (MenuItem server in mi_server.Items)
                    {
                        data.Add(new CompletionData($"{mi_server.Header}.{server.Header}")
                        {
                            Content = $"{mi_server.Header}.{server.Header}",
                            Description = server.ToolTip
                        });
                    }

                    foreach (MenuItem renderer in mi_renderer.Items)
                    {
                        data.Add(new CompletionData($"{mi_renderer.Header}.{renderer.Header}")
                        {
                            Content = $"{mi_renderer.Header}.{renderer.Header}",
                            Description = renderer.ToolTip
                        });
                    }

                    foreach (MenuItem model in mi_model.Items)
                    {
                        data.Add(new CompletionData($"{mi_model.Header}.{model.Header}")
                        {
                            Content = $"{mi_model.Header}.{model.Header}",
                            Description = model.ToolTip
                        });
                    }

                    foreach (MenuItem modelpart in mi_modelpart.Items)
                    {
                        data.Add(new CompletionData($"{mi_modelpart.Header}.{modelpart.Header}")
                        {
                            Content = $"{mi_modelpart.Header}.{modelpart.Header}",
                            Description = modelpart.ToolTip
                        });
                    }

                    foreach(MenuItem math in mi_math_methods.Items)
                    {
                        data.Add(new CompletionData($"{mi_math.Header}.{math.Header}")
                        {
                            Content = $"{mi_math.Header}.{math.Header}",
                            Description = math.ToolTip
                        });
                    }

                    completionwindow.Show();
                    completionwindow.Closed += delegate
                    {
                        completionwindow = null;
                    };
                }
            }
        }

        private void te_code_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionwindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]) && (e.Text[0] != '(' && e.Text[0] != '"'))
                {
                    if (e.Text[0] == '(' || e.Text[0] == '"')
                    {
                        te_code.TextArea.PerformTextInput(completionwindow.CompletionList.SelectedItem.Text);
                    }
                }
            }
        }

        //Global
        private void Global_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            te_code.TextArea.PerformTextInput($"{tmp.Header}()");
            MoveCaret(-1);
        }

        //ModPE
        private void ModPE_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_modpe.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Level
        private void Level_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_level.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Player
        private void Player_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_player.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Entity
        private void Entity_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_entity.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Item
        private void Item_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_item.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Block
        private void Block_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_block.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Server
        private void Server_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_server.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Renderer
        private void Renderer_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_renderer.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Model
        private void Model_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_model.Header, tmp.Header);

            MoveCaret(-1);
        }

        //ModelPart
        private void ModelPart_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_modelpart.Header, tmp.Header);

            MoveCaret(-1);
        }

        //Math
        private void Math_Click(object sender, RoutedEventArgs e)
        {
            var tmp = (MenuItem)sender;
            AddFunction(mi_math.Header, tmp.Header);

            MoveCaret(-1);
        }

        //コピー
        private void Copy_Click(object sender, RoutedEventArgs e) => te_code.Copy();

        //切り取り
        private void Cut_Click(object sender, RoutedEventArgs e) => te_code.Cut();

        //貼り付け
        private void Paste_Click(object sender, RoutedEventArgs e) => te_code.Paste();

        //クリア
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            te_code.Clear();
            te_code.AppendText("\t");
        }

        //全選択
        private void SelectAll_Click(object sender, RoutedEventArgs e) => te_code.SelectAll();

        private void te_code_KeyDown(object sender, KeyEventArgs e)
        {
            //自動入力 & キャレット
            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                switch (e.Key)
                {
                    case Key.D2:
                        te_code.TextArea.PerformTextInput("\"");
                        MoveCaret(-1);

                        break;
                    case Key.D8:
                        te_code.TextArea.PerformTextInput(")");
                        MoveCaret(-1);

                        break;
                    case Key.OemOpenBrackets:
                        te_code.TextArea.PerformTextInput("}");
                        MoveCaret(-1);

                        break;
                }
            }
        }

        private void te_code_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.OemComma:
                    te_code.TextArea.PerformTextInput(" ");

                    break;
            }
        }

        //エディタへの関数挿入時に少しコードを短く(グローバルは除く)
        private void AddFunction(object classname, object functionname) => te_code.TextArea.PerformTextInput($"{classname}.{functionname}()");

        //キャレット移動
        private void MoveCaret(int move) => te_code.CaretOffset = te_code.SelectionStart + move;
    }
}
