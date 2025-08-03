/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 21st Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Core.Effects
{
    /// <summary>
    /// A simple controller for a pooled particle effect it returns itself to its pool when its finished playing.
    /// </summary>
    public class BlastEffectController : MonoBehaviour, IPoolable
    {
        private ObjectPool myPool;

        void OnDisable()
        {
            myPool?.Return(this.gameObject);
        }

        public void SetPool(ObjectPool _pool)
        {
            myPool = _pool;
        }
    }
}