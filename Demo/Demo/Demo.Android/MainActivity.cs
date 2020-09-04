using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
using Demo.Droid.Services.SQLiteService;
using Demo.Interfaces.LocalDatabase;

namespace Demo.Droid
{
    [Activity(Label = "Demo",
        Icon = "@mipmap/ic_launcher",
        //Theme = "@style/MainTheme",
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Init(bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

        #region Init

        //[System.Obsolete]
        public void Init(Bundle bundle)
        {
#pragma warning disable CS0612 // Type or member is obsolete
            RequestPermission();
#pragma warning restore CS0612 // Type or member is obsolete
            Xamarin.Forms.Forms.SetFlags(new string[] { "CarouselView_Experimental", "SwipeView_Experimental", "IndicatorView_Experimental" });
            //CarouselViewRenderer.Init();
            //CachedImageRenderer.Init(true);
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            InitScreen();

            Xamarin.Forms.Forms.Init(this, bundle);
        }

        #region InitScreen

        private void InitScreen()
        {
            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;

            App.DisplayScreenWidth = (double)Resources.DisplayMetrics.WidthPixels;
            App.DisplayScreenHeight = (double)Resources.DisplayMetrics.HeightPixels;
            App.DisplayScaleFactor = (double)Resources.DisplayMetrics.Density;
        }

        #endregion

        #endregion

        #region Permissions
        /// <summary>
        /// Permission request
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="permissions"></param>
        /// <param name="grantResults"></param>
        public override void OnRequestPermissionsResult(int requestCode,
            string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [System.Obsolete]
        private async void RequestPermission()
        {
            await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Storage);
        }

        #endregion
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterInstance<IDatabaseConnection>(new DatabaseConnection());
        }
    }
}

