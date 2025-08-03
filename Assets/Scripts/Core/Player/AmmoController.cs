/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using System.Collections;
using UnityEngine;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Player
{
    /// <summary>
    /// Controls the behavior of a single ammo instance.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class AmmoController : MonoBehaviour, IPoolable
    {
        [Header("Configuration")]
        public AmmoSO AmmoConfig;

        private Rigidbody2D rb;
        private ObjectPool myPool;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Initializes the ammo's state and fires.
        /// </summary>
        public void Fire(Vector3 _position, Quaternion _rotation)
        {
            if (AmmoConfig == null) return;

            transform.position = _position;
            transform.rotation = _rotation;

            rb.linearVelocity = transform.up * AmmoConfig.Speed;

            StartCoroutine(LifetimeRoutine());
        }

        /// <summary>
        /// Coroutine that returns the ammo to the pool after its lifetime expires.
        /// </summary>
        private IEnumerator LifetimeRoutine()
        {
            yield return new WaitForSeconds(AmmoConfig.Lifetime);
            ReturnToPool();
        }

        /// <summary>
        /// Handles collision with other objects.
        /// </summary>
        void OnTriggerEnter2D(Collider2D _other)
        {
            if (_other.TryGetComponent<IDamageable>(out var _damageable))
            {
                _damageable.TakeDamage(AmmoConfig.Damage, false);
                ReturnToPool();
            }
        }

        /// <summary>
        /// Stores a reference to the pool this ammo came from.
        /// </summary>
        public void SetPool(ObjectPool _pool)
        {
            myPool = _pool;
        }

        /// <summary>
        /// Returns this ammo to its object pool.
        /// </summary>
        private void ReturnToPool()
        {
            StopAllCoroutines();
            rb.linearVelocity = Vector2.zero;

            if (myPool != null)
            {
                myPool.Return(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}