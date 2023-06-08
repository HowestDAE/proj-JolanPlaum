using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows;

namespace AnimalCrossing.View.Converters
{
	internal class EndpointToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
				return new BitmapImage(new Uri("https://acnhapi.com/v1a/images/fish/1"));


			string endpoint = value.ToString();

			return new BitmapImage(new Uri($"https://acnhapi.com/v1a/images/{endpoint}"));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
