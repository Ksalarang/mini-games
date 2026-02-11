using System.Collections.Generic;

namespace Minigames.FlappyBird.Scripts
{
    public interface ITowerProvider
    {
        IReadOnlyList<Tower> CurrentTowers { get; }
    }
}