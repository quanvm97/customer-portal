
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Demo.Droid
{
    [Activity(
        Theme = "@style/SplashTheme",
       Icon = "@mipmap/ic_launcher",
        RoundIcon = "@mipmap/ic_launcher",
        //RoundIcon = "@drawable/Icon",
        MainLauncher = true,
        NoHistory = true,
        Exported = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here;

            StartActivity(typeof(MainActivity));
        }
    }
}
