/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 22nd Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Configurations;
using SuperLaggy.AsteroidsNeo.Core.States;

namespace SuperLaggy.AsteroidsNeo.Core.Audio
{
    /// <summary>
    /// Manages all game audio, including state-based music and sound effects.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("Configuration")]
        public AudioSO GameAudioConfig;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource thrustSource;

        [Header("SFX Pool")]
        public ObjectPool SfxPlayerPool;

        private bool isNewHighScoreSet = false;
        private const string MUTE_PREF_KEY = "IsMuted";

        void OnEnable()
        {
            GameEvents.OnGameStateChangeRequest += HandleGameStateChange;
            GameEvents.OnAsteroidDestroyed += HandleExplosion;
            GameEvents.OnPlayerDied += HandlePlayerDied;
            GameEvents.OnWeaponFired += HandleWeaponFired;
            GameEvents.OnNewHighScore += HandleNewHighScore;
        }

        void OnDisable()
        {
            GameEvents.OnGameStateChangeRequest -= HandleGameStateChange;
            GameEvents.OnAsteroidDestroyed -= HandleExplosion;
            GameEvents.OnPlayerDied -= HandlePlayerDied;
            GameEvents.OnWeaponFired -= HandleWeaponFired;
            GameEvents.OnNewHighScore -= HandleNewHighScore;
        }

        void Start()
        {
            bool _isMuted = PlayerPrefs.GetInt(MUTE_PREF_KEY, 0) == 1;
            AudioListener.volume = _isMuted ? 0f : 1f;

            GameEvents.TriggerMuteStateChanged(_isMuted);
        }

        private void HandleGameStateChange(System.Type _stateType)
        {
            if (_stateType == typeof(MainMenuState))
            {
                isNewHighScoreSet = false;
                PlayMusic(GameAudioConfig.MainMenuMusic);
            }
            else if (_stateType == typeof(GameplayState))
            {
                PlayMusic(GameAudioConfig.GameplayMusic);
            }
            else if (_stateType == typeof(GameOverState))
            {
                AudioClip _gameOverClip = isNewHighScoreSet ? GameAudioConfig.VictoriousMusic : GameAudioConfig.GameOverMusic;
                PlayMusic(_gameOverClip);
            }
        }

        private void PlayMusic(AudioClip _clip)
        {
            if (musicSource.clip == _clip) return;
            musicSource.clip = _clip;
            musicSource.Play();
        }

        private void HandleExplosion(AsteroidSO _so, Vector3 _pos) => PlaySfx(GameAudioConfig.ExplosionSfx);
        private void HandleWeaponFired() => PlaySfx(GameAudioConfig.ShootSfx);
        private void HandlePlayerDied()
        {
            StopThrustLoop();
            PlaySfx(GameAudioConfig.ExplosionSfx);
            PlaySfx(GameAudioConfig.PlayerDeathJingle);
        }
        private void HandleNewHighScore() => isNewHighScoreSet = true;

        /// <summary>
        /// Gets an SFX player from the pool and plays the given audio clip.
        /// </summary>
        private void PlaySfx(AudioClip _clip)
        {
            if (SfxPlayerPool == null || _clip == null) return;

            GameObject _sfxObject = SfxPlayerPool.Get();
            if (_sfxObject != null)
            {
                SfxPlayer _sfxPlayer = _sfxObject.GetComponent<SfxPlayer>();
                _sfxPlayer?.Play(_clip);
            }
        }

        public void StartThrustLoop()
        {
            if (thrustSource.isPlaying) return;
            thrustSource.clip = GameAudioConfig.ThrustSfx;
            thrustSource.Play();
        }

        public void StopThrustLoop()
        {
            thrustSource.Stop();
        }

        public void ToggleMute()
        {
            bool _isCurrentlyMuted = AudioListener.volume == 0f;
            bool _isNowMuted = !_isCurrentlyMuted;

            AudioListener.volume = _isNowMuted ? 0f : 1f;
            PlayerPrefs.SetInt(MUTE_PREF_KEY, _isNowMuted ? 1 : 0);
            PlayerPrefs.Save();

            GameEvents.TriggerMuteStateChanged(_isNowMuted);
        }
    }
}