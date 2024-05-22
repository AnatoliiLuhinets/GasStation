using System;
using System.Collections.Generic;
using Constants;
using Environment;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class MoneyService : MonoBehaviour
    {
        public event Action<int> MoneyCountUpdated;
        [field: SerializeField] public int MoneyCount { get; private set; } = 60;

        private List<Upgradable> _upgradables;
        private GasStation _gasStation;

        private void Awake()    
        {
            var loadedMoney = SaveService.LoadUserMoney(Consts.SaveSystem.UserProgress);
            MoneyCount = loadedMoney.HasValue ? loadedMoney.Value : MoneyCount; 

            _gasStation = FindObjectOfType<GasStation>();
            _gasStation.OnServiceEnd += CalculateRevenue;
            _upgradables = new List<Upgradable>(FindObjectsOfType<Upgradable>());
        }

        public void CalculateRevenue()
        {
            foreach (var upgradable in _upgradables)
            {
                var currentUpgrade = upgradable.GetCurrentUpgrade();
                MoneyCount += currentUpgrade.Profit;
            }

            SaveService.SaveUserMoney(MoneyCount, Consts.SaveSystem.UserProgress);

            MoneyCountUpdated?.Invoke(MoneyCount);
        }

        public void Spend(int value)
        {
            MoneyCount -= value;

            SaveService.SaveUserMoney(MoneyCount, Consts.SaveSystem.UserProgress);

            MoneyCountUpdated?.Invoke(MoneyCount);
        }

        private void OnDestroy()
        {
            if (_gasStation != null)
                _gasStation.OnServiceEnd -= CalculateRevenue;
        }
    }
}