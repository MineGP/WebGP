using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using WebGP.Domain.Helpers.MinecraftParser.Models;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Metrics;

namespace Domain.Tests.Helpers
{
    public class MinecraftParseBody
    {
        private Image<Rgba32> img;
        private Body body;

        private Image<Rgba32> topImage;
        private Image<Rgba32> botImage;
        private Image<Rgba32> leftImage;
        private Image<Rgba32> rightImage;
        private Image<Rgba32> frontImage;
        private Image<Rgba32> backImage;

        [SetUp]
        public void Setup()
        {
            #region byte[] imgBytes = fullImg, 64x6x, from base64
            byte[] imgBytes = Convert.FromBase64String("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");
            #endregion

            #region byte[] topBytes = topImg, 8x8, from base64
            byte[] topBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAECAYAAACzzX7wAAAAOElEQVQYGWPQ0ND4D8I2NjYYNEiMAZsETBOIZjAzM/sPwiCOuro6GMMUgE0AcWAKGBgY/oMwsqkAqK4wwY2OjaoAAAAASUVORK5CYII=");
            #endregion
            #region byte[] botBytes = botImg, 8x8, from base64
            byte[] botBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAECAYAAACzzX7wAAAAJUlEQVQYGWOQkJD4LyMj819OTg4Fg8RAcgwwBSABdEyaAlxWAAC20iox1ucO+AAAAABJRU5ErkJggg==");
            #endregion
            #region byte[] leftBytes = leftImg, 8x8, from base64
            byte[] leftBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAQAAAAMCAYAAABFohwTAAAAL0lEQVQYGWNQVFT8r6en9x9GM4AYRkZGYAEQzSAtLf0fGQ+UgISExH9kzIDMAbEBwlE//YCC6c0AAAAASUVORK5CYII=");
            #endregion
            #region byte[] rightBytes = rightImg, 8x8, from base64
            byte[] rightBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAQAAAAMCAYAAABFohwTAAAAL0lEQVQYGWNQVFT8r6en9x9GM4AYRkZGYAEQzSAtLf0fGQ+UgISExH9kzIDMAbEBwlE//YCC6c0AAAAASUVORK5CYII=");
            #endregion
            #region byte[] frontBytes = frontImg, 8x8, from base64
            byte[] frontBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAMCAYAAABfnvydAAAAbklEQVQoFZ2QsQoAIQxD+xmCInaqk4P//289chBpOVxuqNHmEauiqr7W8pvKnNP33m5mr/JMlVqrjzFSIQ0APGmtJRMwrkTqAXrvHislgIom9ikhApzlmkDgf0Kc5byilPIZkiA8wYK/YJOKHrwHO+CFPAjt0BAAAAAASUVORK5CYII=");
            #endregion
            #region byte[] backBytes = backImg, 8x8, from base64
            byte[] backBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAMCAYAAABfnvydAAAAZUlEQVQoFZWPwQrAMAhD/QyhRezJWw/9/39zZJCyIYXtoK82MaUyxsg5Z54oEZFrrTxRWmtpZne5e6I4Q/tu4DaJlJLAaPJl4CZZEiiQ/w18F0TKTlDV/e+nCWdogtZ7LybcQbsAphSDO3XqN9QAAAAASUVORK5CYII=");
            #endregion

            body = new Body(Image.Load<Rgba32>(imgBytes));

            topImage = Image.Load<Rgba32>(topBytes);
            botImage = Image.Load<Rgba32>(botBytes);
            leftImage = Image.Load<Rgba32>(leftBytes);
            rightImage = Image.Load<Rgba32>(rightBytes);
            frontImage = Image.Load<Rgba32>(frontBytes);
            backImage = Image.Load<Rgba32>(backBytes);
        }

        [Test]
        public void ParseBody_Base_Top_ValidData_Parse()
        {
            body.ParseTopBase();

            var res = body.Top;

            Assert.That(AreEquals(ref res!, ref topImage));
        }
        [Test]
        public void ParseBody_Base_Bot_ValidData_Parse()
        {
            body.ParseBotBase();

            var res = body.Bot;

            Assert.That(AreEquals(ref res!, ref botImage));
        }
        [Test]
        public void ParseBody_Base_Left_ValidData_Parse()
        {
            body.ParseLeftBase();

            var res = body.Left;

            Assert.That(AreEquals(ref res!, ref leftImage));
        }
        [Test]
        public void ParseBody_Base_Right_ValidData_Parse()
        {
            body.ParseRightBase();

            var res = body.Right;

            Assert.That(AreEquals(ref res!, ref rightImage));
        }
        [Test]
        public void ParseBody_Base_Front_ValidData_Parse()
        {
            body.ParseFrontBase();

            var res = body.Front;

            Assert.That(AreEquals(ref res!, ref frontImage));
        }
        [Test]
        public void ParseBody_Base_Back_ValidData_Parse()
        {
            body.ParseBackBase();

            var res = body.Back;

            Assert.That(AreEquals(ref res!, ref backImage));
        }

        private bool AreEquals(ref Image<Rgba32> first, ref Image<Rgba32> second)
        {
            bool expr = true;
            
            try
            {
                for (int i = 0; i < first.Width; i++)
                {
                    for (int j = 0; j < first.Height; j++)
                    {
                        if (first[i, j] != second[i, j])
                            expr = false;
                    }
                }
            }
            catch
            {
                expr = false;
            }
            return expr;
        }
    }
}
