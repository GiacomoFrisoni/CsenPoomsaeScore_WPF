using CsenPomsaeScore;
using CsenPoomsaeScore.Classes;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for AddDataControl.xaml
    /// </summary>
    public partial class AddDataControl : UserControl
    {
        private const string name_sheet = "Worksheet1";

        public AddDataControl()
        {
            InitializeComponent();
            
            cmbCategory.ItemsSource = Athlete.GetCategoriesDescriptions();
        }

        private void btnInsertAthlete_Click(object sender, RoutedEventArgs e)
        {
            //Checks input
            if (Validator.Validate(grbAthlete))
            {
                Athlete.GradeGroups? grade = Athlete.GetGradeGroupFromGrade(txbGrade.Text);
                if (grade.HasValue)
                {
                    Athlete.Categories category = Athlete.GetCategoryFromDescription(cmbCategory.Text).Value;
                    Athlete.Genders gender = Athlete.GetGenderFromDescription(cmbGender.Text).Value;
                    Athlete athlete = new Athlete(Competition.MyCompetition.GetNextAthleteCounter(), txbAthleteName.Text, txbAthleteSurname.Text, txbAthleteGym.Text,
                        txbAthleteNationality.Text, category, gender, grade.Value, txbGrade.Text);
                    Competition.MyCompetition.AddAthlete(athlete);
                    MainController.MyMainController.RefreshList();

                    //Empty all
                    txbAthleteName.Text = String.Empty;
                    txbAthleteSurname.Text = String.Empty;
                    txbAthleteGym.Text = String.Empty;
                    txbAthleteNationality.Text = String.Empty;
                    txbGrade.Text = String.Empty;
                    txbAthleteName.Focus();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(txbGrade.Text + " non è un grado valido.\nInserisci un valore corretto per procedere all'aggiunta.",
                                  "Grado non valido",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                }
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xls";
            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
                txbFilePath.Text = openfile.FileName;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (txbFilePath.Text != "" && txbFilePath.Text != null)
            {
                Competition.MyCompetition.ClearAll();

                BackgroundWorker worker = new BackgroundWorker();
                worker.RunWorkerCompleted += RefreshData;
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += worker_ProgressChanged;

                ExcelReader er = new ExcelReader(txbFilePath.Text, name_sheet, worker);

                worker.RunWorkerAsync();
            }
        }

        private void RefreshData(object sender, RunWorkerCompletedEventArgs e)
        {
            cmbCategory.ItemsSource = Athlete.GetCategoriesDescriptions();
            MainController.MyMainController.RefreshList();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgbProgress.Value = e.ProgressPercentage;
        }
    }
}
