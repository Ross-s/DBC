using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;
using DBC.Platforms.Android;

namespace DBC
{
    [Service(Name = "com.rosssearle.dbc.DemoService")]
    public class DemoService : Service
    {
        System.Timers.Timer timer = null;
        int myId = (new object()).GetHashCode();
        int BadgeNumber = 0;
        private readonly IBinder binder = new LocalBinder();
        NotificationCompat.Builder notification;

        public class LocalBinder : Binder
        {
            public DemoService GetService()
            {
                return this.GetService();
            }
        }

        public override IBinder? OnBind(Intent? intent)
        {
            return binder;
        }

        public override StartCommandResult OnStartCommand(Intent intent,
       StartCommandFlags flags, int startId)
        {
            var input = intent.GetStringExtra("inputExtra");

            var notificationIntent = new Intent(this, typeof(MainActivity));
            notificationIntent.SetAction("USER_TAPPED_NOTIFIACTION");

            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent,
                PendingIntentFlags.Immutable);

            // Increment the BadgeNumber
            BadgeNumber++;

            notification = new NotificationCompat.Builder(this,
                    MainApplication.ChannelId)
                .SetContentText(input)
                .SetSmallIcon(Resource.Drawable.abc_btn_borderless_material)
                .SetAutoCancel(false)
                .SetContentTitle("Service Running")
                .SetPriority(NotificationCompat.PriorityDefault)
                .SetContentIntent(pendingIntent);

            notification.SetNumber(BadgeNumber);

            // build and notify
            StartForeground(myId, notification.Build(), ForegroundService.TypeDataSync);

            // timer to ensure hub connection
            timer = new System.Timers.Timer(TimeSpan.FromSeconds(10));
            timer.Elapsed += (sender, e) => Timer_Elapsed();
            timer.Start();

            // You can stop the service from inside the service by calling StopSelf();

            return StartCommandResult.Sticky;
        }

        async Task EnsureHubConnection()
        {
            BadgeNumber++;
            notification.SetNumber(BadgeNumber);
            notification.SetContentTitle($"test {BadgeNumber}");
            notification.SetAutoCancel(false);
            StartForeground(myId, notification.Build(), ForegroundService.TypeDataSync);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        async void Timer_Elapsed()
        {
            AndroidServiceManager.IsRunning = true;

            await EnsureHubConnection();
        }
    }
}
