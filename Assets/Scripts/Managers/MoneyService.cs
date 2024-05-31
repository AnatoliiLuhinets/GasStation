using System;
using System.Collections.Generic;
using Common;
using Constants;
using Cysharp.Threading.Tasks;
using Environment;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class MoneyService : MonoBehaviour
    {
        public event Action<int> MoneyCountUpdated;
        [field: SerializeField] private int MoneyCount { get; set; }

        private List<UpgradableItem> _upgradables;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, UpgradableItemsPool pool)
        {
            _upgradables = pool.GetAllItems();
            _signalBus = signalBus;
            _signalBus.Subscribe<EnvironmentSignals.OnServiceEnd>(CalculateRevenue);
            
            var loadedMoney = SaveService.LoadUserProgress(Consts.SaveSystem.UserProgress);
            MoneyCount = loadedMoney.HasValue ? loadedMoney.Value : Consts.Values.DefaultMoneyCount; 
        }

        private void CalculateRevenue()
        {
            foreach (var upgradable in _upgradables)
            {
                var currentUpgrade = upgradable.GetCurrentUpgrade();
                MoneyCount += currentUpgrade.Profit;
            }

            SaveService.SaveUserProgress(MoneyCount, Consts.SaveSystem.UserProgress).Forget();

            MoneyCountUpdated?.Invoke(MoneyCount);
        }

        public void Spend(int value)
        {
            MoneyCount -= value;

            SaveService.SaveUserProgress(MoneyCount, Consts.SaveSystem.UserProgress).Forget();

            MoneyCountUpdated?.Invoke(MoneyCount);
        }

        public int GetMoneyCount()
        {
            return MoneyCount;
        }
        

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<EnvironmentSignals.OnServiceEnd>(CalculateRevenue);
        }
    }
}