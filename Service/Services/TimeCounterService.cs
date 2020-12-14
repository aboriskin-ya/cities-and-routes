using Service.Services.Interfaces;
using System.Diagnostics;

namespace Service.Services
{
    public class TimeCounterService : ITimeCounterService
    {
        private Stopwatch _timeCounter;

        public TimeCounterService()
        {
            _timeCounter = new Stopwatch();
        }

        public void Start() => _timeCounter.Start();       

        public void Stop() => _timeCounter.Stop();

        public string GetTime()
        {
            var seconds = _timeCounter.Elapsed.Seconds;
            var milliSeconds = _timeCounter.Elapsed.Milliseconds;
            return $"{seconds}s,{milliSeconds}ms.";
        }
    }
}