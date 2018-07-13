using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MvvmHelpers;

namespace GroceryHelper.Models
{
	public class AddList : ObservableObject
    {
		public string List { get; set; }

		public string[] Items { get; set; }

        public bool Done { get; set; }
    }
}