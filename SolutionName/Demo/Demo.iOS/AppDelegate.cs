using Foundation;
using Prism;
using Prism.Ioc;
using Demo.Interfaces.LocalDatabase;
using Demo.iOS.Services.SQLiteService;
using UIKit;


namespace Demo.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Init(app);
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        #region Init

        private void Init(UIApplication app)
        {
            Rg.Plugins.Popup.Popup.Init();

            Xamarin.Forms.Forms.SetFlags(new string[] { "CarouselView_Experimental", "SwipeView_Experimental", "IndicatorView_Experimental" });
            //CarouselViewRenderer.Init();
            InitScreen();
            //InitCachedImage();

            global::Xamarin.Forms.Forms.Init();
        }

        private void InitScreen()
        {
            App.ScreenWidth = UIScreen.MainScreen.Bounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.Bounds.Height;
        }

        private void InitCachedImage()
        {
            //CachedImageRenderer.Init();

            //var config = new FFImageLoading.Config.Configuration()
            //{
            //    VerboseLogging = false,
            //    VerbosePerformanceLogging = false,
            //    VerboseMemoryCacheLogging = false,
            //    VerboseLoadingCancelledLogging = false,
            //    Logger = new CustomLogger(),
            //};
            //ImageService.Instance.Initialize(config);
        }

        //public class CustomLogger : FFImageLoading.Helpers.IMiniLogger
        //{
        //    public void Debug(string message)
        //    {
        //        Console.WriteLine(message);
        //    }

        //    public void Error(string errorMessage)
        //    {
        //        Console.WriteLine(errorMessage);
        //    }

        //    public void Error(string errorMessage, Exception ex)
        //    {
        //        Error(errorMessage + System.Environment.NewLine + ex.ToString());
        //    }
        //}

        #endregion
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterInstance<IDatabaseConnection>(new DatabaseConnection());
        }
    }
}
