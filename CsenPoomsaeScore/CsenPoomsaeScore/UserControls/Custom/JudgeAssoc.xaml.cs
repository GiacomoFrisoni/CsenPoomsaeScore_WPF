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

namespace CsenPoomsaeScore.UserControls.Custom
{
    /// <summary>
    /// Logica di interazione per JudgeAssoc.xaml
    /// </summary>
    public partial class JudgeAssoc : UserControl
    {
        public JudgeAssoc()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return TxblTitle.Text; }
            set { TxblTitle.Text = value; }
        }

        public string Message
        {
            get { return TxblMessage.Text; }
            set { TxblMessage.Text = value; }
        }

        public string Info
        {
            get { return TxblInfo.Text; }
            set { TxblInfo.Text = value; }
        }
    }
}
