/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 22nd Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Configurations
{
    /// <summary>
    /// A central place to hold all the audio clips for the game.
    /// Makes it easy to manage and swap sounds.
    /// </summary>
    [CreateAssetMenu(fileName = "GameAudioConfig", menuName = "Scriptable Objects/Audio Configuration")]
    public class AudioSO : ScriptableObject
    {
        [Header("Background Music")]
        public AudioClip MainMenuMusic;
        public AudioClip GameplayMusic;
        public AudioClip GameOverMusic;
        public AudioClip VictoriousMusic; // For new high scores!

        [Header("Sound Effects")]
        public AudioClip ShootSfx;
        public AudioClip ThrustSfx; // This should be a seamless loop
        public AudioClip ExplosionSfx;
        public AudioClip PlayerDeathJingle;
    }
}