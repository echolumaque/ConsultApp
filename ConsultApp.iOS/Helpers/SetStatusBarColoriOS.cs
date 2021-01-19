using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsultApp.Helpers;
using ConsultApp.iOS.Helpers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(SetStatusBarColoriOS))]
namespace ConsultApp.iOS.Helpers
{
    public class SetStatusBarColoriOS : ISetStatusBarColor
    {
        public void SetStatusBarColor(Color color)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    var statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame)
                    {
                        BackgroundColor = color.ToUIColor()
                    };
                    UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                }
                else
                {
                    UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                    if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                    {
                        statusBar.BackgroundColor = color.ToUIColor();
                    }
                }
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.DarkContent, true);
                GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
            });
        }

        private UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;
            return vc;
        }
    }
}