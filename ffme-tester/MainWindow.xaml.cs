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

namespace ffme_tester
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        public MainWindow()
        {
            // Initialize FFME
            var ffmpegPath = System.IO.Path.Combine(AppPath, "share/ffmpeg");
            if (!System.IO.File.Exists(System.IO.Path.Combine(ffmpegPath, "ffmpeg.exe")) ||
                !System.IO.File.Exists(System.IO.Path.Combine(ffmpegPath, "ffplay.exe")) ||
                !System.IO.File.Exists(System.IO.Path.Combine(ffmpegPath, "ffprobe.exe"))
               )
            {
                MessageBox.Show("缺少ffmpeg相关的可执行文件，请重新解压/安装本程序。\n遇到无法继续的错误，程序将会退出。", "初始化失败", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            Unosquare.FFME.MediaElement.FFmpegDirectory = ffmpegPath;

            InitializeComponent();
        }

        /// <summary>
        /// Handles the SizeChanged event of the PlayerWrapGrid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SizeChangedEventArgs"/> instance containing the event data.</param>
        private void PlayerWrapGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = sender as Grid;
            var width = grid.ActualWidth;
            var height = grid.ActualHeight - 46;
            if(width / 16 > height / 9)
            {
                var towidth = height / 9 * 16;
                Player.Height = height;
                Player.Width = towidth;
            }
            else
            {
                var toheight = width / 16 * 9;
                Player.Height = toheight;
                Player.Width = width;
            }
        }
    }
}
