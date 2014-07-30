using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace OSAds
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        public static string PERIODICTASKNAME = "PeriodicTaskTest";

        private struct AdInfo
        {
            public AdInfo(string location, string phone, string title)
            {
                this.location = location;
                this.phone = phone;
                this.title = title;
            }

            public string location;
            public string phone;
            public string title;
        }

        AdInfo[] ads = new AdInfo[3]
        {
            new AdInfo("1 km", "(202) 580-8889", "Mitchell's Fish Market"), 
            new AdInfo("777 m", "(248) 646-3663", "Restaurant Reservations"), 
            new AdInfo("1.5 km", "(010) 111222", "Rose's Luxury")
        };

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        protected override void OnInvoke(ScheduledTask task)
        {
            if (task.Name == PERIODICTASKNAME)
            {
                for(int i = 0; i<3; i++)
                {
                    ShellToast toast = new ShellToast();
                    toast.Title = ads[i].title;
                    toast.Content = "Phone: " + ads[i].phone + " Location: " + ads[i].location;
                    toast.Show();

                    Thread.Sleep(3000);
                }
            }

            ScheduledActionService.LaunchForTest(PERIODICTASKNAME, TimeSpan.FromSeconds(10));
            NotifyComplete(); 
        }
    }
}