using System.Collections.Generic;
using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;
using GroceryHelper.Services;

namespace GroceryHelper.ViewModels
{
	public class GroceryStorePageViewModel : ViewModelBase, INavigationAware
    {
        public GroceryStorePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
		                         IDeviceService deviceService, IGroceryItemDataService groceryItemDataService,
		                                 IGroceryItems groceryItems)
			: base(navigationService, pageDialogService, deviceService)
        {
			Title = AppResources.AdminTitle;

			SyncCommand = new DelegateCommand(OnSyncCommandExecuted);
			NewItemCommand = new DelegateCommand(OnNewItemCommandExecuted);
			EditItemCommand = new DelegateCommand<Item>(OnEditItemCommandExecuted);
			DeleteItemCommand = new DelegateCommand<Item>(OnDeleteItemCommandExecuted);
  
			GroceryItemsCollection = new ObservableRangeCollection<Item>();

			_groceryItems = groceryItems;
			_groceryItemDataService = groceryItemDataService;

			LoadItems(_groceryItems.GItems);
        }

        #region props
        IGroceryItems _groceryItems;
		IGroceryItemDataService _groceryItemDataService;

		public ObservableRangeCollection<Item> GroceryItemsCollection { get; set; }

		public IList<Item> GroceryItems { get; set; }

		public DelegateCommand SyncCommand { get; }

		public DelegateCommand NewItemCommand { get; }
      
		public DelegateCommand<Item> EditItemCommand { get; }

		public DelegateCommand<Item> DeleteItemCommand { get; }
        #endregion

        private void LoadItems(IList<Item> items)
        {
			GroceryItemsCollection.Clear();

            foreach (var i in items)
            {
				GroceryItemsCollection.Add(i);
            }
        }

        #region navigation
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
					if (parameters.ContainsKey("newItem"))
                    {
						var item = parameters.GetValue<Item>("newItem");
						_groceryItems.GItems.Add(item);
						GroceryItemsCollection.Add(item);
                    }
					else if (parameters.ContainsKey("editItem"))
                    {
						var item = parameters.GetValue<Item>("editItem");
						Helpers.ListHelpers.UpdateItem(item, _groceryItems.GItems);
                    }
					else if (parameters.ContainsKey("sync"))
					{
						LoadItems(parameters.GetValue<IList<Item>>("sync"));
					}

                    break;
                case NavigationMode.New:
                    break;
            }
            IsBusy = false;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
        }
        #endregion

        #region commmands
        private async void OnSyncCommandExecuted() =>
        await _navigationService.NavigateAsync("SyncPage", new NavigationParameters
        {
			{ "sync", new List<Item>() }
        });

		private async void OnNewItemCommandExecuted()
		{
			await _navigationService.NavigateAsync("NewItemPage", new NavigationParameters
    		{
    			{ "newItem", new Item() }
    		});
		}

		private async void OnEditItemCommandExecuted(Item item)
		{
			await _navigationService.NavigateAsync("EditItemPage", new NavigationParameters
			{
				{ "editItem", item }
			});
		}

		private async void OnDeleteItemCommandExecuted(Item item)
        {
			await _groceryItemDataService.RemoveItemAsync(item);
			Helpers.ListHelpers.RemoveItem(item, _groceryItems.GItems);
        }
        #endregion
    }
}
