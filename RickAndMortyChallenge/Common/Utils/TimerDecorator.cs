using System;
using System.Timers;

namespace Common.Utils
{
    public class TimerDecorator : Timer
    {
        private DateTime start;

        public void Iniciar()
        {
            start = DateTime.Now;
            this.Start();
        }

        public TimeSpan Detener()
        {
            base.Stop();
            TimeSpan elapsed = DateTime.Now - start;
            return elapsed;
        }
    }
}
