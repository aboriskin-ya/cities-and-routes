namespace Service.Services.Interfaces
{
    public interface ITimeCounterService
    {
        void Start();
        void Stop();
        string GetTime();
    }
}