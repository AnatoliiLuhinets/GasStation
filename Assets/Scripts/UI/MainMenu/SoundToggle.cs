using Common;
using Constants;
using Managers;
using UnityEngine;

namespace UI.MainMenu
{
    public class SoundToggle : ToggleBase
    {
        [SerializeField] private AudioSource _audioSource;

        protected override void OnAwake()
        {
            if (_audioSource != null && AudioManager.Instance.TryGetAudioSource() == null)
            {
                AudioManager.Instance.SetAudioSource(_audioSource);
                DontDestroyOnLoad(_audioSource.gameObject);
            }
            else if(_audioSource == null && AudioManager.Instance.TryGetAudioSource() != null)
            {
                _audioSource = AudioManager.Instance.TryGetAudioSource();
            }
            
            if (PlayerPrefsExtensions.HasKey(Consts.AudioState.AudioStateKey))
            {
                var isOn = PlayerPrefsExtensions.GetBool(Consts.AudioState.AudioStateKey);

                Toggle.isOn = isOn;
                _audioSource.mute = isOn;
            }
            
            base.OnAwake();
        }
        protected override void OnToggleClicked(bool isActive)
        {
            AudioManager.Instance.SetAudioState(isActive);
            base.OnToggleClicked(isActive);
        }
    }
}
