using CsenPomsaeScore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CsenPoomsaeScore.Classes;
using System.Collections.Generic;

namespace CsenPoomsaeScore.UserControls
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl, MainControlInterface
    {
        private const string ALERT_TITLE = "Conferma cambio schermata";
        private const string GO_ADD_MODE = "Sei sicuro di voler passare all'aggiunta degli atleti?";
        private const string GO_DELETE_MODE = "Sei sicuro di voler passare all'eliminazione degli atleti?";
        private const string BACK_DELETE_MODE = "Sei sicuro di voler tornare alla modalità normale?";

        private bool deleteMode = false;
        private object previousSelectedItem = null;

        public MainControl()
        {
            InitializeComponent();
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainController.MyMainController.SetControl(this);

            //Sets the new controller
            TransitionController.MyTransitionController.SetControl(pageTransitionControl);
            //Loads the delete info screen
            TransitionController.MyTransitionController.ShowUserControlPage(new SelectInfoControl(), WpfPageTransitions.PageTransitionType.Fade);

            //Loads combobox itemsources
            cmbCategory.ItemsSource = Athlete.GetCategoriesDescriptions();
            cmbGender.ItemsSource = Athlete.GetGendersDescriptions();
            cmbGrade.ItemsSource = Athlete.GetGradeDescriptions();
        }
        
        public void RefreshList()
        {
            if (Validator.Validate(grdFilter))
            {
                lsvData.ClearValue(ItemsControl.ItemsSourceProperty);
                Athlete.Categories? category = Athlete.GetCategoryFromDescription(cmbCategory.Text);
                Athlete.Genders? gender = Athlete.GetGenderFromDescription(cmbGender.Text);
                Athlete.GradeGroups? gradeGroup = Athlete.GetGradeGroupFromDescription(cmbGrade.Text);
                List<Athlete> athletes = Competition.MyCompetition.GetFilteredAthlete(category.Value, gender.Value, gradeGroup.Value);
                if (athletes.Count > 0)
                    lsvData.ItemsSource = athletes;
                else
                    brdEmptyList.Visibility = Visibility.Visible;
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lsvData.ClearValue(ItemsControl.ItemsSourceProperty);
            brdEmptyList.Visibility = Visibility.Collapsed;
            
            RefreshList();
        }
        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Yes! I want to change!
            if (checkWindowChange(ALERT_TITLE, GO_ADD_MODE))
            {
                deleteMode = false;

                //Sets the new controller
                TransitionController.MyTransitionController.SetControl(pageTransitionControl);
                //Loads the data screen
                TransitionController.MyTransitionController.ShowUserControlPage(new AddDataControl(), WpfPageTransitions.PageTransitionType.SlideAndFade);

                ScoreWindowController.MyScoreWindowController.GetControl().Clean();
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string msg = deleteMode ? BACK_DELETE_MODE : GO_DELETE_MODE;
            //Yes! I want to change!
            if (checkWindowChange(ALERT_TITLE, msg))
            {
                ScoreWindowController.MyScoreWindowController.GetControl().Clean();

                deleteMode = !deleteMode;
                if (deleteMode == false)
                {
                    //Sets the new controller
                    TransitionController.MyTransitionController.SetControl(pageTransitionControl);
                    //Loads the delete info screen
                    TransitionController.MyTransitionController.ShowUserControlPage(new SelectInfoControl(), WpfPageTransitions.PageTransitionType.Fade);
                }
                else
                {
                    //Sets the new controller
                    TransitionController.MyTransitionController.SetControl(pageTransitionControl);
                    //Loads the delete info screen
                    TransitionController.MyTransitionController.ShowUserControlPage(new DeleteInfoControl(), WpfPageTransitions.PageTransitionType.Fade);
                }
            }
        }

        private void lsvData_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lsvData.SelectedItem != null)
            {
                if (lsvData.SelectedItem.GetType() == typeof(Athlete))
                {
                    Athlete athlete = (Athlete)lsvData.SelectedItem;

                    //DELETE MODE
                    if (deleteMode)
                    {
                        MessageBoxResult result = MessageBox.Show("Sei sicuro di voler eliminare l'atleta " + athlete.Name + " " + athlete.Surname + "?",
                               "Conferma eliminazione",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            Competition.MyCompetition.RemoveAthlete(athlete);
                            ScoreWindowController.MyScoreWindowController.GetControl().Clean();
                        }
                        RefreshList();
                    }
                    //VIEW MODE
                    else
                    {
                        //If athlete has NO finished
                        if (!athlete.HasFinished)
                        {
                            //If I click on a DIFFERENT athlete on the list
                            if (previousSelectedItem != lsvData.SelectedItem)
                            {
                                //Yes! I want to change!
                                if (checkWindowChange(ALERT_TITLE, "Sei sicuro di voler passare a " + athlete.Name + " " + athlete.Surname + "?"))
                                {
                                    //Updates score window
                                    ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
                                    //Sets the new controller
                                    TransitionController.MyTransitionController.SetControl(pageTransitionControl);
                                    //Loads the delete info screen
                                    TransitionController.MyTransitionController.ShowUserControlPage(new TimerControl(athlete), WpfPageTransitions.PageTransitionType.Fade);

                                    //Clear data
                                    athlete.ClearData();
                                }

                                //Bog of you wanker, I don't wanna to change screen!
                                else
                                {
                                    lsvData.SelectedItem = previousSelectedItem;
                                }
                            }
                        }
                        else
                        {
                            //If I click on a DIFFERENT athlete on the list
                            if (previousSelectedItem != lsvData.SelectedItem)
                            {
                                //Yes! I want to change!
                                if (checkWindowChange(ALERT_TITLE, "Sei sicuro di voler passare a " + athlete.Name + " " + athlete.Surname + "?"))
                                {
                                    //Updates score window
                                    ScoreWindowController.MyScoreWindowController.GetControl().UpdateAthleteData(athlete);
                                    //Sets the new controller
                                    TransitionController.MyTransitionController.SetControl(pageTransitionControl);
                                    //Loads the finished screen
                                    TransitionController.MyTransitionController.ShowUserControlPage(new AthleteDoneControl(athlete), WpfPageTransitions.PageTransitionType.Fade);
                                }

                                //Bog of you wanker, I don't wanna to change screen!
                                else
                                {
                                    lsvData.SelectedItem = previousSelectedItem;
                                }
                            }
                        }
                    }
                }

                //Save prev item, in case I will no change
                previousSelectedItem = lsvData.SelectedItem;
            }
        }
        
        private bool checkWindowChange(string title, string message)
        {
            //Asking if I'm REALLY sure that I wanna to change screen
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            //Yes! I want to change!
            if (result == MessageBoxResult.Yes)
            {
                //Clear data
                //Check if some athlete was selected
                if (lsvData.SelectedItem != null)
                {
                    if (lsvData.SelectedItem.GetType() == typeof(Athlete))
                    {
                        Athlete athlete = (Athlete)lsvData.SelectedItem;
                        if (!athlete.HasFinished)
                            athlete.ClearData();
                    }
                }

                previousSelectedItem = null;

                GlobalTimer.MyGlobalTimer.Stop();
                GlobalTimer.MyGlobalTimerReset();

                return true;
            }

            return false;
        }
    }
}
