using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EkzamenADO.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        public static bool TestMode { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string filename && !string.IsNullOrEmpty(filename))
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", filename);
                if (File.Exists(path))
                {
                    if (TestMode)
                        return path;

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(path, UriKind.Absolute);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    return image;
                }
            }

            // Если нет изображения — не возвращаем вообще, пусть UI покажет пустое
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
