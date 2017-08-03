using CsenPomsaeScore;
using CsenPoomsaeScore.UserControls.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Logica di interazione per JudgeAssocControl.xaml
    /// </summary>
    public partial class JudgeAssocControl : UserControl
    {
        public JudgeAssocControl()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            btnOK.Content = 0 + "/" + Competition.MyCompetition.GetJudgeNumber();

            for (int i = 0; i < Competition.MyCompetition.GetJudgeNumber(); i++)
            {
                JudgeAssoc ja = new JudgeAssoc();
                ja.Title = "Giudice " + (i+1);
                ja.Info = "[in attesa]";
                StpDevices.Children.Add(ja);

                await TheEnclosingMethod();
                ja.Info = "[OK]";
                ja.Message = "<ip adress of tablet>";
                

                btnOK.Content = (i + 1) + "/" + Competition.MyCompetition.GetJudgeNumber();
            }

            btnOK.IsEnabled = true;
            btnOK.Content = "OK";
        }


        public async Task<bool> TheEnclosingMethod()
        {
            await Task.Delay(2000);
            return true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            //Shows next page
            TransitionController.MyTransitionController.ShowUserControlPage(new MainControl(), WpfPageTransitions.PageTransitionType.Fade);

        }
    }
}
