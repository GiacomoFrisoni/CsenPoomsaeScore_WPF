using CsenPomsaeScore;
using CsenPoomsaeScore.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for AthleteDoneControl.xaml
    /// </summary>
    public partial class AthleteDoneControl : UserControl
    {
        private Athlete athlete;

        public AthleteDoneControl(Athlete athlete)
        {
            InitializeComponent();

            this.athlete = athlete;
            txbName.Text = athlete.GetFullName();
            txbGym.Text = athlete.Gym;
            txbTime.Text = athlete.Time.Value.Minutes.ToString("00") + ":" + athlete.Time.Value.Seconds.ToString("00") + ":" + athlete.Time.Value.Milliseconds.ToString("000");
            txblTotalScore.Text = athlete.TotalScore.Value.ToString();
        }

        private void btnRestart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Asking if I'm REALLY sure that I wanna to reset
            MessageBoxResult result = MessageBox.Show("Sei sicuro di rieseguire la prova di " + athlete.Name + " " + athlete.Surname + "?",
              "Conferma riesecuzione prova",
              MessageBoxButton.YesNo,
              MessageBoxImage.Question);
            
            //Yes! I want to reset!
            if (result == MessageBoxResult.Yes)
            {
                athlete.ClearData();
                athlete.IncrementRound();
                MainController.MyMainController.RefreshList();

                ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
                TransitionController.MyTransitionController.ShowUserControlPage(new TimerControl(athlete), WpfPageTransitions.PageTransitionType.Fade);
            }
        }
    }
}
