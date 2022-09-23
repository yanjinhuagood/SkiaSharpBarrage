using System;
using SkiaSharp;

namespace SkiaSharpBarrage
{
    /// <summary>
    ///     msg info
    /// </summary>
    public class MsgInfo
    {
        private string _msg;

        public string GUID;

        public MsgInfo(string msg, SKTypeface _font, float windowsWidth)
        {
            _msg = msg;
            var _random = new Random();
            var skColor = new SKColor((byte) _random.Next(1, 255),
                (byte) _random.Next(1, 255), (byte) _random.Next(1, 233));
            using var paint = new SKPaint
            {
                Color = skColor,
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                StrokeWidth = 2
            };
            paint.Shader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(1, 1),
                new[] {SKColors.Transparent, skColor},
                new float[] {0, 1},
                SKShaderTileMode.Repeat);

            using var paintText = new SKPaint
            {
                Color = SKColors.White,
                IsAntialias = true,
                Typeface = _font,
                TextSize = 24
            };
            var textBounds = new SKRect();
            paintText.MeasureText(msg, ref textBounds);
            var width = textBounds.Width + 100;
            SKImage skImage;
            using (var bitmap = new SKBitmap((int) width, 40, true))
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.DrawRoundRect(0, 0, width, 40, 20, 20, paint);
                canvas.DrawText(msg, width / 2 - textBounds.Width / 2, bitmap.Height / 2 + textBounds.Height / 2,
                    paintText);
                var image = SKImage.FromBitmap(bitmap);
                skImage = image;
            }

            SKImage = skImage;
            Width = width;
            X = windowsWidth + Width;
            CanvasWidth = windowsWidth;
            CostTime = TimeSpan.FromMilliseconds(Width);
            GUID = Guid.NewGuid().ToString("N");
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float CanvasWidth { get; set; }
        public SKImage SKImage { get; set; }
        public float MoveNum => CanvasWidth / (float) CostTime.TotalMilliseconds;
        public TimeSpan CostTime { get; set; }

        /// <summary>
        ///     定时调用，移动指定距离
        /// </summary>
        public void Move(out string guid)
        {
            guid = string.Empty;
            X = X - MoveNum;
            if (X <= -Width)
                guid = GUID;
        }
    }
}