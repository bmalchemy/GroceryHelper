using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;
using GroceryHelper.Services;
using GroceryHelper.Helpers;

namespace GroceryHelper.ViewModels
{
	public class CreateGroceryListPageViewModel : ViewModelBase, INavigationAware
    {
		public CreateGroceryListPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
		                                      IDeviceService deviceService, IGroceryItemDataService groceryItemDataService, 
		                                      IGroceryItems groceryItems)
			: base(navigationService, pageDialogService, deviceService)
        {
			Title = AppResources.UserTitle;
            
			LoadItemsCommand = new DelegateCommand(OnLoadItemsCommandExecuted);
			ClearItemsCommand = new DelegateCommand(OnClearItemsCommandExecuted);
			CreateListCommand = new DelegateCommand(OnCreateListCommandExecuted);
			SelectItemTappedCommand = new DelegateCommand<ToBuyItem>(OnSelectItemCommandExecuted);

			GroceryItemsCollection = new ObservableRangeCollection<ToBuyItem>();

			_groceryItemDataService = groceryItemDataService;

			_groceryItems = groceryItems;
            
			LoadItemsCommand.Execute();
        }

        #region props
        readonly IGroceryItemDataService _groceryItemDataService;

		readonly IGroceryItems _groceryItems;

		public ObservableRangeCollection<ToBuyItem> GroceryItemsCollection { get; set; }

		public DelegateCommand CreateListCommand { get; }

		public DelegateCommand ClearItemsCommand { get; }
      
		public DelegateCommand LoadItemsCommand { get; }

		public DelegateCommand<ToBuyItem> SelectItemTappedCommand { get; }
        #endregion

        #region navigation
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
					if (parameters.ContainsKey("newItem"))
                    {
						GroceryItemsCollection.Add(CollectionConverters.ConvertToToBuyItem(parameters.GetValue<Item>("newItem")));
                    }
                    else if (parameters.ContainsKey("sync"))
					{
						GroceryItemsCollection = CollectionConverters.ConvertToObservable(parameters.GetValue<IList<Item>>("sync"));
					}

                    break;
                case NavigationMode.New:
					var buyingItems = parameters.GetValue<ObservableRangeCollection<ToBuyItem>>("toBuyItems");
					var selectedBuyingItems = buyingItems.Where((arg) => arg.IsSelected);
					if (parameters.ContainsKey("toBuyItems"))
					{
						foreach(var i in selectedBuyingItems)
						{
							var findInStoreItem = GroceryItemsCollection.Where((arg) => arg.Id == i.Id);
							if (findInStoreItem.Any())
							    findInStoreItem.Single().IsSelected = true;
						}
					}
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

        #region commands
        private void OnLoadItemsCommandExecuted()
		{
			GroceryItemsCollection = CollectionConverters.ConvertToObservable(_groceryItems.GItems);
        }

		private void OnClearItemsCommandExecuted()
		{
			foreach (var i in GroceryItemsCollection)
            {
				i.IsSelected = false;
            }
		}

		private async void OnCreateListCommandExecuted()
		{
			var tempColl = new ObservableRangeCollection<ToBuyItem>();

			foreach (var i in GroceryItemsCollection)
            {
				if (i.IsSelected)
					tempColl.Add(i);
            }
			if (tempColl.Count == 0)
			{
				await Xamarin.Forms.Application.Current.MainPage.DisplayAlert
				             (AppResources.Warning, AppResources.PleaseSelectItem, AppResources.OK);
				return;
			}
			await _navigationService.GoBackAsync(new NavigationParameters { { "createList", tempColl } });
           
		}

		private void OnSelectItemCommandExecuted(ToBuyItem item)
		{
			item.IsSelected = !item.IsSelected;
		}
        #endregion
    }
}
