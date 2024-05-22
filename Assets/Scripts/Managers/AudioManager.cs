using Common;
using Constants;
using UnityEngine;

namespace Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource _audioSource;

        private AudioManager()
        {
        }

        public void SetAudioSource(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public AudioSource TryGetAudioSource()
        {
            return _audioSource;
        }

        public void SetAudioState(bool state)
        {
            if (_audioSource == null)
            {
                return;
            }
            
            _audioSource.mute = state;
            
            PlayerPrefsExtensions.SetBool(Consts.AudioState.AudioStateKey, state);
        }

        public void SetVolume(float value)
        {  
            if (_audioSource == null)
            {
                return;
            }
            
            _audioSource.volume = value;
            
            PlayerPrefsExtensions.SetFloat(Consts.AudioState.AudioVolumeValueKey, value);
        }
    }
}
