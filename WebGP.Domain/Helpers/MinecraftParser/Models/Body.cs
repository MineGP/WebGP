using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace WebGP.Domain.Helpers.MinecraftParser.Models
{
    public class Body : ImageProjections
    {
        private const int HEIGHT = 12;
        private const int WIDTH = 4;
        private const int LENGHT = 8;

        public Body(Image<Rgba32> image) 
            : base(image)
        {
        }

        public override void ParseAll()
        {
            throw new NotImplementedException();
        }

        public override void ParseAllBase()
        {
            throw new NotImplementedException();
        }

        public override void ParseBack()
        {
            throw new NotImplementedException();
        }

        public override void ParseBackBase()
        {
            var tmp = new Image<Rgba32>(LENGHT, HEIGHT);
            parseHeadBaseProjection(ref tmp, 32, 20, LENGHT, HEIGHT);
            Back = tmp;
        }

        public override void ParseBot()
        {
            throw new NotImplementedException();
        }

        public override void ParseBotBase()
        {
            var tmp = new Image<Rgba32>(LENGHT, WIDTH);
            parseHeadBaseProjection(ref tmp, 28, 16, LENGHT, WIDTH);
            Bot = tmp;
        }

        public override void ParseFront()
        {
            throw new NotImplementedException();
        }

        public override void ParseFrontBase()
        {
            var tmp = new Image<Rgba32>(LENGHT, HEIGHT);
            parseHeadBaseProjection(ref tmp, 20, 20, LENGHT, HEIGHT);
            Front = tmp;
        }

        public override void ParseLeft()
        {
            throw new NotImplementedException();
        }

        public override void ParseLeftBase()
        {
            var tmp = new Image<Rgba32>(WIDTH, HEIGHT);
            parseHeadBaseProjection(ref tmp, 28, 20, WIDTH, HEIGHT);
            Left = tmp;
        }

        public override void ParseRight()
        {
            throw new NotImplementedException();
        }

        public override void ParseRightBase()
        {
            var tmp = new Image<Rgba32>(WIDTH, HEIGHT);
            parseHeadBaseProjection(ref tmp, 16, 20, WIDTH, HEIGHT);
            Right = tmp;
        }

        public override void ParseTop()
        {
            throw new NotImplementedException();
        }

        public override void ParseTopBase()
        {
            var tmp = new Image<Rgba32>(LENGHT, WIDTH);
            parseHeadBaseProjection(ref tmp, 20, 16, LENGHT, WIDTH);
            Top = tmp;
        }
    }
}
