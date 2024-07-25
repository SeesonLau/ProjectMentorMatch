using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Methods
{
    public static class CompressImage
    {
        public static byte[] ResizeImage(byte[] imageData, int width, int height)
        {

            using (var inputStream = new MemoryStream(imageData))
            using (var original = SKBitmap.Decode(inputStream))
            {
                var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
                using (var image = SKImage.FromBitmap(resized))
                using (var outputStream = new MemoryStream())
                {
                    image.Encode(SKEncodedImageFormat.Jpeg, 75).SaveTo(outputStream);
                    return outputStream.ToArray();
                }
            }
        }
    }
}