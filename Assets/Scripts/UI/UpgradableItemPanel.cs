using Data;
using Environment;
using Interfaces;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradableItemPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private TextMeshProUGUI _profit;
        [SerializeField] private TextMeshProUGUI _upgrade;

        private UpgradableItem _upgradableItem;
        private MoneyService _moneyService;
        private Upgrades _nextUpgrade;
        private ObjectData _objectData;
        
        public void Init(ObjectData objectData, UpgradableItemsPool pool, MoneyService moneyService)
        {
            _objectData = objectData;
            _upgradableItem = pool.GetItem(objectData);
            _moneyService = moneyService;

            _moneyService.MoneyCountUpdated += UpdateInteractable;

            UpdateView();
            
            _button.onClick.AddListener(TryUpgrade);
        }

        private void UpdateView()
        {
            var upgrade = _upgradableItem.GetNextUpgrade();
            _nextUpgrade = upgrade;

            if (upgrade == null)
            {
                upgrade = _upgradableItem.GetCurrentUpgrade();
            }
            
            _icon.sprite = _objectData.Icon;
            _price.text = upgrade.UpgradeCost.ToString();
            _profit.text = upgrade.Profit.ToString();
            _upgrade.text = _upgradableItem.GetCurrentUpgrade().ID.ToString();
            _name.text = _objectData.Name;
        }

        private void TryUpgrade()
        {
            if (_nextUpgrade == null)
            {
                return;
            }
                
            if (_moneyService.GetMoneyCount() >= _nextUpgrade.UpgradeCost)
            {
                _upgradableItem.Upgrade();
                _moneyService.Spend(_nextUpgrade.UpgradeCost);
                UpdateView();
            }
        }

        private void UpdateInteractable(int money)
        {
            if (!_moneyService || _nextUpgrade == null)
            {
                _button.interactable = false;
                return;
            }

            _button.interactable = money >= _nextUpgrade.UpgradeCost && _nextUpgrade != null;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            _moneyService.MoneyCountUpdated -= UpdateInteractable;
        }
    }
}
