using System;
using Demo.Extensions;
using Demo.Views.Base;
using Xamarin.Forms;

namespace Demo.Views
{
    public partial class MainPage : PageBase
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await progress.ProgressTo(0.9, 900, Easing.SpringIn);
        }

        private bool _isCancel = false;

        private void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
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
                enUrl.Image = progress.IsVisible ? "ic_cancel" : "ic_reload";
                _isCancel = false;
            }
            
        }

        private void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            UpdateBackAndForwardButton();
            _isCancel = false;
            enUrl.Image = "ic_reload";

            progress.IsVisible = false;
        }

        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            UpdateBackAndForwardButton();
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }

        }

        private void OnForwardButtonClicked(object sender, EventArgs e)
        {
            UpdateBackAndForwardButton();
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        private void UpdateBackAndForwardButton()
        {
            btnBack.IsEnabled = webView.CanGoBack;
            btnBack.Opacity = webView.CanGoBack ? 1 : 0.5;

            btnForward.IsEnabled = webView.CanGoForward;
            btnForward.Opacity = webView.CanGoForward ? 1 : 0.5;
        }

        private void OnReloadButtonClicked(object sender, EventArgs e)
        {
            if (enUrl.Image == "ic_cancel")
                _isCancel = true;
            webView.Reload();
        }


    }
}