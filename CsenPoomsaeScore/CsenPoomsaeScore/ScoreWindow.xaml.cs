using System.Windows;
using CsenPomsaeScore;
using System.Runtime.InteropServices;
using System;
using System.Windows.Interop;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace CsenPoomsaeScore
{
    /// <summary>
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window, ScoreWindowInterface
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        
        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;
        const uint MF_ENABLED = 0x00000000;
        const uint SC_CLOSE = 0xF060;
        const int WM_SHOWWINDOW = 0x00000018;
        const int WM_CLOSE = 0x10;
        

        private const string no_info = "-";

        public ScoreWindow()
        {
            InitializeComponent();

            Clean();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.hwndSourceHook));
            }
        }

        IntPtr hwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SHOWWINDOW)
            {
                IntPtr hMenu = GetSystemMenu(hwnd, false);
                if (hMenu != IntPtr.Zero)
                {
                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                }
            }
            else if (msg == WM_CLOSE)
            {
                handled = true;
            }
            return IntPtr.Zero;
        }

        public void UpdateAthleteData(Athlete a)
        {
            grdDataTable.Visibility = Visibility.Visible;

            txblAthleteName.Text = (a.GetFullName() != null) ? a.GetFullName() : no_info;
            txblAthleteNameCenter.Text = (a.GetFullName() != null) ? a.GetFullName() : no_info;
            txblGym.Text = (a.Gym != null) ? a.Gym : no_info;
            txblTime.Text = (a.Time.HasValue) ? a.Time.Value.Minutes.ToString("00") + ":" + a.Time.Value.Seconds.ToString("00") + ":" + a.Time.Value.Milliseconds.ToString("000") : no_info;
            txblCategory.Text = a.GetCategoryDescription();
            txblGender.Text = a.GetGenderDescription();
            txblGrade.Text = a.Grade;

            if (a.PoomsaeCompleted)
            {
                txblTotal.Text = (a.TotalScore.HasValue) ? a.TotalScore.ToString() : no_info;
                vbAthleteNameCenter.Visibility = Visibility.Collapsed;
                vbTotal.Visibility = Visibility.Visible;
            }
            else
            {
                vbAthleteNameCenter.Visibility = Visibility.Visible;
                vbTotal.Visibility = Visibility.Collapsed;
            }

            string country_code = ConvertThreeLetterNameToTwoLetterName(a.Nationality);
            if (country_code != null)
            {
                imgNationality.Source = new BitmapImage(new Uri(@"/Images/Flags/" + country_code + ".png", UriKind.Relative));
            }

            stpJudgesPoints.Children.Clear();

            for (int i = 1; i <= Competition.MyCompetition.GetJudgeNumber(); i++)
            {
                StackPanel stp = new StackPanel();
                TextBlock txbTitle = new TextBlock();
                TextBlock txbJudge = new TextBlock();

                txbTitle.Text = "Giudice " + i;
                txbTitle.Style = (Style)Application.Current.FindResource("TextBlockTitleStyle");
                txbTitle.FontSize = 20;
                txbTitle.HorizontalAlignment = HorizontalAlignment.Center;

                txbJudge.Text = (a.JudgeVotes.Count == Competition.MyCompetition.GetJudgeNumber()) ? a.JudgeVotes[i - 1].ToString() : no_info;
                txbJudge.Style = (Style)Application.Current.FindResource("TextBlockTitleStyle");
                txbJudge.FontSize = 25;
                txbJudge.HorizontalAlignment = HorizontalAlignment.Center;

                stp.Margin = new Thickness(25, 15, 25, 15);
                stp.Children.Add(txbTitle);
                stp.Children.Add(txbJudge);

                stpJudgesPoints.Children.Add(stp);
            }
        }

        public void Clean()
        {
            grdDataTable.Visibility = Visibility.Collapsed;
        }

        private string ConvertThreeLetterNameToTwoLetterName(string name)
        {
            if (name.Length != 3)
            {
                throw new ArgumentException("name must be three letters");
            }

            name = name.ToUpper();

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                if (region.ThreeLetterISORegionName.ToUpper() == name)
                {
                    return region.TwoLetterISORegionName;
                }
            }

            return null;
        }
    }
}
