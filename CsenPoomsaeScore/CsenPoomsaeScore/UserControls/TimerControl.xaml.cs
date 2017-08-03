using CsenPomsaeScore;
using CsenPoomsaeScore.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for TimerControl.xaml
    /// </summary>
    public partial class TimerControl : UserControl
    {
        private const string start = "Start";
        private const string stop = "Stop";

        private Athlete athlete;

        public TimerControl(Athlete athlete)
        {
            InitializeComponent();

            this.athlete = athlete;
            
            txbName.Text = athlete.Name + " " + athlete.Surname;
            btnStartStop.Content = start;
        }

        DateTime startTime;
        TimeSpan counter;

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalTimer.MyGlobalTimer.IsEnabled)
            {
                GlobalTimer.MyGlobalTimer.Stop();
                athlete.PoomsaeCompleted = true;
                ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
                TransitionController.MyTransitionController.ShowUserControlPage(new JudgePointsControl(athlete), WpfPageTransitions.PageTransitionType.Fade);
            }
            else
            {
                startTime = DateTime.Now;
                GlobalTimer.MyGlobalTimerReset();
                GlobalTimer.MyGlobalTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                GlobalTimer.MyGlobalTimer.Start();
                btnStartStop.Content = stop;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            counter = DateTime.Now - startTime;
            txbTime.Text = counter.Minutes.ToString("00") + ":" + counter.Seconds.ToString("00") + ":" + counter.Milliseconds.ToString("000");
            athlete.Time = counter;
            ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
        }

        private void btnRecede_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sei sicuro di ritirare " + athlete.Name + " " + athlete.Surname + " dalla gara?",
                               "Conferma ritiro",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                athlete.SetReceded();
                ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
                MainController.MyMainController.RefreshList();
                TransitionController.MyTransitionController.ShowUserControlPage(new AthleteDoneControl(athlete), WpfPageTransitions.PageTransitionType.Fade);
            }
        }
    }
}
