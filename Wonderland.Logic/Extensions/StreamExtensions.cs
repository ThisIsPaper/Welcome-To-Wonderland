
namespace Wonderland.Logic.Extensions
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public static class StreamExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>true is the supplied stream is a jpg / png or gif image</returns>
        public static bool IsImage(this Stream stream)
        {
            try
            {
                // read image from stream
                Image image = Image.FromStream(stream);

                // move pointer back to the beginning of the stream
                stream.Seek(0, SeekOrigin.Begin);

                return ImageFormat.Jpeg.Equals(image.RawFormat) || ImageFormat.Png.Equals(image.RawFormat) || ImageFormat.Gif.Equals(image.RawFormat);
            }
            catch
            {
                return false;
            }
        }
    }
}
