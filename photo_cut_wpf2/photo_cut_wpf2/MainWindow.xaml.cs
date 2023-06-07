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
using System.IO;
using System.Runtime.InteropServices;

namespace photo_cut_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 注册拖放事件
            this.AllowDrop = true;
            this.Drop += new DragEventHandler(MainWindow_Drop);
        }
        private void MainWindow_Drop(object sender, DragEventArgs e)
        {
            // 获取拖放的文件路径
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // 判断是否为 jpg 文件
            if (files.Length > 0 && System.IO.Path.GetExtension(files[0]).ToLower() == ".jpg")
            {
                // 将文件路径记录在一个字符串中
                string filepath = files[0];
                //MessageBox.Show("已选择文件：" + filepath);
                string path=pathinput.Text+"\\";
                string name=idinput.Text;

                int result;

                if (int.TryParse(name, out result))
                {
                    //MessageBox.Show("您输入的整数是：" + result);
                }
                else
                {
                    MessageBox.Show("请输入一个有效的整数作为图片编号！");
                    return;
                }

                if (Directory.Exists(path))
                {
                    //MessageBox.Show("文件夹存在！");
                }
                else
                {
                    MessageBox.Show("文件夹不存在！");
                    return;
                }

                msgoutput.Content = path + name + ".jpg";

                [DllImport("cv_c.dll")]
                static extern void ShowImage(string filepath, string save_path, string save_id);
                ShowImage(filepath,path,name);

                result = result + 1;
                idinput.Text = result.ToString();

            }
            else
            {
                MessageBox.Show("请选择一个 JPG 图片文件！");
            }
        }


    }
}
