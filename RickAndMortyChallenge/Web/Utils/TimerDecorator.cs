using System;
using System.Timers;

namespace Web.Utils
{
    public class TimerDecorator
    {
        private DateTime start;
        private Timer timer;

        public TimerDecorator()
        {
            timer = new Timer();
        }

        public void Start()
        {
            start = DateTime.Now;
            timer.Start();
        }

        public TimeSpan Stop()
        {
            timer.Stop();
            TimeSpan elapsed = DateTime.Now - start;
            return elapsed;
        }
    }
}
