using CsenPomsaeScore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace CsenPoomsaeScore.Classes
{
    class ExcelReader
    {
        private const string default_nationality = "ITA";

        private string filePath, sheetName;


        public ExcelReader(string path, string sheet, BackgroundWorker worker)
        {
            filePath = path;
            sheetName = sheet;

            worker.DoWork += worker_DoWork;
        }


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Dictionary <string, List<Athlete>> athletes = new Dictionary<string, List<Athlete>>();

            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook;
                Excel.Worksheet worksheet;
                Excel.Range range;
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = (Excel.Worksheet)workbook.Sheets[sheetName];

                int row = 0;
                int id = 1;
                range = worksheet.UsedRange;
                int rows = range.Rows.Count;

                for (row = 2; row <= rows; row++)
                {
                    //Check nationality
                    var tmp = (range.Cells[row, 15] as Excel.Range).Value2;
                    String nationality = (tmp == null) ? default_nationality : tmp.ToString();

                    //Check name
                    tmp = (range.Cells[row, 6] as Excel.Range).Value2;
                    String name = (tmp == null) ? null : tmp.ToString();

                    //Check surname
                    tmp = (range.Cells[row, 5] as Excel.Range).Value2;
                    String surname = (tmp == null) ? null : tmp.ToString();

                    //Check gym
                    tmp = (range.Cells[row, 2] as Excel.Range).Value2;
                    String gym = (tmp == null) ? null : tmp.ToString();

                    //Check category
                    tmp = (range.Cells[row, 9] as Excel.Range).Value2;
                    Athlete.Categories? category = (tmp == null) ? null : Athlete.GetCategoryFromDescription(tmp.ToString());

                    //Check gender
                    tmp = (range.Cells[row, 7] as Excel.Range).Value2;
                    Athlete.Genders? gender = (tmp == null) ? null : Athlete.GetGenderFromDescription(tmp.ToString());

                    //Check grade group
                    tmp = (range.Cells[row, 11] as Excel.Range).Value2;
                    Athlete.GradeGroups? gradeGroup = (tmp == null) ? null : Athlete.GetGradeGroupFromGrade(tmp.ToString());

                    //Validation and insert
                    if (name == null)
                        MessageBox.Show("Nome non valido alla riga " + row, "Errore nel file", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (surname == null)
                        MessageBox.Show("Cognome non valido alla riga " + row, "Errore nel file", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (gym == null)
                        MessageBox.Show("Palestra non valida alla riga " + row, "Errore nel file", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (!category.HasValue)
                        MessageBox.Show("Categoria non valida alla riga " + row, "Errore nel file", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (!gender.HasValue)
                        MessageBox.Show("Sesso non valido alla riga " + row, "Errore nel file", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (!gradeGroup.HasValue)
                        MessageBox.Show("Grado non valido alla riga " + row, "Errore nel file", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (category.HasValue && gender.HasValue && gradeGroup.HasValue)
                    {
                        Athlete athlete = new Athlete(id,
                            name,
                            surname,
                            gym,
                            nationality,
                            category.Value,
                            gender.Value,
                            gradeGroup.Value,
                            (range.Cells[row, 11] as Excel.Range).Value2.ToString());
                        
                        Competition.MyCompetition.AddAthlete(athlete);
                    }

                    id++;
                    (sender as BackgroundWorker).ReportProgress((id * 100) / rows);
                }

                Competition.MyCompetition.GetSingleAthletes();
                workbook.Close(true, Missing.Value, Missing.Value);
                excelApp.Quit();
            }
            catch (Exception exeption)
            {
                MessageBox.Show("Errore nella lettura del file: " + exeption.Message + "\nControlla che il file sia del giusto formato e che il nome del foglio sia corretto",
                    "File non valido",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                (sender as BackgroundWorker).ReportProgress(100);
            }
        }
    }
}
