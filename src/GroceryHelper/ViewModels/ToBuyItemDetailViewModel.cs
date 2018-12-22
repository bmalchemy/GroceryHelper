using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;
using GroceryHelper.Services;

namespace GroceryHelper.ViewModels
{
    public class ToBuyItemDetailViewModel : ViewModelBase, INavigationAware
    {
        public ToBuyItemDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
		                                IDeviceService deviceService, IGroceryItemDataService groceryItemDataService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = AppResources.ToBuyItemDetailTitle;
            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);
			_groceryItemDataService = groceryItemDataService;
        }

        #region props
        public IGroceryItemDataService _groceryItemDataService;

        public ToBuyItem Model { get; set; }

        public DelegateCommand SaveCommand { get; }

        private bool _isNew;
        #endregion

        #region navigation
        public void OnNavigatingTo(INavigationParameters parameters)
        {
            _isNew = parameters.GetValue<bool>("new");
            Model = parameters.GetValue<ToBuyItem>("toBuyItem");
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
            if (_isNew)
			{
                await _navigationService.GoBackAsync(new NavigationParameters { { "toBuyItem", Model } });
            }
            else
            {
                await _navigationService.GoBackAsync();
            }
        }
        #endregion
    }
}