using CsenPomsaeScore;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for DeleteInfoControl.xaml
    /// </summary>
    public partial class DeleteInfoControl : UserControl
    {
        public DeleteInfoControl()
        {
            InitializeComponent();

            if (Competition.MyCompetition.GetAthletes().Count == 0)
                txbInfo.Text = "Inserisci le categorie e gli atleti per poter continuare.";
            else
                txbInfo.Text = "Seleziona un atleta da eliminare dal menù di sinistra. Premi nuovamente su '-' per tornare alla modalità precedente.";
        }
    }
}
