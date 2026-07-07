using Core.Services.Storage;
using Core.Services.UnityAppEvents;

namespace Minigames.FlappyBird.Scripts
{
    public class PlayerDataStorage : IAppPauseListener
    {
        private const string PlayerDataKey = "FlappyBirdPlayerData";

        private readonly IStorage storage;
        private readonly IPointProvider pointProvider;

        public PlayerDataStorage(IStorage storage, IPointProvider pointProvider)
        {
            this.storage = storage;
            this.pointProvider = pointProvider;
        }

        public void SetMaxPoints()
        {
            var data = storage.Get<PlayerData>(PlayerDataKey);

            if (pointProvider.Points > data.MaxPoints)
            {
                data.MaxPoints = pointProvider.Points;
            }

            storage.Set(PlayerDataKey, data);
        }

        public int GetMaxPoints()
        {
            return storage.Get<PlayerData>(PlayerDataKey).MaxPoints;
        }

        public void OnAppPause(bool paused)
        {
            if (paused)
            {
                SetMaxPoints();
            }
        }
    }
}
