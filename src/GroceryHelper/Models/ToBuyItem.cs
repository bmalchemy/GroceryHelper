using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MvvmHelpers;

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