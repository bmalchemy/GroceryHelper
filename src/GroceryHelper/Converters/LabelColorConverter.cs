using System;
using System.Globalization;
using Xamarin.Forms;

namespace GroceryHelper.Converters
{
	public class LabelColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool)value)
			{
				return "#C2C2C2";
			}
			return "#212121";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}