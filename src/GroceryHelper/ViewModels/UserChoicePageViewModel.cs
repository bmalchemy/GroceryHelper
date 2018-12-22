using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Resources;
namespace GroceryHelper.ViewModels
{
    public class UserChoicePageViewModel : ViewModelBase, INavigationAware
    {
		public UserChoicePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
		                                IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
			Title = AppResources.SyncTitle;
            
			StoreModeCommand = new DelegateCommand(OnStoreCommandExecuted);
			ManualModeCommand = new DelegateCommand(OnManualCommandExecuted);

        }

        #region props
        public DelegateCommand StoreModeCommand { get; }
		public DelegateCommand ManualModeCommand { get; }
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
        }
        #endregion

        #region commands
        private async void OnStoreCommandExecuted()
        {
			await _navigationService.GoBackAsync(new NavigationParameters { { "mode", "store" } });
        }

		private async void OnManualCommandExecuted()
        {
			await _navigationService.GoBackAsync(new NavigationParameters { { "mode", "manual" } });
        }
        #endregion
    }
}