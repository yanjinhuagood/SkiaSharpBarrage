using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SkiaSharpBarrage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private SKTypeface _font;
        private Barrage _barrage;
        private  List<string> list = new List<string>();
        public MainWindow()
        {
            list.Add("2333");
            list.Add("测试弹幕公众号：WPF开发者");
            list.Add("很难开心");
            list.Add("LOL~");
            list.Add("青春记忆");
            list.Add("bing");
            list.Add("Microsoft");
            InitializeComponent();
            var index = SKFontManager.Default.FontFamilies.ToList().IndexOf("微软雅黑");
            _font = SKFontManager.Default.GetFontStyles(index).CreateTypeface(0);
            _barrage = new Barrage(_font,(float)Width, (float)Height - (float)MyGrid.ActualHeight, list);
            skElement.PaintSurface += SkElement_PaintSurface;
            Loaded += delegate
            {
                myMediaElement.Source = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Leagueoflegends.mp4"));
            };
            _ = Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            skElement.InvalidateVisual();
                        });
                        _ = SpinWait.SpinUntil(() => false, 1000 / 60);//每秒60帧
                    }
                }
                catch (Exception e)
                {
                }
            });
        }



        private void SkElement_PaintSurface(object? sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();
            _barrage.Render(canvas, _font, e.Info.Width, e.Info.Height, list);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _barrage.AddBarrage(tbBarrage.Text);
        }
    }
}
