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
    public class AddListPageViewModel : ViewModelBase
    {
        public AddListPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
                                       IDeviceService deviceService)
            : base(navigationService, pageDialogService, deviceService)
        {
			Title = AppResources.AddListTitle;
			AddListCommand = new DelegateCommand(OnAddListCommandExecuted);
        }
        
		public AddList ListModel { get; set; }

		public DelegateCommand AddListCommand { get; }

        private bool _isNew;

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            _isNew = parameters.GetValue<bool>("new");
			ListModel = parameters.GetValue<AddList>("addList");
		}

		private async void OnAddListCommandExecuted()
        {
			await _navigationService.GoBackAsync(new NavigationParameters { { "addList", ListModel } });
        }
    }
}