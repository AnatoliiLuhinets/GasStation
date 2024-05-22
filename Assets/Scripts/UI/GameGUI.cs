using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameGUI : MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private TextMeshProUGUI _coins;
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private UpgradeWindow _upgradeWindow;

        private MoneyService _moneyService;

        private void Start()
        {
            _moneyService = FindObjectOfType<MoneyService>();
            _coins.text = _moneyService.MoneyCount.ToString();

            _moneyService.MoneyCountUpdated += OnMoneyCountUpdated;
            
            _settingsButton.onClick.AddListener(()=> _settingsWindow.OpenPanel());
            _upgradeButton.onClick.AddListener(()=> _upgradeWindow.OpenPanel());
        }

        private void OnMoneyCountUpdated(int value)
        {
            _coins.text = value.ToString();
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveAllListeners();
            _upgradeButton.onClick.RemoveAllListeners();
            
            _moneyService.MoneyCountUpdated -= OnMoneyCountUpdated;
        }
    }
}
