using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestTimer.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(() => StartTimer());
        }

        public ICommand OpenWebCommand { get; }


        public void StartTimer()
        {
            const int waitTime = 60;

            int currentSpan = 0;



            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
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
                }

                return currentSpan < waitTime; // runs again, or false to stop
            });
        }
    }
}
