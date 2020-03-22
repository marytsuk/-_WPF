using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;
using System.Windows;
using DataLib;
namespace WPF_Sample
{
    public class Conv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string res = "";
            if (value is Consulting)
            {
                res = "Consulting Org. Name : " + ((Consulting)value).Name;
            }
            else if (value is Project)
            {
                res = "Project Org. Name : " + ((Project)value).Name;
            }
            else if (value is Activity)
            {
                res = "Activity Org. Name : " + ((Activity)value).Name;
            }
            else
            {
                throw new Exception("Not an Activity");
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        

    }
}
