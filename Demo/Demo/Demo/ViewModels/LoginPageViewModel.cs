using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Demo.Extensions;
using Demo.Interfaces.LocalDatabase;
using Demo.Managers;
using Demo.Models;
using Demo.ViewModels.Base;
using Demo.Views.Popups;

namespace Demo.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Constructor

        public LoginPageViewModel(INavigationService navigationService, ISqLiteService sqLiteService)
            : base(navigationService: navigationService, sqliteService: sqLiteService)
        {
            LoginCommand = new DelegateCommand(LoginExecute);
            ForgotPasswordCommand = new DelegateCommand(ForgotPasswordExecute);
        }

        #endregion

        #region Properties

        private UserModel _userAccount = new UserModel();
        public UserModel UserAccount
        {
            get => _userAccount;
            set => SetProperty(ref _userAccount, value);
        }

        #endregion

        #region LoginCommand 

        public ICommand LoginCommand { get; set; }
        private async void LoginExecute()
        {
            await CheckBusy(async () =>
            {

                if (string.IsNullOrEmpty(UserAccount.Email))
                {
                    await MessagePopup.Instance.Show("Please enter your username!");
                    return;
                }
                if (string.IsNullOrEmpty(UserAccount.Password))
                {
                    await MessagePopup.Instance.Show("Please enter your password!");
                    return;
                }

                await Task.Run(async () =>
                {
                    // Delay 500ms to show loading
                    await Task.Delay(500);

                    await LoadingPopup.Instance.Show("Logging in");

                    if (!UserAccount.Email.Equals("viet.tran.business@gmail.com") || !UserAccount.Password.Equals("Abc@123456"))
                    {
                        // Show username and password is incorrect
                        await MessagePopup.Instance.Show("Username and password is incorrect!");
                        return;
                    }

                    // Store to database
                    UserAccount.EncryptedPassword = UserAccount.CryptPassword(UserAccount.Password);
                    SqLiteService.Insert(UserAccount);

                    await LoadingPopup.Instance.Hide();

                    // Navigate to Home page
                    await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
                    {
                        await Navigation.NavigateAsync(ManagerPage.Home());
                    });
                });

            });
        }

        #endregion

        #region ForgotPasswordCommand

        public ICommand ForgotPasswordCommand { get; set; }
        private async void ForgotPasswordExecute()
        {
            await CheckBusy(async () =>
            {
                await MessagePopup.Instance.Show(message: "Comming soon!");
            });
        }

        #endregion
    }
}
