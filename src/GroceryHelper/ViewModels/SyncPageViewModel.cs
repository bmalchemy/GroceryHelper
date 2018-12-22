using System.Collections.Generic;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;
using GroceryHelper.Services;

namespace GroceryHelper.ViewModels
{
    public class SyncPageViewModel : ViewModelBase, INavigationAware
    {
        public SyncPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                        IDeviceService deviceService, IGroceryItemDataService groceryItemDataService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = AppResources.SyncTitle;

            SyncCommand = new DelegateCommand(OnSyncCommandExecuted);

            _groceryItemDataService = groceryItemDataService;
        }

        #region props
        public IGroceryItemDataService _groceryItemDataService;

        public IList<Item> SyncItems { get; set; }

        public DelegateCommand SyncCommand { get; }
        #endregion

        #region navigation
        public void OnNavigatingTo(INavigationParameters parameters)
        {
            SyncItems = parameters.GetValue<List<Item>>("sync");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        #endregion

        #region commands
        private async void OnSyncCommandExecuted()
        {
            SyncItems = await _groceryItemDataService.GetItemsAsync();

            await _navigationService.GoBackAsync(new NavigationParameters { { "sync", SyncItems } });
        }
        #endregion
    }
}