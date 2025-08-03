/*
 * author: Mahatva Gurung (SuperLaggy)
 * date: 19th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Configurations
{
    /// <summary>
    /// For configuring power-ups in the game. TODO: will implement power-up logic in future update
    /// </summary>
    [CreateAssetMenu(fileName = "NewPowerUpConfig", menuName = "Scriptable Objects/Power-up Configuration")]
    public class PowerUpSO : ScriptableObject
    {
        public enum PowerUpType { ExtraLife, Shield }
        public PowerUpType type;
        public float duration = 10f;
        public AudioClip pickupSound;
    }
}