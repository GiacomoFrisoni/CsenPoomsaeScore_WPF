using System;
using System.Windows;

namespace CsenPoomsaeScore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScoreWindow scoreWindow;

        public MainWindow()
        {
            InitializeComponent();

            //Sets the controller
            TransitionController.MyTransitionController.SetControl(pageTransitionControl);

            //Loads the first screen
            TransitionController.MyTransitionController.ShowUserControlPage(new UserControls.InitializeControl(), WpfPageTransitions.PageTransitionType.GrowAndFade);

            //Open the score window
            scoreWindow = new ScoreWindow();
            scoreWindow.Show();
            ScoreWindowController.MyScoreWindowController.SetControl(scoreWindow);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Asking if I'm REALLY sure that I wanna to change screen
            MessageBoxResult result = MessageBox.Show("Sei sicuro di voler chiudere l'applicazione?", "Conferma chiusura", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //Yes! I want to change!
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
