/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 20tyh Jul 2025
 */

using System.Collections.Generic;
using UnityEngine;
using SuperLaggy.AsteroidsNeo.Core.Obstacles;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Factories
{
    /// <summary>
    /// A data-driven factory for creating different sizes of asteroids.
    /// It is configured with AsteroidSOs and dynamically creates the object pools it needs.
    /// </summary>
    public class AsteroidFactory : MonoBehaviour
    {
        public AsteroidSO LargeAsteroidConfig;
        public AsteroidSO MediumAsteroidConfig;
        public AsteroidSO SmallAsteroidConfig;
        public int InitialPoolSize = 15;

        private Dictionary<AsteroidSO.AsteroidSize, ObjectPool> asteroidPoolsDict;

        void Awake()
        {
            asteroidPoolsDict = new Dictionary<AsteroidSO.AsteroidSize, ObjectPool>();

            // Create and initialize a pool for each config
            InitializePoolFor(LargeAsteroidConfig);
            InitializePoolFor(MediumAsteroidConfig);
            InitializePoolFor(SmallAsteroidConfig);
        }

        void OnEnable()
        {
            GameEvents.OnPrepareNewGame += HandlePrepareNewGame;
        }

        void OnDisable()
        {
            GameEvents.OnPrepareNewGame -= HandlePrepareNewGame;
        }

        /// <summary>
        /// Creates a new child GameObject, adds an ObjectPool component, and initializes it.
        /// </summary>
        private void InitializePoolFor(AsteroidSO _config)
        {
            if (_config == null) return;

            GameObject _poolObject = new GameObject($"AsteroidPool_{_config.Size}");
            _poolObject.transform.SetParent(this.transform);

            ObjectPool _newPool = _poolObject.AddComponent<ObjectPool>();
            _newPool.Initialize(_config.AsteroidPrefab, InitialPoolSize);

            asteroidPoolsDict[_config.Size] = _newPool;
        }

        /// <summary>
        /// Creates an asteroid of a specific size by getting it from the correct pool.
        /// </summary>
        public GameObject Create(AsteroidSO.AsteroidSize _size)
        {
            if (asteroidPoolsDict.TryGetValue(_size, out ObjectPool _pool))
            {
                GameObject _asteroidObject = _pool.Get();
                if (_asteroidObject != null)
                {
                    // Inject this factory into the new asteroid so it can create splits
                    _asteroidObject.GetComponent<AsteroidController>()?.SetFactory(this);
                    return _asteroidObject;
                }
            }

            Debug.LogError($"[AsteroidFactory] No pool found for asteroid size: {_size}");
            return null;
        }

        /// <summary>
        /// Called when a new game session starts. It tells all managed pools to return their active objects.
        /// </summary>
        private void HandlePrepareNewGame()
        {
            if (asteroidPoolsDict == null) return;

            Debug.Log("[AsteroidFactory] Returning all active asteroids to pools.");
            foreach (var _pool in asteroidPoolsDict.Values)
            {
                _pool.ReturnAll();
            }
        }
    }
}