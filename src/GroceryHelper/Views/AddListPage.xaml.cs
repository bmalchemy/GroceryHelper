using Rg.Plugins.Popup.Pages;

namespace GroceryHelper.Views
{
    public partial class AddListPage : PopupPage
    {
		public AddListPage()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}
