using System.Collections.Generic;
using Data;
using Managers;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UpgradeWindow : WindowBase
    {
        [SerializeField] private UpgradableItemPanel _panelPrefab;
        [SerializeField] private ObjectDataLibrary _dataLibrary;
        [SerializeField] private RectTransform _container;

        private List<UpgradableItemPanel> _panels = new List<UpgradableItemPanel>();
        private UpgradableItemsPool _itemsPool;
        private MoneyService _moneyService;

        [Inject]
        private void Construct(UpgradableItemsPool pool, MoneyService moneyService)
        {
            _itemsPool = pool;
            _moneyService = moneyService;
        }
        
        public override void OpenPanel()
        {
            MainPanel.SetActive(true);
            Clear();

            foreach (var item in _dataLibrary.ObjectDatas)
            {
                var instance = Instantiate(_panelPrefab, _container);
                instance.Init(item, _itemsPool, _moneyService);
                _panels.Add(instance);
            }
            
            base.OpenPanel();
        }

        private void Clear()
        {
            foreach (var item in _panels)
            {
                Destroy(item.gameObject);
            }
            
            _panels.Clear();
        }
    }
}
