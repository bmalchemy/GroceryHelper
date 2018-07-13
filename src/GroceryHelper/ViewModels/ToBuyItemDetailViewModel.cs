using System;
using System.Collections.Generic;
using System.Linq;
using MvvmHelpers;
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using GroceryHelper.Models;
using GroceryHelper.Resources;

namespace GroceryHelper.ViewModels
{
    public class ToBuyItemDetailViewModel : ViewModelBase
    {
        public ToBuyItemDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                       IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
            Title = AppResources.ToBuyItemDetailTitle;
            SaveCommand = new DelegateCommand(OnSaveCommandExecuted);
        }

        public ToBuyItem Model { get; set; }

        public DelegateCommand SaveCommand { get; }

        private bool _isNew;

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            _isNew = parameters.GetValue<bool>("new");
            Model = parameters.GetValue<ToBuyItem>("toBuyItem");
        }

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
    }
}