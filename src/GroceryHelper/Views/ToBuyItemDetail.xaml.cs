using Rg.Plugins.Popup.Pages;

namespace GroceryHelper.Views
{
    public partial class ToBuyItemDetail : PopupPage
    {
        public ToBuyItemDetail()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}