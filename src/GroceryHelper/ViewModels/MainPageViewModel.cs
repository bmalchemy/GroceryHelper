using System;
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
    public class MainPageViewModel : ViewModelBase, INavigationAware
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
		                         IDeviceService deviceService, IGroceryItemDataService groceryItemDataService,
		                         IGroceryItems groceryItems)
			: base(navigationService, pageDialogService, deviceService)
        {
            Title = AppResources.MainPageTitle;
            ToBuyItems = new ObservableRangeCollection<ToBuyItem>();
            
			GroceryStorePageCommand = new DelegateCommand(OnGroceryStorePageCommandExecuted);
			CreateGroceryListPageCommand = new DelegateCommand(OnCreateGroceryListPageCommandExecuted);
			UserChoiceCommand = new DelegateCommand(OnUserChoicePageCommandExecuted);

			SyncCommand = new DelegateCommand(OnSyncCommandExecuted);

			AddItemCommand = new DelegateCommand(OnAddItemCommandExecuted);
			AddListCommand = new DelegateCommand(OnAddListCommandExecuted);
            DeleteItemCommand = new DelegateCommand<ToBuyItem>(OnDeleteItemCommandExecuted);
            ToBuyItemTappedCommand = new DelegateCommand<ToBuyItem>(OnToBuyItemTappedCommandExecuted);

			_groceryItemDataService = groceryItemDataService;
			_groceryItems = groceryItems;
        
        }

        #region props
        readonly IGroceryItems _groceryItems;

		readonly IGroceryItemDataService _groceryItemDataService;
        
        public ObservableRangeCollection<ToBuyItem> ToBuyItems { get; set; }
        
		public DelegateCommand CreateGroceryListPageCommand { get; }

		public DelegateCommand GroceryStorePageCommand { get; }

		public DelegateCommand UserChoiceCommand { get; }

		public DelegateCommand SyncCommand { get; }
        
        public DelegateCommand AddItemCommand { get; }
        
		public DelegateCommand AddListCommand { get; }

        public DelegateCommand<ToBuyItem> DeleteItemCommand { get; }

        public DelegateCommand<ToBuyItem> ToBuyItemTappedCommand { get; }
        #endregion

        #region navigation
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    if (parameters.ContainsKey("toBuyItem"))
                    {
                        ToBuyItems.Add(parameters.GetValue<ToBuyItem>("toBuyItem"));
					
					} 
					else if (parameters.ContainsKey("sync"))
					{
						var items = parameters.GetValue<IList<Item>>("sync");
						ToBuyItems = CollectionConverters.ConvertToObservable(items);

					} 
					else if (parameters.ContainsKey("createList"))
                    {
						ToBuyItems = parameters.GetValue<ObservableRangeCollection<ToBuyItem>>("createList");

					} 
					else if (parameters.ContainsKey("mode"))
                    {
						var mode = parameters.GetValue<string>("mode");
						if (mode == "store")
							CreateGroceryListPageCommand.Execute();
						else
							AddListCommand.Execute();

                    } 
					else if (parameters.ContainsKey("addList"))
					{
						var items = parameters.GetValue<AddList>("addList");
						if (items.List == null) {
							IsBusy = false;
							return;
						}

						string[] separators = { ",", "\n" };
						var itemsArray = items.List.Split(separators, StringSplitOptions.RemoveEmptyEntries);

						ToBuyItems.AddRange(itemsArray
						                    .Select(n => new ToBuyItem { Name = n }).Distinct());
					}
                    break;
                case NavigationMode.New:
					IsBusy = true;
					SyncCommand.Execute();

                    ToBuyItems.AddRange(parameters.GetValues<string>("tobuy")
                                         .Select(n => new ToBuyItem { Name = n }));
					IsBusy = false;
                    break;
            }
            IsBusy = false;
        }
        #endregion

        #region commands
        private async void OnUserChoicePageCommandExecuted() =>
	    	await _navigationService.NavigateAsync("UserChoicePage");
            
		private async void OnCreateGroceryListPageCommandExecuted() =>
		    await _navigationService.NavigateAsync("CreateGroceryListPage", new NavigationParameters
            {
                        { "toBuyItems", ToBuyItems }
            });

		private async void OnGroceryStorePageCommandExecuted() =>
		    await _navigationService.NavigateAsync("GroceryStorePage", new NavigationParameters
            {
    			{ "toBuyItems", ToBuyItems }
            });

		private async void OnSyncCommandExecuted()
		{
			var items = await _groceryItemDataService.GetItemsAsync();
			_groceryItems.GItems = items;
		}

        private async void OnAddItemCommandExecuted() =>
            await _navigationService.NavigateAsync("ToBuyItemDetail", new NavigationParameters
            {
                { "new", true },
                { "toBuyItem", new ToBuyItem() }
            });

		private async void OnAddListCommandExecuted() =>
            await _navigationService.NavigateAsync("AddListPage", new NavigationParameters
            {
                        { "new", true },
                        { "addList", new AddList() }
            });

        private void OnDeleteItemCommandExecuted(ToBuyItem item) =>
            ToBuyItems.Remove(item);

		private void OnToBuyItemTappedCommandExecuted(ToBuyItem item) =>
		    item.Done = !item.Done;

        #endregion
    }
}
