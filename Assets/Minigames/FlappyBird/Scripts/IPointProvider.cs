using System;

namespace Minigames.FlappyBird.Scripts
{
    public interface IPointProvider
    {
        event Action<int> PointsChanged;
        int Points { get; }
    }
}
