using System;

namespace CsenPoomsaeScore
{
    /// <summary>
    /// This class uses Singleton pattern to manage the transition controller of the application.
    /// </summary>
    class TransitionController
    {
        private static TransitionController transitionController;
        private WpfPageTransitions.PageTransition transitionControl;

        private TransitionController() { }

        public static TransitionController MyTransitionController
        {
            get
            {
                if (transitionController == null)
                {
                    transitionController = new TransitionController();
                }
                return transitionController;
            }
        }
        
        /// <summary>
        /// Sets the control for the transition handler.
        /// </summary>
        /// <param name="control">the page control</param>
        public void SetControl(WpfPageTransitions.PageTransition control)
        {
            transitionControl = control;
        }

        /// <summary>
        /// Show a new page in the controller with the specified transition.
        /// </summary>
        /// <param name="page">the new page</param>
        /// <param name="transitionType">the transition type</param>
        public void ShowUserControlPage(System.Windows.Controls.UserControl page, WpfPageTransitions.PageTransitionType transitionType)
        {
            if (transitionControl != null)
            {
                transitionControl.TransitionType = transitionType;
                transitionControl.ShowPage(page);
            } else
            {
                throw new NullReferenceException();
            }
        }
    }
}
