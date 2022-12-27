using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using static WebGP.Domain.Helpers.MinecraftParser.MinecraftImageParser;
using WebGP.Domain.Helpers.MinecraftParser.Models;
namespace Domain.Tests.Helpers
{
    public class MinecraftParseHead
    {
        private Image<Rgba32> img;
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

            #region byte[] topBytes = headImg, 8x8, from base64
            byte[] topBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAIAAABLbSncAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAChSURBVBhXFY25EsIwEEO9q403kGQSCo6ajv//KUrAJPjEmE7z9Eai22mZ9n3JZdrr+omHZXy4zW0e1+PSGmFapkGIIFDb1VpZBWCygHY4zEOJOYbYBnAedoo/uszT2GsxtROp9ct9M62EVO5Pt4bUwsOtbguY1bIxMaUmGqJ3CAzEXLi9kcCqBaPpKp1POZYvRotaTUqZQTFn58Nr9e+P/wHRzlMrvBEybgAAAABJRU5ErkJggg==");
            #endregion
            #region byte[] botBytes = botImg, 8x8, from base64
            byte[] botBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAIAAABLbSncAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAADBSURBVBhXFY29DcIwFIRtvzh2CJi/EIogN6RiBwoKBqBgAMQMiBIxCIIV6FiCEZAQJVUECEJixzykr7j7ijuaRqoeSBVKyT1CSPZ8F7Z6fXLQ3SbaTliTnAvPY0ABmHOEWWOdsV9TCg5IQ0qsKOk4TYDR3Wah9BCnHrfLcru3lWNoi8KgDWKNYPAB8m8J/Tg+rOeyJkEIZ/Iyu09GyfF8hdNq1hrEoqWCXuQJPCekeE91m6mI+yEXQInN/1QGq4r4DxzeQRQhkWYIAAAAAElFTkSuQmCC");
            #endregion
            #region byte[] leftBytes = botImg, 8x8, from base64
            byte[] leftBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAYAAADED76LAAAArklEQVQoFWMwkBX7rysp/N9WRfq/qazYfwc1mf/GMqL/zRQk/muKCfxnsNOQA0t66iv/d9OU/x9grA5W5KarBFbEAGKAdEVZ6f4Pt9D6n+JgBFYYbKr1HyTHEGWvA9aV623yP9/d/H9liBVYYZKLwX+QIoZwGy0wA2YCSCGIDTcB5Ibr0/L+P9/UBjYaJAlyT4CxBsQNKioq/42MjP6DaA0NDTAGsWGYQV1dHa8CAAupZ9OKlCBlAAAAAElFTkSuQmCC");
            #endregion
            #region byte[] rightBytes = botImg, 8x8, from base64
            byte[] rightBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAYAAADED76LAAAArElEQVQoFWMwkBX7rysp/N9WRfq/qazYfwc1mf/GMqL/zRQk/muKCfxnsNOQA0t66iv/d9OU/x9grA5W5KarBFbEEGCs8R8kGWWl+99XV+l/ioMRWFGwqdZ/kCKGKHsdsGSut8n/JHv9/5UhVmBFSS4G/0GK4CaABEC6QQpB1sBNgLkBJACSAFn1fFPb/+vT8v6D5BhUVFT+w7CGhsZ/EAbx1dXV/xsZGRFWAAAJAWgtZgoiYQAAAABJRU5ErkJggg==");
            #endregion
            #region byte[] frontBytes = botImg, 8x8, from base64
            byte[] frontBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAYAAADED76LAAAAkklEQVQoFWPQFBP4b6Yg8d9NV+m/p74yGIPYIDGQHANMMthU63+UlS4Yg9gwRQwgBkgg3dPkf1GAJRiD2CDTbFWk/zPAJB9u7Pzfmebxvz7SAUUR2IStW7f+VzS59//jhXX/a6IcwWyQGMh0sBvOnz//36Ds4v8vl9f8f39yIZgNEgO5j0FdXf2/kZHRf1w0QQUATLd4ObzFavgAAAAASUVORK5CYII=");
            #endregion
            #region byte[] backBytes = botImg, 8x8, from base64
            byte[] backBytes = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAYAAADED76LAAAApElEQVQoFYWOzQrCQAyE8wiCuCi2KrrotrhYXFtL8acHaREvhQqFHsWrePbpRxLoTfAwhCRfJkORr2BHfRz1GLGvkC89OG+AZDZEoHqgk5nI8rpZoAimqNxKoMLOBaLKGRm0mUWThnjmW4HqOJRDas9rGbxuO7zLPT73TKDHJQK7UnMIwfQvB3an0mrwP4b4P4MctE4MOB9prdHJGANW13P9C3wBJEtk/jW2zC4AAAAASUVORK5CYII=");
            #endregion
            // DON'T EVEN TRY TO OPEN THIS REGIONS!!!!


            img = Image.Load<Rgba32>(imgBytes);
            topImage = Image.Load<Rgba32>(topBytes);
            botImage = Image.Load<Rgba32>(botBytes);
            leftImage = Image.Load<Rgba32>(leftBytes);
            rightImage = Image.Load<Rgba32>(rightBytes);
            frontImage = Image.Load<Rgba32>(frontBytes);
            backImage = Image.Load<Rgba32>(backBytes);
        }
        
        [Test]
        public void ParseHead_Top_ValidData_Parse()
        {
            bool expr = true;
            var head = new Head();
            head.ParseTop(img);
            try
            {
                for(int i = 0; i < 8; i++)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        if (head.Top[i, j] != topImage[i, j])
                            expr = false;
                    }
                }
            }
            catch 
            {
                expr = false; 
            }
            Assert.That(expr, Is.True);
        }
        [Test]
        public void ParseHead_Bot_ValidData_Parse()
        {
            bool expr = true;
            var head = new Head();
            head.ParseBot(img);
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (head.Bot[i, j] != botImage[i, j])
                            expr = false;
                    }
                }
            }
            catch
            {
                expr = false;
            }
            Assert.That(expr, Is.True);
        }
        [Test]
        public void ParseHead_Left_ValidData_Parse()
        {
            bool expr = true;
            var head = new Head();
            head.ParseLeft(img);
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (head.Left[i, j] != leftImage[i, j])
                            expr = false;
                    }
                }
            }
            catch
            {
                expr = false;
            }
            Assert.That(expr, Is.True);
        }
        [Test]
        public void ParseHead_Right_ValidData_Parse()
        {
            bool expr = true;
            var head = new Head();
            head.ParseRight(img);
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (head.Right[i, j] != rightImage[i, j])
                            expr = false;
                    }
                }
            }
            catch
            {
                expr = false;
            }
            Assert.That(expr, Is.True);
        }
        [Test]
        public void ParseHead_Front_ValidData_Parse()
        {
            bool expr = true;
            var head = new Head();
            head.ParseFront(img);
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (head.Front[i, j] != frontImage[i, j])
                            expr = false;
                    }
                }
            }
            catch
            {
                expr = false;
            }
            Assert.That(expr, Is.True);
        }
        [Test]
        public void ParseHead_Back_ValidData_Parse()
        {
            bool expr = true;
            var head = new Head();
            head.ParseBack(img);
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (head.Back[i, j] != backImage[i, j])
                            expr = false;
                    }
                }
            }
            catch
            {
                expr = false;
            }
            Assert.That(expr, Is.True);
        }
        
    }
}
