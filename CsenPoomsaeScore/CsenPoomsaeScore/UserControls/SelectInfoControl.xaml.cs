using CsenPomsaeScore;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for SelectInfoControl.xaml
    /// </summary>
    public partial class SelectInfoControl : UserControl
    {
        public SelectInfoControl()
        {
            InitializeComponent();

            if (Competition.MyCompetition.GetAthletes().Count == 0)
                txbInfo.Text = "Inserisci le categorie e gli atleti per poter continuare.";
            else
                txbInfo.Text = "Seleziona un atleta dal menù di sinistra per gestire la sua esibizione.";
        }
    }
}
