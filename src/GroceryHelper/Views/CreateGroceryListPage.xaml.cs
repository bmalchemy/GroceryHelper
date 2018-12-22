using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GroceryHelper.Views
{
    public partial class CreateGroceryListPage : ContentPage
    {
        public CreateGroceryListPage()
        {
            InitializeComponent();
        }

		private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            // to clear selection on cell
            (sender as ListView).SelectedItem = null;
        }
    }
}
