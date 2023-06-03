using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AnimalCrossing.View.Converters
{
	internal class TypeToIconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
				return new BitmapImage(new Uri("https://acnhcritterpedia.com/icons/icon-transparent.png"));


			string type = value.ToString();

			return new BitmapImage(new Uri($"/Resources/Icons/{type}.png", UriKind.Relative));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
