/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 21st Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Effects
{
    /// <summary>
    /// Listens for game events and creates effects. This act as both factory and manager for effects.
    /// </summary>
    public class EffectManager : MonoBehaviour
    {
        [Header("Dependencies")]
        public ObjectPool BlastEffectPool;

        void OnEnable()
        {
            GameEvents.OnAsteroidDestroyed += HandleAsteroidDestroyed;
        }

        void OnDisable()
        {
            GameEvents.OnAsteroidDestroyed -= HandleAsteroidDestroyed;
        }

        /// <summary>
        /// Creates a blast effect at the asteroid's last known position when an asteroid is destroyed.
        /// </summary>
        private void HandleAsteroidDestroyed(AsteroidSO _config, Vector3 _position)
        {
            CreateBlastEffect(_position);
        }

        /// <summary>
        /// Gets a blast effect from the pool and places it in the world.
        /// </summary>
        private void CreateBlastEffect(Vector3 _position)
        {
            if (BlastEffectPool == null) return;

            GameObject _effect = BlastEffectPool.Get();
            if (_effect != null)
            {
                _effect.transform.position = _position;
            }
        }
    }
}