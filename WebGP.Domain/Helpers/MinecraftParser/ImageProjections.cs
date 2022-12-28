using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGP.Domain.Helpers.MinecraftParser
{
    public abstract class ImageProjections
    {
        protected Image<Rgba32> _image;

        public Image<Rgba32>? Top { get; set; }
        public Image<Rgba32>? Bot { get; set; }
        public Image<Rgba32>? Left { get; set; }
        public Image<Rgba32>? Right { get; set; }
        public Image<Rgba32>? Front { get; set; }
        public Image<Rgba32>? Back { get; set; }

        public ImageProjections(Image<Rgba32> image)
        {
            _image = image;
            Top = null;
            Bot = null;
            Left = null;
            Right = null;
            Front = null;
            Back = null;
        }
        public abstract void ParseAll();
        public abstract void ParseTop();
        public abstract void ParseBot();
        public abstract void ParseLeft();
        public abstract void ParseRight();
        public abstract void ParseFront();
        public abstract void ParseBack();

    }
}
