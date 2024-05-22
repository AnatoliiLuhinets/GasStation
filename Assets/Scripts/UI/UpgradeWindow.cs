using System.Collections.Generic;
using Data;
using UnityEngine;

namespace UI
{
    public class UpgradeWindow : WindowBase
    {
        [SerializeField] private UpgradableItemPanel _panelPrefab;
        [SerializeField] private ObjectDataLibrary _dataLibrary;
        [SerializeField] private RectTransform _container;

        private List<UpgradableItemPanel> _panels = new List<UpgradableItemPanel>();
        
        public override void OpenPanel()
        {
            MainPanel.SetActive(true);
            Clear();

            foreach (var item in _dataLibrary.ObjectDatas)
            {
                var instance = Instantiate(_panelPrefab, _container);
                instance.Init(item);
                _panels.Add(instance);
            }
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
