using Prism.Mvvm;
using Prism.Navigation;
using Demo.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace Demo.Views.Base
{
    public class PageBase : ContentPage, INavigationAware
    {
        #region Properties

        private bool _isAppeared;
        protected ViewModelBase ViewModel;

        #endregion

        #region 

        public PageBase()
        {
            //On<iOS>().SetPrefersLargeTitles(true);
            On<iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModelLocator.SetAutowireViewModel(this, true);
            BackgroundColor = Color.White;
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

        #region OnBindingContextChanged

        protected override void OnBindingContextChanged()
        {
            if (BindingContext != null)
                ViewModel = (ViewModelBase)BindingContext;
        }

        #endregion

        #region OnAppearing

        protected override void OnAppearing()
        {
            try
            {
                if (ViewModel == null && BindingContext != null)
                    ViewModel = (ViewModelBase)BindingContext;

                if (!_isAppeared)
                    ViewModel?.OnFirstTimeAppear();

                _isAppeared = true;
                ViewModel?.OnAppear();

                On<iOS>().SetUseSafeArea(true);
                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Left = 20;
                //Padding = new Thickness(20);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        #endregion

        #region OnDisappearing

        protected override void OnDisappearing()
        {
            ViewModel?.OnDisappear();
        }


        #endregion

        #region BackButtonPress

        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as ViewModelBase;
            var result = bindingContext?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
            return result;
        }


        public void OnSoftBackButtonPressed()
        {
            var bindingContext = BindingContext as ViewModelBase;
            bindingContext?.OnSoftBackButtonPressed();
        }

        public bool NeedOverrideSoftBackButton { get; set; } = false;

        #endregion
    }
}
