using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace WebGP.Domain.Helpers.MinecraftParser.Models
{
    public class Head : ImageProjections
    {
        private const int SIDE_SIZE = 8;

        public Head(Image<Rgba32> image)
            : base (image)
        {
        }

        public override void ParseAll()
        {
            ParseBack();
            ParseFront();
            ParseLeft();
            ParseRight();
            ParseTop();
            ParseBot();
        }

        public override void ParseBack()
        {
            var tmp = new Image<Rgba32>(SIDE_SIZE, SIDE_SIZE);
            parseHeadProjection(ref tmp, SIDE_SIZE * 3, SIDE_SIZE);
            Back = tmp;
        }
        public override void ParseBot()
        {
            var tmp = new Image<Rgba32>(SIDE_SIZE, SIDE_SIZE);
            parseHeadProjection(ref tmp, SIDE_SIZE * 2, 0);
            Bot = tmp;
        }
        public override void ParseFront()
        {
            var tmp = new Image<Rgba32>(SIDE_SIZE, SIDE_SIZE);
            parseHeadProjection(ref tmp, SIDE_SIZE, SIDE_SIZE);
            Front = tmp;
        }
        public override void ParseLeft()
        {
            var tmp = new Image<Rgba32>(SIDE_SIZE, SIDE_SIZE);
            parseHeadProjection(ref tmp, SIDE_SIZE * 2, SIDE_SIZE);
            Left = tmp;
        }
        public override void ParseRight()
        {
            var tmp = new Image<Rgba32>(SIDE_SIZE, SIDE_SIZE);
            parseHeadProjection(ref tmp, 0, SIDE_SIZE);
            Right = tmp;
        }
        public override void ParseTop()
        {
            var tmp = new Image<Rgba32>(SIDE_SIZE, SIDE_SIZE);
            parseHeadProjection(ref tmp, SIDE_SIZE, 0);
            Top = tmp;
        }

        private void parseHeadProjection(ref Image<Rgba32> part, int dx, int dy)
        {
            for (int i = 0; i < SIDE_SIZE; i++)
            {
                for (int j = 0; j < SIDE_SIZE; j++)
                {
                    part[i, j] = _image[i + dx, j + dy];
                }
            }
        }
    }
}
