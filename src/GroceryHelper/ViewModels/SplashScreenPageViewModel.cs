using Prism.Navigation;
using Prism.Services;

namespace GroceryHelper.ViewModels
{

    public class SplashScreenPageViewModel : ViewModelBase, INavigationAware
    {
        public SplashScreenPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
        }

        #region navigation
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            // TODO: Implement any initialization logic you need here. Example would be to handle automatic user login

            // Simulated long running task. You should remove this in your app.
            // await Task.Delay(4000);

            // After performing the long running task we perform an absolute Navigation to remove the SplashScreen from
            // the Navigation Stack.
            await _navigationService.NavigateAsync("/NavigationPage/MainPage?tobuy=Beer&tobuy=Milk&tobuy=Creamer");
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
        }
        #endregion
    }
}