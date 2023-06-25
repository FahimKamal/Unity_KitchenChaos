using UnityEngine;

namespace GameScripts{
    public class MusicManager : MonoBehaviour{

        public static MusicManager Instance{ get; private set; }

        public const string MUSIC_VOLUME_KEY = "MusicVolume";

        private float _volume = 0.3f;
        private AudioSource _audioSource;

        private void Awake(){
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, _volume);
            _audioSource.volume = _volume;
        }

        public void ChangeVolume(){
            _volume += 0.1f;
            if (_volume > 1.0f) _volume = 0f;
            _audioSource.volume = _volume;
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, _volume);
            PlayerPrefs.Save();
        }

        public float GetVolume => _volume;
    }
}