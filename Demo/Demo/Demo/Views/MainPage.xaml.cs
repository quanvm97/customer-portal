using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.Extensions;
using Demo.Views.Base;
using Demo.Views.Popups;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Demo.Views
{
    public partial class MainPage : PageBase
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region OnAppearing

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await progress.ProgressTo(0.9, 900, Easing.SpringIn);
        }

        #endregion

        #region Properties

        private bool _isCancel = false;
        private readonly string ic_reload = "ic_reload";
        private readonly string ic_cancel = "ic_cancel";

        #endregion

        #region webviewNavigating

        private async void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            //If on iOS or none were returned
            if (!CrossConnectivity.Current.IsConnected)
            {
                //You are offline, notify the user

                e.Cancel = true;

                progress.IsVisible = false;

                enUrl.Image = ic_reload;

                await MessagePopup.Instance.Show(message: "Please check on the internet connection and try again");
                return;
            }

            if (e.Url.ToLower().StartsWith("https://apps.mypurecloud.ie/webchat/storage/"))
            {
                // except case
                e.Cancel = true;
            }
            else
            {
                enUrl.Text = e.Url;
                UpdateBackAndForwardButton();

                e.Cancel = _isCancel;

                progress.IsVisible = !_isCancel;
                enUrl.Image = progress.IsVisible ? ic_cancel : ic_reload;
                _isCancel = false;
            }

        }

        #endregion

        #region webviewNavigated

        private void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            UpdateBackAndForwardButton();
            _isCancel = false;
            enUrl.Image = ic_reload;

            progress.IsVisible = false;
        }

        #endregion

        #region OnBackButtonClicked

        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            UpdateBackAndForwardButton();
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }

        }

        #endregion

        #region OnForwardButtonClicked

        private void OnForwardButtonClicked(object sender, EventArgs e)
        {
            UpdateBackAndForwardButton();
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        #endregion

        #region UpdateBackAndForwardButton

        private void UpdateBackAndForwardButton()
        {
            btnBack.IsEnabled = webView.CanGoBack;
            btnBack.Opacity = webView.CanGoBack ? 1 : 0.5;

            btnForward.IsEnabled = webView.CanGoForward;
            btnForward.Opacity = webView.CanGoForward ? 1 : 0.5;
        }

        #endregion

        #region OnReloadButtonClicked

        private void OnReloadButtonClicked(object sender, EventArgs e)
        {
            if (enUrl.Image == ic_cancel)
                _isCancel = true;

            webView.Reload();
        }

        #endregion

        #region TappedOnBackground

        private void TappedOnBackground(object sender, EventArgs e)
        {
            menuItem.IsVisible = false;
        }

        #endregion

        #region OnMenuItemClicked

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            lblLogout.Opacity = 0;
            menuItem.IsVisible = true;

            await lblLogout.TranslateTo(150, 0, 0, Easing.BounceOut);

            lblLogout.Opacity = 1;
            await lblLogout.TranslateTo(0, 0, 200);

        }

        #endregion

        #region OnBackButtonPress

        protected override bool OnBackButtonPressed()
        {
            UpdateBackAndForwardButton();
            if (webView.CanGoBack)
            {
                webView.GoBack();
                return true;
            }
            return false;
        }

        #endregion

    }
}