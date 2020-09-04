using Prism.Navigation;
using Demo.ViewModels.Base;
using Prism.Commands;
using System.Windows.Input;
using Demo.Models;
using Demo.Extensions;
using Demo.Managers;
using System;
using Demo.Interfaces.LocalDatabase;

namespace Demo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, ISqLiteService sqliteService)
            : base(navigationService: navigationService, sqliteService: sqliteService)
        {
            Title = "Main Page";
            LogoutCommand = new DelegateCommand(LogoutExecute);
        }

        #region LogoutCommand

        public ICommand LogoutCommand { get; set; }
        private async void LogoutExecute()
        {
            await CheckBusy(async () =>
            {
                SqLiteService.DeleteAll<UserModel>();

                // Navigate to Login page
                await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
                {
                    await Navigation.NavigateAsync(new Uri($"{ManagerPage.NavigationHomeUri}/{ManagerPage.LoginPage}"));
                });
            });
        }

        #endregion
    }
}
