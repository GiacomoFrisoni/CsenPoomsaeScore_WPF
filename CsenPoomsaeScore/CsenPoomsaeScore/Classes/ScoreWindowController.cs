namespace CsenPoomsaeScore
{
    class ScoreWindowController
    {
        private static ScoreWindowController scoreWindowController;
        private ScoreWindow scoreWindow;

        private ScoreWindowController() { }

        public static ScoreWindowController MyScoreWindowController
        {
            get
            {
                if (scoreWindowController == null)
                {
                    scoreWindowController = new ScoreWindowController();
                }
                return scoreWindowController;
            }
        }

        /// <summary>
        /// Sets the control for the score window handler.
        /// </summary>
        /// <param name="control">the score window control</param>
        public void SetControl(ScoreWindow control)
        {
            scoreWindow = control;
        }
        
        /// <summary>
        /// Returns the score window element.
        /// </summary>
        /// <returns>ScoreWindow</returns>
        public ScoreWindowInterface GetControl()
        {
            return scoreWindow;
        }
    }
}
