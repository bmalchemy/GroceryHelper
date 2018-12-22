using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;
using GroceryHelper.Services;

namespace GroceryHelper.ViewModels
{
    public class AddListPageViewModel : ViewModelBase, INavigationAware
    {
        public AddListPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
		                            IDeviceService deviceService, IGroceryItemDataService groceryItemDataService)
            : base(navigationService, pageDialogService, deviceService)
        {
			Title = AppResources.AddListTitle;
			AddListCommand = new DelegateCommand(OnAddListCommandExecuted);
			_groceryItemDataService = groceryItemDataService;
        }

        #region props
        public IGroceryItemDataService _groceryItemDataService;

		public AddList ListModel { get; set; }

		public DelegateCommand AddListCommand { get; }

        private bool _isNew;
        #endregion

        #region navigation
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            _isNew = parameters.GetValue<bool>("new");
			ListModel = parameters.GetValue<AddList>("addList");
		}
        #endregion

        #region commands
        private async void OnAddListCommandExecuted()
        {
			var items = await _groceryItemDataService.GetItemsAsync();
			await _navigationService.GoBackAsync(new NavigationParameters { { "addList", ListModel } });
        }
        #endregion
    }
}