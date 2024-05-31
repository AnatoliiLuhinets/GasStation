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
            Load().Forget();
            base.UpdateView();
        }

        public async UniTask Save()
        {
            await SaveService.SaveItem(_objectData.ID, CurrentUpgradeID, Consts.SaveSystem.ItemPath);
        }

        public async UniTask Load()
        {
            var currentSave =  SaveService.LoadItem(_objectData.ID, Consts.SaveSystem.ItemPath);
            CurrentUpgradeID = currentSave;
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
