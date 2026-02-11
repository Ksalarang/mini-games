using System.Collections.Generic;
using TMPro;
using VContainer.Unity;

namespace Minigames.FlappyBird.Scripts
{
    public class PointController : ITickable
    {
        private readonly ITowerProvider towerProvider;
        private readonly TMP_Text pointLabel;
        private readonly Bird bird;
        private readonly List<Tower> towers;

        private int points;

        public PointController(ITowerProvider towerProvider, SceneContainer sceneContainer)
        {
            this.towerProvider = towerProvider;
            pointLabel = sceneContainer.PointLabel;
            bird = sceneContainer.Bird;
            towers = new List<Tower>();
        }

        void ITickable.Tick()
        {
            foreach (var tower in towerProvider.CurrentTowers)
            {
                if (tower == null
                    || tower.gameObject == null
                    || tower.IsBottom!
                    || towers.Contains(tower))
                {
                    continue;
                }

                if (bird.transform.position.x > tower.transform.position.x)
                {
                    towers.Add(tower);
                    points++;
                    pointLabel.SetText("{0}", points);
                    break;
                }
            }

            for (var i = towers.Count - 1; i >= 0; i--)
            {
                var tower = towers[i];

                if (tower == null || tower.gameObject == null)
                {
                    towers.RemoveAt(i);
                }
            }
        }

        public void Reset()
        {
            points = 0;
            pointLabel.SetText("{0}", points);
            towers.Clear();
        }
    }
}