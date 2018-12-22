using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;
using GroceryHelper.Services;

namespace GroceryHelper.ViewModels
{
    public class NewItemPageViewModel : ViewModelBase, INavigationAware
    {
		public NewItemPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
		                                IDeviceService deviceService, IGroceryItemDataService groceryItemDataService,
		                            IGroceryItems groceryItems)
            : base(navigationService, pageDialogService, deviceService)
        {
			Title = AppResources.NewItemTitle;

            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);

			_groceryItems = groceryItems;
			_groceryItemDataService = groceryItemDataService;
        }

        #region props
        public IGroceryItems _groceryItems;
		public IGroceryItemDataService _groceryItemDataService;

        public Item NewItem { get; set; }

        public DelegateCommand SaveCommand { get; }
        #endregion

        #region navigation
        public void OnNavigatingTo(INavigationParameters parameters)
        {
			NewItem = parameters.GetValue<Item>("newItem");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        #endregion

        #region commands
        private async void OnSaveCommandExecuted()
        {

			Item newitem = new Item
			{
				Name = NewItem.Name,
				Aisle = NewItem.Aisle,
				Latitude = 0,
				Longitude = 0,
				Type = NewItem.Type, //ItemTypes.Canned.ToString();
				Notes = NewItem.Notes
			};

		//	if (!Helpers.ListHelpers.ItemIsDuplicate(newitem, _groceryItems.GItems))
		//	{
				NewItem = await _groceryItemDataService.AddItemAsync(newitem);
		//	}
			await _navigationService.GoBackAsync(new NavigationParameters { { "newItem", NewItem } });

        }
        #endregion

    }
}