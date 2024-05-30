using Environment;
using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private UpgradableItemsPool _pool;
        [SerializeField] private GasStation _gasStation;
        [SerializeField] private MoneyService _moneyService;
        public override void InstallBindings()
        {
            Container.Bind<SpawnService<BaseComponent>>().AsSingle(); 
            
            Container.Bind<UpgradableItemsPool>().FromInstance(_pool).AsSingle().NonLazy();
            Container.Bind<GasStation>().FromInstance(_gasStation).AsSingle().NonLazy();
            Container.Bind<MoneyService>().FromInstance(_moneyService).AsSingle().NonLazy();
        }
    }
}
