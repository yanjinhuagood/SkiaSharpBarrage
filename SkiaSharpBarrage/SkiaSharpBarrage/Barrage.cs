using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace SkiaSharpBarrage
{
    public class Barrage
    {
        private List<MsgInfo> _MsgInfo;
        private int _num, _index;
        private double _right, _top;
        private float _width, _height;
        private SKTypeface _font;

        public Barrage(SKTypeface font,float width,float height, List<string> strList)
        {
            _width = width;
            _height = height;  
            _font = font;
            _num = (int)height / 40;
            _MsgInfo = new List<MsgInfo>();
            foreach (var item in strList)
            {
                BuildMsgInfo(item);
            }
        }

        void BuildMsgInfo(string text)
        {
            _index++;
            if (_right != 0)
                _width = (float)_right;
            var model = new MsgInfo(text, _font, _width);
            _right = _right == 0 ? _height + model.Width : _right;
            var y = _height - 40;
            _top = _top + 40 >= y ? 40 : _top;
            model.Y = (float)_top;
            _MsgInfo.Add(model);
            _top += 60;
        }
        public void AddBarrage(string text)
        {
            BuildMsgInfo(text);
        }

        public void Render(SKCanvas canvas, SKTypeface font, int width, int height, List<string> strList)
        {
            for (int i = 0; i < _MsgInfo.Count; i++)
            {
                var info = _MsgInfo[i];
                var guid = string.Empty;
                info.Move(out guid);
                if (!string.IsNullOrEmpty(guid))
                {
                    var model = _MsgInfo.FirstOrDefault(x => x.GUID == guid);
                    _MsgInfo.Remove(model);

                }
                canvas.DrawImage(info.SKImage, info.X, info.Y);
            }
           
        }
    }
}
