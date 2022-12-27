using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGP.Domain.Helpers.MinecraftParser.Models
{
    public class Head : ImageProjections
    {
        public override void ParseAll(Image<Rgba32> image)
        {
            ParseBack(image);
            ParseFront(image);
            ParseLeft(image);
            ParseRight(image);
            ParseTop(image);
            ParseBot(image);
        }

        public override void ParseBack(Image<Rgba32> image)
        {
            throw new NotImplementedException();
        }

        public override void ParseBot(Image<Rgba32> image)
        {
            throw new NotImplementedException();
        }

        public override void ParseFront(Image<Rgba32> image)
        {
            throw new NotImplementedException();
        }

        public override void ParseLeft(Image<Rgba32> image)
        {
            throw new NotImplementedException();
        }

        public override void ParseRight(Image<Rgba32> image)
        {
            throw new NotImplementedException();
        }

        public override void ParseTop(Image<Rgba32> image)
        {
            throw new NotImplementedException();
        }
    }
}
