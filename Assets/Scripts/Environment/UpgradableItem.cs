using Constants;
using Cysharp.Threading.Tasks;
using Data;
using Interfaces;
using Managers;
using UnityEngine;

namespace Environment
{
    public class UpgradableItem : Upgradable, ISavable
    {
        [SerializeField] private ObjectData _objectData;
        [SerializeField] private ParticleSystem _upgradeEffect;

        private void Start()
        {
            Load();
            base.UpdateView();
        }

        public void Save()
        {
            SaveService.SaveItem(_objectData.ID, CurrentUpgradeID, Consts.SaveSystem.ItemPath).Forget();
        }

        public void Load()
        {
            var currentSave =  SaveService.LoadItem(_objectData.ID, Consts.SaveSystem.ItemPath);

            if (currentSave != null)
            {
                CurrentUpgradeID = currentSave.Value;
            }
        }
        
        public string GetItemID()
        {
            return _objectData.ID;
        }
        
        public override void Upgrade()
        {
            base.Upgrade();
            Save();
            UpdateView();
        }

        protected override void UpdateView()
        {
            _upgradeEffect.Play();
            
            base.UpdateView();
        }
    }
}
