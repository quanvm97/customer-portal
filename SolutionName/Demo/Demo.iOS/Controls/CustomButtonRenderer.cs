using System.ComponentModel;
using System.Drawing;
using Foundation;
using Demo.Controls;
using Demo.iOS.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace Demo.iOS.Controls
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        public CustomButtonRenderer() : base()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = (CustomButton)e.NewElement;

                #region Upper

                // Set text in ALL CAPS mode or not.
                //if (element.Upper)
                //{
                //    this.Control.TextInputContextIdentifier.ToUpper(new NSLocale(element.Text));
                //    //this.Control.ContentEdgeInsets = new UIEdgeInsets(0, 0, 0, 0);
                //}

                this.Control.TitleLabel.LineBreakMode = UIKit.UILineBreakMode.WordWrap;
                this.Control.LineBreakMode = UIKit.UILineBreakMode.WordWrap;
                this.Control.TitleLabel.TextAlignment = UITextAlignment.Center;
                if (element.Image != null)
                {
                    this.Control.ImageEdgeInsets = new UIEdgeInsets(8, 0, 8, 0);

                }

                #endregion

                // Set max line
                if (element.MaxLine != 0)
                    Control.TitleLabel.Lines = element.MaxLine;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomButton.ImageProperty.PropertyName)
            {
                this.Control.ImageEdgeInsets = new UIEdgeInsets(8, 0, 8, 2);

            }
        }

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
