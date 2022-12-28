using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using WebGP.Domain.Helpers.MinecraftParser.Models;
using System.Security.Cryptography.X509Certificates;

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
            byte[] imgBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAACAAAAAQCAIAAAD4YuoOAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJUSURBVDhPlZPPS1RRFMffXyBBNRhp/ujRzEgPh5lRayhngvAHEQwoSLNUqTYVJAhBIW6EWbapIMIi2sVAwQRu2ghCIE3hokISXESEUCQGLvu8+d5utxkf5OHwOOd7v+d77z33PC/KkrED/Z2tmbbDhRNtfe2xoaCbFNA/1JJqjw0mjoEIJAU0ZXtafmTVRE5MJfWoFzPxoWTHaO9xK4d6MZMY6+vBAUkBVWXN1fybENj4rH80FE12TJzqwQnYjAsFRw5qqZQLcPYgBXTLXR1jglw0bEtd/cbwAE7AVdgDOb6fn93ZWn6MEwhUVbOOseYFHRNRpKcLaanrSXKdrUjvfHqNE3AOTmPK9tzD5u6CWoEc0rTCvsFA0v/4cObb0t2f7yo4ASmgqlwpIZFmR0WPyavqMdfKU18q89+X7+9uvMK33z4lBTRl/28aR82SHRXAzUdXt5YWfq082F2v4Nuri6SApqzZKFM36KO6jCjzrv8AaZpj+wMYxY/CPY6m9+St9JLSkjrNseMoPIofhXs0FxSJi0G3pkVaUr8ymr1ZPI0TQEMCnGL4mi7kxEfHxaUD3yvlT4Jeu5CdzPfeGs+xNnk+xZrUN1+Uy5dH5i4V7B7wKYbPzwEfOfEbcOnANzcgAWLZvUG1Wu3KbjCLt0vnCEgBJ86Ee+uk8AnEd3F7A8TNG9STcN6/vlz4cO86IO2r1Wqp2fc7a89/vHlCQKpeQ4BGc+BzOFTAhwOfbawO7RrrDyfb8/9Yom4E8Xg8nU5HfSHoa/kN1oDvewN9XX6D/YP7/m8lU0tZBXaczAAAAABJRU5ErkJggg==");
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
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
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
