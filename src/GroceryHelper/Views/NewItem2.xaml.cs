using Rg.Plugins.Popup.Pages;

namespace GroceryHelper.Views
{
    public partial class NewItem : PopupPage
    {
		public NewItem()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}