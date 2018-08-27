using System.ComponentModel;

namespace GroceryHelper.Models
{
	public class ToBuyItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		public string Name { get; set; }

		public bool Done { get; set; }

		public Xamarin.Forms.Color Color { get; set; }
    }
}