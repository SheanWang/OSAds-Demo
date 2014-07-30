using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using OSAdsSetting.Resources;

namespace OSAdsSetting
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static string PERIODICTASKNAME = "PeriodicTaskTest";
        public PeriodicTask _tskPeriodic;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            ScheduledAction tTask = ScheduledActionService.Find(PERIODICTASKNAME);
            if (tTask != null)
            {
                _tskPeriodic = tTask as PeriodicTask;
            } 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StartPeriodicTask();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StopPeriodicTask(); 
        }

        private void StartPeriodicTask()
        {
            _tskPeriodic = new PeriodicTask(PERIODICTASKNAME);
            _tskPeriodic.Description = "OSAds, update tile by webserivce";
            if (IsTaskStart() == false)
            {
                ScheduledActionService.Add(_tskPeriodic);
            }

            ScheduledActionService.LaunchForTest(PERIODICTASKNAME, TimeSpan.FromSeconds(1));
        }

        private void StopPeriodicTask()
        {
            ScheduledActionService.Remove(PERIODICTASKNAME);
        }

        private bool IsTaskStart()
        {
            if (_tskPeriodic != null && _tskPeriodic.IsScheduled)
                return true;
            return false;
        }

    }
}