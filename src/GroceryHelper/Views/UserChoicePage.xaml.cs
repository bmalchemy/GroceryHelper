using Rg.Plugins.Popup.Pages;

namespace GroceryHelper.Views
{
    public partial class UserChoicePage : PopupPage
    {
        public UserChoicePage()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}
