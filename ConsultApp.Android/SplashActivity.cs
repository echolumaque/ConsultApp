using System.IO;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using Felipecsl.GifImageViewLibrary;

namespace ConsultApp.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        private GifImageView gifImageView;

        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.GIFSplash);
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageViews);

            Stream input = Assets.Open("splash2.gif");
            byte[] bytes = ConvertFileToByteArray(input);
            gifImageView.SetBytes(bytes);
            gifImageView.StartAnimation();

            var timer = new Timer
            {
                Interval = 1500,
                AutoReset = false,
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CreateActivity();
        }

        private byte[] ConvertFileToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }

        }

        private void CreateActivity()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
