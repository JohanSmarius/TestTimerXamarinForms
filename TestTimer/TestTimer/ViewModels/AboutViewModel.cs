using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestTimer.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        private string _timer1Progress = string.Empty; 
        public string Timer1Progress
        {
            get => _timer1Progress;
            set => SetProperty(ref _timer1Progress, value);
        }

        private bool _timer1Running = true;
        public bool Timer1Running
        {
            get => _timer1Running;
            set => SetProperty(ref _timer1Running, value);
        }


        private string _timer2Progress = string.Empty; 
        public string Timer2Progress
        {
            get => _timer2Progress;
            set => SetProperty(ref _timer2Progress, value);
        }

        private bool _timer2Running = true;
        public bool Timer2Running
        {
            get => _timer2Running;
            set => SetProperty(ref _timer2Running, value);
        }


        public string Type { get; set; }
        public void StartTimer()
        {
            const int waitTime = 60;

            int currentSpan = 0;
            
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Timer1Running = true;

                // do something every 10 seconds
                currentSpan += 5;

                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    Title = $"Remaining {waitTime - currentSpan} seconds";

                });

                if (currentSpan == waitTime)
                {
                    Vibration.Vibrate();
                    App.Current.MainPage.DisplayAlert("Timer finished", "Timer is afgelopen", "OK");
                    Timer1Running = false;
                }

                return currentSpan < waitTime; // runs again, or false to stop
            });
        }

        
        
        public AboutViewModel()
        {
            Title = "About";
            StartTimer1Command = new Command(() => StartTimer1());
            StartTimer2Command = new Command(() => StartTimer2());
            
        }

        public ICommand StartTimer1Command { get; }
        
        public ICommand StartTimer2Command { get; }


        public void StartTimer1()
        {
            const int waitTime = 60;

            int currentSpan = 0;
            
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Timer1Running = false;

                // do something every 10 seconds
                currentSpan += 5;


                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    Timer1Progress = $"Remaining {waitTime - currentSpan} seconds";

                });

                if (currentSpan == waitTime)
                {
                    Vibration.Vibrate();

                    var notification = new NotificationRequest
                    {
                        Android = new AndroidOptions
                        {
                            ChannelId = "ChannelForAlerts",
                            Ongoing = false,
                            Priority = NotificationPriority.Max,
                            VisibilityType = AndroidVisibilityType.Private,
                        },
                        BadgeNumber = 1,
                        CategoryType = NotificationCategoryType.Alarm,
                        Description = "Step 1 is completed",
                        Title = "Step completed",
                        ReturningData = "This data belongs to step 1",
                        NotificationId = 10,
                    };

                    NotificationCenter.Current.Show(notification);

//                    App.Current.MainPage.DisplayAlert("Timer finished", "Timer is afgelopen", "OK");
                    Timer1Running = true;
                }

                return currentSpan < waitTime; // runs again, or false to stop
            });
        }
        

        public void StartTimer2()
        {
            const int waitTime = 30;

            int currentSpan = 0;
            
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Timer2Running = false;

                // do something every 10 seconds
                currentSpan += 5;

                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    Timer2Progress = $"Remaining {waitTime - currentSpan} seconds";

                });

                if (currentSpan == waitTime)
                {
                    Vibration.Vibrate();

                    var notification = new NotificationRequest()
                    {
                        BadgeNumber = 1,
                        Description = "Step 2 ready",
                        Title = "Step ready",
                        ReturningData = "This data belongs to step 2",
                        NotificationId = 10,
                        Android = new AndroidOptions
                        {
                            ChannelId = "ChannelForAlerts",
                            Ongoing = false,
                            Priority = NotificationPriority.Max,
                            VisibilityType = AndroidVisibilityType.Private,
                        },

                    };

                    NotificationCenter.Current.Show(notification);

//                    App.Current.MainPage.DisplayAlert("Timer finished", "Timer is afgelopen", "OK");
                    Timer2Running = true;
                }

                return currentSpan < waitTime; // runs again, or false to stop
            });
        }

        
    }
}
