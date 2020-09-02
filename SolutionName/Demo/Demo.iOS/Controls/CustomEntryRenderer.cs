using System;
using System.ComponentModel;
using System.Drawing;
using Demo.Controls;
using Demo.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Demo.iOS.Controls
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer() : base()
        {

        }

        #region OnElementPropertyChanged

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (CustomEntry)this.Element;
            var textField = this.Control;

            #region Image

            //if (!string.IsNullOrEmpty(element.Image))
            //{
            //    switch (element.ImageAlignment)
            //    {
            //        case ImageAlignment.Left:
            //            textField.LeftViewMode = UITextFieldViewMode.Always;
            //            textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
            //            break;
            //        case ImageAlignment.Right:
            //            textField.RightViewMode = UITextFieldViewMode.Always;
            //            textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
            //            break;
            //    }
            //}

            #endregion

            #region HasRoundedCorner

            //if (!element.HasRoundedCorner)
            //{
            //    this.Control.BorderStyle = UITextBorderStyle.None;
            //    this.Element.HeightRequest = 30;
            //    var a = this.Frame.Width;
            //    CALayer bottomBorder = new CALayer
            //    {
            //        Frame = new CGRect(0.0f, element.HeightRequest - 1, a, 1.0f),
            //        BorderWidth = 2.0f,
            //        BorderColor = Color.Black.ToCGColor()
            //    };
            //    textField.Layer.AddSublayer(bottomBorder);
            //}

            #endregion
        }

        #endregion

        #region OnElementPropertyChanged

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var element = (CustomEntry)sender;

            if (e.PropertyName == nameof(CustomEntry.Image))
            {
                if (!string.IsNullOrEmpty(element.Image))
                {
                    var textField = this.Control;
                    switch (element.ImageAlignment)
                    {
                        case ImageAlignment.Left:
                            textField.LeftViewMode = UITextFieldViewMode.Always;
                            textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                            break;
                        case ImageAlignment.Right:
                            textField.RightViewMode = UITextFieldViewMode.Always;
                            textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                            break;
                    }
                }
            }
        }

        #endregion

        #region GetImageView

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(0, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }

        #endregion

    }
}
