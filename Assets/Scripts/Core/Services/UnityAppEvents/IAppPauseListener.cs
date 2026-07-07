namespace Core.Services.UnityAppEvents
{
    public interface IAppPauseListener
    {
        void OnAppPause(bool paused);
    }
}
