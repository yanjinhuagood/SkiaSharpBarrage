using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace SkiaSharpBarrage
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Barrage _barrage;
        private readonly SKTypeface _font;
        private readonly List<string> list = new List<string>();

        public MainWindow()
        {
            list.Add("WPF开发者");
            list.Add("有志者事竟成也！");
            list.Add("bing");
            list.Add("人无善志，虽勇必伤。");
            list.Add("Microsoft");
            list.Add("只有在人群中间，才能认识自己。");
            list.Add("一思尚存，此志不懈。");
            list.Add("奢侈是舒适的，否则就不是奢侈。");
            list.Add("形成天才的决定因素应该是勤奋。");
            InitializeComponent();
            var index = SKFontManager.Default.FontFamilies.ToList().IndexOf("微软雅黑");
            _font = SKFontManager.Default.GetFontStyles(index).CreateTypeface(0);
            _barrage = new Barrage(_font, (float) Width, (float) Height - (float) MyGrid.ActualHeight, list);
            skElement.PaintSurface += SkElement_PaintSurface;
            Loaded += delegate
            {
                myMediaElement.Source =
                    new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Leagueoflegends.mp4"));
            };
            _ = Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        Dispatcher.Invoke(() => { skElement.InvalidateVisual(); });
                        _ = SpinWait.SpinUntil(() => false, 1000 / 60); //每秒60帧
                    }
                }
                catch (Exception e)
                {
                }
            });
        }


        private void SkElement_PaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear();
            _barrage.Render(canvas, _font, e.Info.Width, e.Info.Height, list);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var _random = new Random();
            var msg = string.Empty;
            if (string.IsNullOrWhiteSpace(tbBarrage.Text))
                msg = list[_random.Next(0, list.Count)];
            else
                msg = tbBarrage.Text;
            _barrage.AddBarrage(msg);
        }
    }
}