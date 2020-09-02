using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Demo.Extensions;
using Demo.Views.Base;
using Xamarin.Forms;

namespace Demo.Views.Popups
{
    public partial class MessagePopup : PopupBasePage
    {
        public MessagePopup()
        {
            InitializeComponent();
        }

        #region Instance

        private static MessagePopup _instance;

        public static MessagePopup Instance => _instance ?? (_instance = new MessagePopup() { IsClosed = true });

        private static Color backGroundDefault = Color.FromHex("#C91C3E");
        public async Task<MessagePopup> Show(string message = null, string closeButtonText = null,
            ICommand closeCommand = null, object closeCommandParameter = null, string textBackgroundColor = null,
            bool isAutoClose = false, uint duration = 2000)
        {
            // Close Loading Popup if it is showing
            await LoadingPopup.Instance.Hide();

            await DeviceExtension.BeginInvokeOnMainThreadAsync(() =>
            {
                //if (textBackgroundColor != null)
                //    LayoutButton.BackgroundColor = Color.FromHex(textBackgroundColor);
                //else
                //    LayoutButton.BackgroundColor = backGroundDefault;

                if (message != null)
                    LabelMessageContent.Text = message;

                //if (closeButtonText != null)
                //    ButtonMessageClose.Text = closeButtonText;

                ClosedPopupCommand = closeCommand;
                ClosedPopupCommandParameter = closeCommandParameter;

                IsAutoClose = isAutoClose;
                Duration = duration;
            });

            if (IsClosed)
            {
                IsClosed = false;

                if (isAutoClose && duration > 0)
                    AutoClosedPopupAfter(duration);

                await DeviceExtension.BeginInvokeOnMainThreadAsync(async () =>
                {
                    await Application.Current.MainPage.Navigation.PushPopupAsync(this);
                });
            }

            return this;
        }

        #endregion

        #region RefreshUI

        //public void RefreshUI()
        //{
        //    InitializeComponent();
        //}

        #endregion
    }
}
