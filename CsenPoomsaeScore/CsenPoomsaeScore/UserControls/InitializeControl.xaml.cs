using CsenPomsaeScore;
using CsenPoomsaeScore.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for InitializeControl.xaml
    /// </summary>
    public partial class InitializeControl : UserControl
    {
        public InitializeControl()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Checks input
            if (Validator.Validate(this))
            {
                //Sets data
                Competition.MyCompetition.SetCompetitionName(txbNomeGara.Text);
                Competition.MyCompetition.SetJudgeNumber(Int32.Parse(cmbJudgeNumber.Text));

                //Shows next page
                TransitionController.MyTransitionController.ShowUserControlPage(new MainControl(), WpfPageTransitions.PageTransitionType.Fade);
            }
        }
    }
}
