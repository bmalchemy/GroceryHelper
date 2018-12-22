using System.ComponentModel;

namespace GroceryHelper.Models
{
	public class ToBuyItem : Item, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
		public bool Done { get; set; }

		public bool IsSelected { get; set; }

		public int Quantity { get; set; }

		public Xamarin.Forms.Color Color { get; set; }

		public ToBuyItem(){
			Quantity = 1;
		}
    }
}