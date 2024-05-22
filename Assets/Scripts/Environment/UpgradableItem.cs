using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Constants;
using Data;
using Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment
{
    public class UpgradableItem : Upgradable
    {
        [SerializeField] private ObjectData _objectData;
        [SerializeField] private ParticleSystem _upgradeEffect;

        private void Start()
        {
            Load();
            base.UpdateView();
        }

        private void Save()
        {
            SaveService.SaveItem(_objectData.ID, CurrentUpgradeID, Consts.SaveSystem.ItemPath);
        }

        private void Load()
        {
            var currentSave =  SaveService.LoadItem(_objectData.ID, Consts.SaveSystem.ItemPath);

            if (currentSave != null)
            {
                CurrentUpgradeID = currentSave.Value;
            }
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

        public string GetID()
        {
            return _objectData.ID;
        }
    }
}
