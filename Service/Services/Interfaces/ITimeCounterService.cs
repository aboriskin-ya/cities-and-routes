namespace Service.Services.Interfaces
{
    public interface ITimeCounterService
    {
        void Stop();
        string GetTime();
    }
}