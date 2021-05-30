using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DatabaseWPF.Models.Helpers
{
    public static class ImageHelper
    {
        public const string resourcesPath = "/DatabaseWPF;component/Images/";

        public const string uriPath = "pack://application:,,,/Images/";

        public static BitmapImage GetImage(string path, int pixelWidth = 800)
        {
            BitmapImage image = new ();

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(uriPath + path);
            image.DecodePixelWidth = pixelWidth;
            image.EndInit();
            image.Freeze();

            return image;
        }
    }
}
