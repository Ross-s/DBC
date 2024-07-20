using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace DBC
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public static readonly string ChannelId = "backgroundServiceChannel";

        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override void OnCreate()
        {
            base.OnCreate();

            if ((int)Build.VERSION.SdkInt < 33) return;
            if (CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                Permissions.RequestAsync<Permissions.PostNotifications>();
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
#pragma warning disable CA1416
                var serviceChannel =
                    new NotificationChannel(ChannelId,
                        "Background Service Channel",
                    NotificationImportance.High);

                if (GetSystemService(NotificationService)
                    is NotificationManager manager)
                {
                    manager.CreateNotificationChannel(serviceChannel);
                }
#pragma warning restore CA1416
            }
        }
    }
}
