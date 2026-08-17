using System.Collections.Generic;
using System.Linq;
using Core.Tools;
using Leopotam.Ecs;
using Minigames.Survivor.Scripts.Configs.Upgrades;
using Minigames.Survivor.Scripts.Ecs.Components;
using Minigames.Survivor.Scripts.UI;

namespace Minigames.Survivor.Scripts.Ecs.Systems
{
    public class UpgradeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float ExpMultiplier = 1.5f;

        private readonly UpgradeBundleConfig bundleConfig;
        private readonly IEcsHandler ecsHandler;
        private readonly UpgradeCardSelectionView upgradeCardSelectionView;

        private readonly EcsWorld world;
        private readonly EcsFilter<PlayerTag> playerFilter;

        private EcsEntity player;

        public UpgradeSystem(UpgradeBundleConfig bundleConfig, IEcsHandler ecsHandler, UiContainer uiContainer)
        {
            this.bundleConfig = bundleConfig;
            this.ecsHandler = ecsHandler;
            upgradeCardSelectionView = uiContainer.UpgradeCardSelectionView;
        }

        public void Init()
        {
            player = playerFilter.GetEntity(0);
            player.Get<PlayerExpComponent>().NextLevelValue = bundleConfig.FirstLevelExp;
        }

        public void Run()
        {
            var playerExp = player.Get<PlayerExpComponent>();

            if (playerExp.CurrentValue >= playerExp.NextLevelValue)
            {
                ecsHandler.Active = false;

                var upgrades = GetUpgrades();

                for (var i = 0; i < 3; i++)
                {
                    var view = upgradeCardSelectionView.Cards[i];
                    var upgrade = upgrades[i];

                    view.Title.text = string.Format(upgrade.Title, upgrade.Level);
                    view.Description.text = string.Format(upgrade.Description, upgrade.Value);
                    view.Button.onClick.RemoveAllListeners();
                    view.Button.onClick.AddListener(() => OnUpgradeSelect(upgrade));
                }

                upgradeCardSelectionView.gameObject.SetActive(true);
            }
        }

        private List<UpgradeConfig> GetUpgrades()
        {
            var upgrades = bundleConfig.Upgrades.ToList();

            for (var i = upgrades.Count - 1; i >= 0; i--)
            {
                var upgrade = upgrades[i];

                if (!upgrade.IsApplicableTo(player))
                {
                    upgrades.Remove(upgrade);
                }
            }

            upgrades.Shuffle();

            return upgrades;
        }

        private void OnUpgradeSelect(UpgradeConfig config)
        {
            upgradeCardSelectionView.gameObject.SetActive(false);
            config.Apply(player, world);
            ref var playerExp = ref player.Get<PlayerExpComponent>();
            playerExp.CurrentValue = 0;
            playerExp.NextLevelValue = (int)(playerExp.NextLevelValue * ExpMultiplier);
            playerExp.Level++;
            ecsHandler.Active = true;
        }
    }
}
