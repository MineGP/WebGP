using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGP.Domain.Helpers.MinecraftParser
{
    public struct MinecraftModel
    {
        public ImageProjections Head { get; set; }
        public ImageProjections Body { get; set; }
        public ImageProjections LeftHand { get; set; }
        public ImageProjections RightHand { get; set; }
        public ImageProjections LeftLeg { get; set; }
        public ImageProjections RightLeg { get; set; }
    }
}
