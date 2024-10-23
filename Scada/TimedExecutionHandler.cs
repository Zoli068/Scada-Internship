using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Scada
{
    public class TimedExecutionHandler:IDisposable
    {
        public Timer timer;
        private bool disposedValue;

        public TimedExecutionHandler(int interval) {
            timer = new Timer();
            timer.AutoReset = true;
            timer.Interval= interval;
        }

        public void StartTimer()
        { 
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
