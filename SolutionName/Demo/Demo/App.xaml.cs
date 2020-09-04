using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Demo.Constants;
using Demo.Interfaces.LocalDatabase;
using Demo.Managers;
using Demo.Models;
using Demo.Services;
using Demo.ViewModels;
using Demo.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Demo
{
    public partial class App
    {
        #region Constructor

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        #endregion

        #region Properties 

        public new static App Current => Application.Current as App;
        public static double ScreenWidth;
        public static double ScreenHeight;

        public static double DisplayScreenHeight { get; set; }
        public static double DisplayScreenWidth { get; set; }
        public static double DisplayScaleFactor { get; set; }

        public static double RatioFontSizeResponsiveLayout { get; set; }

        public static bool IsBusy = true;

        public static DeviceInfo DeviceInfo { get; set; }

        public static DeviceOrientation DeviceOrientations { get; set; }

        //public static bool IsUseFormatXml = true;

        private ISqLiteService _sqLiteService;
        //public static AppSettings Settings { get; set; }
        //public static AppConfig Configs { get; set; }

        #endregion

        #region OnInitialized

        protected override async void OnInitialized()
        {
            InitDatabase();

            Application.Current.UserAppTheme = OSAppTheme.Light;
            InitializeComponent();

            await StartApp();
        }

        #endregion

        #region StartApp

        private async Task StartApp()
        {
            //await NavigationService.NavigateAsync(new Uri($"{ManagerPage.NavigationHomeUri}/{ManagerPage.NavigationPage}/{ManagerPage.MainPage}"));
            //return;

            var session = _sqLiteService.Get<UserModel>(s => !s.Id.Equals(Guid.Empty));
            if (session != null)
            {
                await NavigationService.NavigateAsync(new Uri($"{ManagerPage.NavigationHomeUri}/{ManagerPage.NavigationPage}/{ManagerPage.MainPage}"));
                //await NavigationService.NavigateAsync(ManagerPage.Home());
            }
            else
            {
                await NavigationService.NavigateAsync($"{ManagerPage.LoginPage}");
            }
        }

        #endregion

        #region InitAppCenter

        private void InitAppCenter()
        {
            AppCenter.Start($"android={SdkKeyConstants.AppCenterAndroid};" +
                            $"ios={SdkKeyConstants.AppCenteriOS}",
                typeof(Analytics), typeof(Crashes));
        }

        #endregion

        #region InitDatabase

        private void InitDatabase()
        {
            var connectionService = DependencyService.Get<IDatabaseConnection>();
            _sqLiteService = new SqLiteService(connectionService);
        }

        #endregion

        #region RegisterTypes

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            // Register Services
            containerRegistry.Register<ISqLiteService, SqLiteService>();
        }

        #endregion
    }
}
