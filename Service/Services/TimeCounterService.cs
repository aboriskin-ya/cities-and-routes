using Service.Services.Interfaces;
using System.Diagnostics;

namespace Service.Services
{
    public class TimeCounterService : ITimeCounterService
    {
        private Stopwatch _timeCounter;

        public TimeCounterService()
        {
            _timeCounter = Stopwatch.StartNew();
        }

        public void Stop()
        {
            _timeCounter.Stop();
        }

        public string GetTime()
        {
            var seconds = _timeCounter.Elapsed.Seconds;
            var milliSeconds = _timeCounter.Elapsed.Milliseconds;
            return $"{seconds}s,{milliSeconds}ms.";
        }
    }
}