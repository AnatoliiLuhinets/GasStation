using Common;
using Constants;
using Managers;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private SoundToggle _soundToggle;
        [SerializeField] private Slider _soundsSlider;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        protected override void OnAwake()
        {
            if(PlayerPrefsExtensions.HasKey(Consts.AudioState.AudioVolumeValueKey))
            {
                _soundsSlider.value = PlayerPrefsExtensions.GetFloat(Consts.AudioState.AudioVolumeValueKey);
            }

            if (PlayerPrefsExtensions.HasKey(Consts.AudioState.AudioStateKey))
            {
                AudioManager.Instance.SetAudioState(PlayerPrefsExtensions.GetBool(Consts.AudioState.AudioStateKey));
            }
            
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _soundsSlider.onValueChanged.AddListener(OnSoundsSliderValueChanged);
            
            base.OnAwake();
        }
        
        private void OnSoundsSliderValueChanged(float value)
        {
            AudioManager.Instance.SetVolume(value);

            _soundToggle.ChangeActiveState(value == 0);
        }

        private void OnExitButtonClicked()
        {
            _sceneLoader.LoadScene(Consts.Scenes.MainMenuScene);
        }

        protected override void Destroyed()
        {
            _exitButton.onClick.RemoveAllListeners();
            _soundsSlider.onValueChanged.RemoveAllListeners();
            
            base.Destroyed();
        }

        public override void OpenPanel()
        {
            MainPanel.SetActive(true);
            base.OpenPanel();
        }
    }
}
