using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.SceneObjects;
using Minigames.Survivor.Scripts.UI;
using UnityEngine;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class GameOverSystem : IEcsRunSystem
    {
        private readonly IEcsHandler ecsHandler;
        private readonly SurvivorWorldContainer worldContainer;
        private readonly SurvivorGameOverScreen gameOverScreen;

        private readonly EcsFilter<PlayerTag> playerFilter;

        private bool gameOver;

        public GameOverSystem(IEcsHandler ecsHandler, SurvivorWorldContainer worldContainer, UiContainer uiContainer)
        {
            this.ecsHandler = ecsHandler;
            this.worldContainer = worldContainer;
            gameOverScreen = uiContainer.GameOverScreen;
        }

        public void Run()
        {
            if (gameOver)
            {
                return;
            }

            if (playerFilter.GetEntity(0).Get<Health>().Value <= 0f)
            {
                gameOver = true;
                ecsHandler.Active = false;

                gameOverScreen.RestartButton.onClick.RemoveListener(OnRestart);
                gameOverScreen.RestartButton.onClick.AddListener(OnRestart);
                gameOverScreen.FadeIn();
            }
        }

        private void OnRestart()
        {
            foreach (Transform enemy in worldContainer.Enemies)
            {
                Object.Destroy(enemy.gameObject);
            }

            foreach (Transform projectile in worldContainer.Projectiles)
            {
                Object.Destroy(projectile.gameObject);
            }

            ecsHandler.Destroy();
            ecsHandler.Initialize();

            gameOverScreen.FadeOut();
            ecsHandler.Active = true;
        }
    }
}
