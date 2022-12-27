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
        public Image<Rgba32>? Top { get; set; }
        public Image<Rgba32>? Bot { get; set; }
        public Image<Rgba32>? Left { get; set; }
        public Image<Rgba32>? Right { get; set; }
        public Image<Rgba32>? Front { get; set; }
        public Image<Rgba32>? Back { get; set; }

        public ImageProjections()
        {
            Top = null;
            Bot = null;
            Left = null;
            Right = null;
            Front = null;
            Back = null;
        }
        public abstract void ParseAll(Image<Rgba32> image);
        public abstract void ParseTop(Image<Rgba32> image);
        public abstract void ParseBot(Image<Rgba32> image);
        public abstract void ParseLeft(Image<Rgba32> image);
        public abstract void ParseRight(Image<Rgba32> image);
        public abstract void ParseFront(Image<Rgba32> image);
        public abstract void ParseBack(Image<Rgba32> image);

    }
}
