using System;
using System.Collections.Generic;
using Environment;
using UnityEngine;

namespace Interfaces
{
    [Serializable]
    public class Upgrades
    {
        public GameObject Stage;
        public int UpgradeCost; 
        public int Profit;
        public int ID;
    }
    
    public abstract class Upgradable: BaseComponent
    {
        [field: SerializeField] protected List<Upgrades> Upgrades { get; private set; } = new List<Upgrades>();
        [field: SerializeField] protected int CurrentUpgradeID { get; set; }
        
        public virtual void Upgrade()
        {
            var currentUpgradeIndex = Upgrades.FindIndex(u => u.ID == CurrentUpgradeID);
            
            if (currentUpgradeIndex == -1) return;
            
            currentUpgradeIndex++;
            if (currentUpgradeIndex < Upgrades.Count)
            {
                CurrentUpgradeID = Upgrades[currentUpgradeIndex].ID;
            }
        }

        protected virtual void UpdateView()
        {
            foreach (var item in Upgrades)
            {
                item.Stage.SetActive(item.ID == CurrentUpgradeID);
            }
        }
        
        public Upgrades GetCurrentUpgrade()
        {
            return Upgrades.Find(u => u.ID == CurrentUpgradeID);
        }

        public Upgrades GetNextUpgrade()
        {
            var currentUpgradeIndex = Upgrades.FindIndex(u => u.ID == CurrentUpgradeID);
            
            if (currentUpgradeIndex == -1 || currentUpgradeIndex >= Upgrades.Count - 1) return null;
            
            currentUpgradeIndex++;

            return Upgrades[currentUpgradeIndex];
        }
    }
}
