using System;
using System.Windows.Threading;

namespace CsenPoomsaeScore.Classes
{
    class GlobalTimer
    {
        private static DispatcherTimer timer;
        
        public static DispatcherTimer MyGlobalTimer
        {
            get
            {
                if (timer == null)
                {
                    timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
                }
                return timer;
            }
        }
        
        public static void MyGlobalTimerReset()
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }
    }
}
