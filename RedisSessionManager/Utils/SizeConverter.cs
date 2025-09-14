using System.Globalization;
using System.Windows.Data;

namespace RedisSessionManager.Utils
{
    class SizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long bytes)
            {
                if (bytes < 1024) return $"{bytes} B";
                    double kb = bytes / 1024.0;
                if (kb < 1024) return $"{kb:F2} KB";
                    double mb = kb / 1024.0;
                if (mb < 1024) return $"{mb:F2} MB";
                    double gb = mb / 1024.0;
                return $"{gb:F2} GB";
            }
            return "Invalid size";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
