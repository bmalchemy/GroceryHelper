using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Prism.AppModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;

namespace GroceryHelper.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                 IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = AppResources.MainPageTitle;
            ToBuyItems = new ObservableRangeCollection<ToBuyItem>();

            AddItemCommand = new DelegateCommand(OnAddItemCommandExecuted);
			AddListCommand = new DelegateCommand(OnAddListCommandExecuted);
            DeleteItemCommand = new DelegateCommand<ToBuyItem>(OnDeleteItemCommandExecuted);
            ToBuyItemTappedCommand = new DelegateCommand<ToBuyItem>(OnToBuyItemTappedCommandExecuted);
        }

        public ObservableRangeCollection<ToBuyItem> ToBuyItems { get; set; }

        public DelegateCommand AddItemCommand { get; }
        
		public DelegateCommand AddListCommand { get; }

        public DelegateCommand<ToBuyItem> DeleteItemCommand { get; }

        public DelegateCommand<ToBuyItem> ToBuyItemTappedCommand { get; }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            IsBusy = true;
            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    if (parameters.ContainsKey("toBuyItem"))
                    {
                        ToBuyItems.Add(parameters.GetValue<ToBuyItem>("toBuyItem"));
					} else if (parameters.ContainsKey("addList"))
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
                    ToBuyItems.AddRange(parameters.GetValues<string>("tobuy")
                                         .Select(n => new ToBuyItem { Name = n }));
                    break;
            }
            IsBusy = false;
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

        private async void OnToBuyItemTappedCommandExecuted(ToBuyItem item) =>
            await _navigationService.NavigateAsync("ToBuyItemDetail", new NavigationParameters{
                { "toBuyItem", item }
            });
    }
}
