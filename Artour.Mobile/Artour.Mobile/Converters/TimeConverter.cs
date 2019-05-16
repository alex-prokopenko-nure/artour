using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Artour.Mobile.Converters
{
    class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Int32 seconds = (Int32)value;
            Int32 minutes = seconds > 59 ? seconds / 60 : 0;
            Int32 hours = minutes > 59 ? minutes / 60 : 0;
            return $"{(hours < 10 ? "0" + hours.ToString() : hours.ToString())}:" +
                $"{(minutes - hours * 60 < 10 ? "0" + (minutes - hours * 60).ToString() : (minutes - hours * 60).ToString())}:" +
                $"{(seconds - minutes * 60 < 10 ? "0" + (seconds - minutes * 60).ToString() : (seconds - minutes * 60).ToString())}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String str = (String)value;
            String[] time = str.Split(':');

            return (System.Convert.ToInt32(time[0]) * 60 + System.Convert.ToInt32(time[1])) * 60 + System.Convert.ToInt32(time[2]);
        }
    }
}
