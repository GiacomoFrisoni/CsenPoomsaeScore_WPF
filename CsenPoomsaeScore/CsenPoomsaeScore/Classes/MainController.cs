using CsenPoomsaeScore.UserControls;

namespace CsenPoomsaeScore.Classes
{
    class MainController
    {
        private static MainController mainController;
        private MainControlInterface mainControl;

        private MainController() { }

        public static MainController MyMainController
        {
            get
            {
                if (mainController == null)
                {
                    mainController = new MainController();
                }
                return mainController;
            }
        }

        /// <summary>
        /// Sets the control for the main handler.
        /// </summary>
        /// <param name="control">the main control</param>
        public void SetControl(MainControlInterface control)
        {
            mainControl = control;
        }

        /// <summary>
        /// Refrsh main tree.
        /// </summary>
        public void RefreshList()
        {
            mainControl.RefreshList();
        }
    }
}
