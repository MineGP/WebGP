using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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
        public abstract void ParseAllBase();
        public abstract void ParseTopBase();
        public abstract void ParseBotBase();
        public abstract void ParseLeftBase();
        public abstract void ParseRightBase();
        public abstract void ParseFrontBase();
        public abstract void ParseBackBase();

        public abstract void ParseAll();
        public abstract void ParseTop();
        public abstract void ParseBot();
        public abstract void ParseLeft();
        public abstract void ParseRight();
        public abstract void ParseFront();
        public abstract void ParseBack();

        protected void parseHeadBaseProjection(ref Image<Rgba32> part, int dx, int dy, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    part[i, j] = _image[i + dx, j + dy];
                }
            }
        }
    }
}
