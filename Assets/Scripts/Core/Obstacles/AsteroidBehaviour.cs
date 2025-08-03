/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 2oth Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Core.Factories;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Obstacles
{
    /// <summary>
    /// Hanles all the behaviour for a single asteroid instance.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidController : MonoBehaviour, IDamageable, IPoolable
    {
        public AsteroidSO Config;

        private Rigidbody2D rb;
        private ObjectPool myPool;
        private AsteroidFactory myFactory;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // This gets called by the pool when the astroid is retrieved.
        void OnEnable()
        {
            float _speed = Random.Range(Config.MinSpeed, Config.MaxSpeed);
            Vector2 _direction = Random.insideUnitCircle.normalized;
            rb.linearVelocity = _direction * _speed;
            rb.angularVelocity = Random.Range(-100f, 100f);
        }

        /// <summary>
        /// This is called by an ammo when it hits the asteroid.
        /// </summary>
        public void TakeDamage(int _amount, bool _isHeavyAttack)
        {
            GameEvents.TriggerAsteroidDestroyed(Config, transform.position);

            if (Config.CanSplit)
            {
                Split();
            }

            myPool?.Return(this.gameObject);
        }

        /// <summary>
        /// Tells the factory to create smaller asteroids.
        /// </summary>
        private void Split()
        {
            if (myFactory == null || Config.SplitAsteroidType == null) return;

            int _splitCount = Random.Range(Config.MinSplitCount, Config.MaxSplitCount + 1);
            for (int i = 0; i < _splitCount; i++)
            {
                GameObject _splitAsteroid = myFactory.Create(Config.SplitAsteroidType.Size);
                if (_splitAsteroid != null)
                {
                    _splitAsteroid.transform.position = this.transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
                }
            }
        }

        /// <summary>
        /// This is called by the pool during initialization.
        /// </summary>
        public void SetPool(ObjectPool _pool)
        {
            myPool = _pool;
        }

        /// <summary>
        /// This is called by the factory right after creation.
        /// </summary>
        public void SetFactory(AsteroidFactory _factory)
        {
            myFactory = _factory;
        }

        // This is for when the asteroid collides with the player
        void OnCollisionEnter2D(Collision2D _collision)
        {
            if (_collision.gameObject.CompareTag("Player"))
            {
                if (_collision.gameObject.TryGetComponent<IDamageable>(out var _damageable))
                {
                    _damageable.TakeDamage((int)Config.DamageToPlayer, false);
                }

                GameEvents.TriggerAsteroidDestroyed(Config, transform.position);
                myPool?.Return(this.gameObject);
            }
        }
    }
}