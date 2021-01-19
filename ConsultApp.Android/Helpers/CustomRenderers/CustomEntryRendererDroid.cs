using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using ConsultApp.Droid.Helpers.CustomRenderers;
using ConsultApp.Helpers.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace ConsultApp.Droid.Helpers.CustomRenderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //GradientDrawable gd = new GradientDrawable();
                //gd.SetColor(global::Android.Graphics.Color.Transparent);
                //Control.Background = gd;
                //Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
                Control.Background = null;
                ///Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}