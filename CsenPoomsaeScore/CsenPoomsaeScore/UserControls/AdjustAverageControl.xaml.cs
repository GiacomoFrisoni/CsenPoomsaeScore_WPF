using CsenPomsaeScore;
using CsenPoomsaeScore.Classes;
using System;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for AdjustAverageControl.xaml
    /// </summary>
    public partial class AdjustAverageControl : UserControl
    {
        Athlete athlete;
        double total;

        public AdjustAverageControl(Athlete athlete)
        {
            InitializeComponent();

            total = athlete.MediumScore.Value;
            txbPoints.Text = total.ToString();
            this.athlete = athlete;
        }

        private void btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Tag.ToString())
            {
                case "BigSub":
                    total = Math.Round(Math.Max(total - 0.30, 0.0), 2);
                    break;
                case "SmallSub":
                    total = Math.Round(Math.Max(total - 0.10, 0.0), 2);
                    break;
                case "BigAdd":
                    total += 0.30;
                    break;
                case "SmallAdd":
                    total += 0.10;
                    break;
                default:
                    break;
            }
            txbPoints.Text = total.ToString();
        }

        private void btnOk_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            athlete.TotalScore = total;
            athlete.HasFinished = true;
            MainController.MyMainController.RefreshList();
            ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
            TransitionController.MyTransitionController.ShowUserControlPage(new AthleteDoneControl(athlete), WpfPageTransitions.PageTransitionType.Fade);
        }
    }
}
