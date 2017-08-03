using CsenPomsaeScore;
using CsenPoomsaeScore.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for JudgePointsControl.xaml
    /// </summary>
    public partial class JudgePointsControl : UserControl
    {
        private Athlete athlete;

        public JudgePointsControl(Athlete athlete)
        {
            InitializeComponent();

            this.athlete = athlete;
            txbName.Text = athlete.GetFullName();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            awpPoints.Children.Clear();

            for (int i = 1; i <= Competition.MyCompetition.GetJudgeNumber(); i++)
            {
                StackPanel sp = new StackPanel();
                TextBlock txbl = new TextBlock();
                TextBox txb = new TextBox();

                txbl.Text = "Giudice " + i;
                txbl.Style = (Style)Application.Current.FindResource("TextBlockTitleStyle");

                txb.Name = "txbJudge" + i;
                txb.Style = (Style)Application.Current.FindResource("RangeNumberTextBoxStyle");

                sp.Margin = new Thickness(20, 10, 20, 10);
                sp.Children.Add(txbl);
                sp.Children.Add(txb);

                awpPoints.Children.Add(sp);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.Validate(this))
            {
                foreach (object obj in awpPoints.Children)
                {
                    if (obj is StackPanel)
                    {
                        StackPanel dp = (StackPanel)obj;
                        athlete.JudgeVotes.Add(Double.Parse(((TextBox)dp.Children[1]).Text.Replace(',', '.'), CultureInfo.InvariantCulture));
                    }
                }
                athlete.MediumScore = calculateAverageScore();

                ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
                TransitionController.MyTransitionController.ShowUserControlPage(new AdjustAverageControl(athlete), WpfPageTransitions.PageTransitionType.Fade);
            }
        }

        private double calculateAverageScore()
        {
            double res = 0.00;
            switch (Competition.MyCompetition.GetJudgeNumber())
            {
                case 3:
                    res = Math.Round(athlete.JudgeVotes.Average(), 2);
                    break;
                case 5:
                case 7:
                    List<double> tmpOrderedList = new List<double>();
                    foreach (double d in athlete.JudgeVotes)
                        tmpOrderedList.Add(d);
                    tmpOrderedList.Sort();
                    res = Math.Round(tmpOrderedList.GetRange(1, tmpOrderedList.Count - 2).Average(), 2);
                    break;
                default:
                    break;
            }
            return res;
        }
    }
}