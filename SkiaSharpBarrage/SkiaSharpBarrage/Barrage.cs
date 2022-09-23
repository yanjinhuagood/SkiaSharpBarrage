using System.Collections.Generic;
using System.Linq;
using SkiaSharp;

namespace SkiaSharpBarrage
{
    public class Barrage
    {
        private readonly SKTypeface _font;
        private readonly List<MsgInfo> _MsgInfo;
        private int _num, _index;
        private double _right, _top;
        private float _width;
        private readonly float _height;

        public Barrage(SKTypeface font, float width, float height, List<string> strList)
        {
            _width = width;
            _height = height;
            _font = font;
            _num = (int) height / 40;
            _MsgInfo = new List<MsgInfo>();
            foreach (var item in strList) BuildMsgInfo(item);
        }

        private void BuildMsgInfo(string text)
        {
            _index++;
            if (_right != 0)
                _width = (float) _right;
            var model = new MsgInfo(text, _font, _width);
            _right = _right == 0 ? _height + model.Width : _right;
            var y = _height - 40;
            _top = _top + 40 >= y ? 40 : _top;
            model.Y = (float) _top;
            _MsgInfo.Add(model);
            _top += 60;
        }

        public void AddBarrage(string text)
        {
            BuildMsgInfo(text);
        }

        public void Render(SKCanvas canvas, SKTypeface font, int width, int height, List<string> strList)
        {
            for (var i = 0; i < _MsgInfo.Count; i++)
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