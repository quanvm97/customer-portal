using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Demo.Interfaces.LocalDatabase;
using Demo.Views.Popups;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Demo.ViewModels.Base
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible, INotifyPropertyChanged
    {
        #region Properties Services

        //protected INavigationService NavigationService { get; private set; }
        public INavigationService Navigation { get; private set; }
        public IPageDialogService DialogService { get; private set; }
        //public IHttpRequest HttpRequest { get; private set; }
        public ISqLiteService SqLiteService { get; private set; }
        //public IFileService FileService { get; private set; }
        //public IAppInfo AppInfo { get; private set; }
        //public IExcelFileService ExcelFileService { get; private set; }

        //protected FirebaseDatabaseHelper FirebaseHelper { get; private set; }

        #endregion

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Constructor

        public ViewModelBase(INavigationService navigationService = null, IPageDialogService dialogService = null, ISqLiteService sqliteService = null/*,
            IHttpRequest httpRequest = null, IAppInfo appInfo = null,
            IFileService fileService = null, IExcelFileService excelFileService = null*/)
        {
            //PageTitle = TranslateExtension.Get("FairflexxFotoScan");
            if (navigationService != null) Navigation = navigationService;
            if (dialogService != null) DialogService = dialogService;
            //if (httpRequest != null) HttpRequest = httpRequest;
            if (sqliteService != null) SqLiteService = sqliteService;
            //if (fileService != null) FileService = fileService;
            //if (appInfo != null) AppInfo = appInfo;
            //if (excelFileService != null) ExcelFileService = excelFileService;
            //FirebaseHelper = new FirebaseDatabaseHelper();

            BackCommand = new DelegateCommand(async () => await BackExecute());
            BackToRootCommand = new DelegateCommand(async () => await BackToRootExecute());
            //ZoomImageCommand = new DelegateCommand<object>(ZoomImageExe);
        }

        #endregion

        #region Initialize

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        #endregion

        #region Navigate

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigated from");
#endif
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigating to");
#endif
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigated to");
#endif 

            if (parameters != null)
            {
                var navMode = parameters.GetNavigationMode();
                switch (navMode)
                {
                    case NavigationMode.New: OnNavigatedNewTo(parameters); break;
                    case NavigationMode.Back: OnNavigatedBackTo(parameters); break;
                }
            }

        }

        public virtual void OnNavigatedNewTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigate new to");
#endif
        }

        public virtual void OnNavigatedBackTo(INavigationParameters parameters)
        {
#if DEBUG
            Debug.WriteLine("Navigate back to");
#endif
        }

        #endregion

        #region OnAppear / Disappear

        public virtual void OnAppear()
        {

        }

        public virtual void OnFirstTimeAppear()
        {

        }

        public virtual void OnDisappear()
        {

        }

        #endregion

        #region CheckBusy

        protected async Task CheckBusy(Func<Task> function)
        {
            if (App.IsBusy)
            {
                App.IsBusy = false;
                try
                {
                    await function();
                }
                catch (Exception e)
                {
#if DEBUG
                    Debug.WriteLine(e);
#endif
                }
                finally
                {
                    App.IsBusy = true;

                    await LoadingPopup.Instance.Hide();
                }
            }
        }

        #endregion

        #region Check Permission

        [Obsolete]
        protected async void CheckPermission(Action action)
        {
            await CheckBusy(async () =>
            {
                var camera = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                var storage = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (storage == PermissionStatus.Granted)
                {
                    action();
                }
                else
                {
                    await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                }
            });
        }

        #endregion

        #region BackCommand

        public ICommand BackCommand { get; }

        protected virtual async Task BackExecute()
        {
            await CheckBusy(async () =>
            {
                await Navigation.GoBackAsync();
            });
        }

        #endregion

        #region BackToRootCommand

        public ICommand BackToRootCommand { get; }

        protected virtual async Task BackToRootExecute()
        {
            await CheckBusy(async () =>
            {
                await Navigation.GoBackToRootAsync();
            });
        }

        #endregion

        #region BackButtonPress

        /// <summary>
        /// //false is default value when system call back press
        /// </summary>
        /// <returns></returns>
        public virtual bool OnBackButtonPressed()
        {
            //false is default value when system call back press
            return false;
        }

        /// <summary>
        /// called when page need override soft back button
        /// </summary>
        public virtual void OnSoftBackButtonPressed() { }

        #endregion

        #region Zoom

        public ICommand ZoomImageCommand { get; }
        protected virtual void ZoomImageExe(object obj)
        {

        }

        #endregion

        #region AddHeaderToken

        public void AddHeaderToken(string token)
        {
            //var checkHeader = HttpRequest.DefaultRequestHeaders.Authorization;

            //if (checkHeader != null)
            //{
            //    HttpRequest.DefaultRequestHeaders.Remove("Authorization");
            //}

            //HttpRequest.DefaultRequestHeaders.Add("Authorization", token);
        }

        #endregion

        #region Token Expired

        public bool IsTokenExpire { get; set; } = false;

        public async Task LogOutAsync(string pageViewModel = null)
        {
            await Task.Run(async () =>
            {

            });
        }

        #endregion

        #region Destroy

        public virtual void Destroy()
        {

        }

        #endregion
    }
}
