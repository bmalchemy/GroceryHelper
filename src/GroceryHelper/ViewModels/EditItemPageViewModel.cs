using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;
using GroceryHelper.Services;

namespace GroceryHelper.ViewModels
{
	public class EditItemPageViewModel : ViewModelBase, INavigationAware
    {
        public EditItemPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                        IDeviceService deviceService, IGroceryItemDataService groceryItemDataService,
		                             IGroceryItems groceryItems)
            : base(navigationService, pageDialogService, deviceService)
        {
			Title = AppResources.EditItemTitle;

            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);

			_groceryItems = groceryItems;

            _groceryItemDataService = groceryItemDataService;
        }

        #region props
        public IGroceryItems _groceryItems;

        public IGroceryItemDataService _groceryItemDataService;
        
        public Item EditItem { get; set; }

        public DelegateCommand SaveCommand { get; }
        #endregion

        #region navigation
        public void OnNavigatingTo(INavigationParameters parameters)
        {
            EditItem = parameters.GetValue<Item>("editItem");
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
            
            Item updatedItem = new Item
            {
				Id = EditItem.Id,
				Name = EditItem.Name,
				Aisle = EditItem.Aisle,
                Latitude = 0,
                Longitude = 0,
				Type = EditItem.Type, //ItemTypes.Canned.ToString();
				Notes = EditItem.Notes
            };

			if (!Helpers.ListHelpers.ItemIsDuplicate(updatedItem, _groceryItems.GItems))
			{
				EditItem = await _groceryItemDataService.UpdateItemAsync(updatedItem);
			}
            await _navigationService.GoBackAsync(new NavigationParameters { { "editItem", EditItem } });

        }
        #endregion
    }
}
