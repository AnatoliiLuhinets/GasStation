using System;
using System.Collections.Generic;
using Constants;
using Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.MainMenu
{
    [Serializable]
    public class MainMenuButtonData
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public int ID { get; private set; }
    }
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MainMenuButton _mainMenuButtonPrefab;
        [SerializeField] private List<MainMenuButtonData> _datas;
        [SerializeField] private Transform _container;
        [SerializeField] private TextMeshProUGUI _moneyCount;

        private List<MainMenuButton> _buttons = new List<MainMenuButton>();
        
        private SceneLoader _loader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _loader = sceneLoader;
            
            foreach (var data in _datas)
            {
                var instance = Instantiate(_mainMenuButtonPrefab, _container);
                instance.Initialize(data);
                instance.ButtonClicked += OnMainMenuButtonClicked;
                _buttons.Add(instance);
            }

            var loadedMoney = SaveService.LoadUserProgress(Consts.SaveSystem.UserProgress);
            _moneyCount.text = loadedMoney.HasValue ? loadedMoney.Value.ToString() : Consts.Values.DefaultMoneyCount.ToString();
        }

        private void OnMainMenuButtonClicked(ButtonBase button)
        {
            if (button is MainMenuButton menuButton)
            {
                switch (menuButton.ButtonID)
                {
                    case 1:
                        _loader.LoadScene(Consts.Scenes.GameScene);
                        break;
                    case 2:
                        Debug.Log("ShareButtonClicked"); //TODO don't forget to implement this later
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var item in _buttons)
            {
                item.ButtonClicked -= OnMainMenuButtonClicked;
            }
        }
    }
}
