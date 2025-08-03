/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * 20th Jul 2025
 */

using System.Collections;
using UnityEngine;
using SuperLaggy.AsteroidsNeo.Core.Factories;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Obstacles
{
    /// <summary>
    /// Handles the spawning of asteroids in waves, with increasing difficulty over time.
    /// </summary>
    public class AsteroidSpawner : MonoBehaviour
    {
        [Header("Dependencies")]
        public AsteroidFactory AsteroidFactory;

        [Header("Spawning Configuration")]
        [SerializeField] private int initialAsteroidsPerWave = 2;
        [Tooltip("The time gap between each wave of asteroids.")]
        [SerializeField] private float waveInterval = 5f;
        [Tooltip("How many more asteroids to add to the wave when difficulty increases.")]
        [SerializeField] private int waveDifficultyIncrease = 2;
        [Tooltip("How often (in seconds) the difficulty should increase.")]
        [SerializeField] private float difficultyIncreaseInterval = 20f;

        [Space]
        [SerializeField] private float spawnRadius = 15f;

        private int currentAsteroidsPerWave;
        private int difficultyLevel = 1;
        private Coroutine spawningCoroutine;
        private Camera mainCamera;

        private readonly AsteroidSO.AsteroidSize[] spawnableSizes =
        {
        AsteroidSO.AsteroidSize.Large,
        AsteroidSO.AsteroidSize.Medium
    };

        void OnEnable()
        {
            GameEvents.OnPrepareNewGame += StartSpawning;
            GameEvents.OnGameSessionEnded += StopSpawning;
        }

        void OnDisable()
        {
            GameEvents.OnPrepareNewGame -= StartSpawning;
            GameEvents.OnGameSessionEnded -= StopSpawning;
        }

        void Start()
        {
            mainCamera = Camera.main;
        }

        public void StartSpawning()
        {
            currentAsteroidsPerWave = initialAsteroidsPerWave;
            difficultyLevel = 1;
            if (spawningCoroutine != null) StopCoroutine(spawningCoroutine);
            spawningCoroutine = StartCoroutine(SpawnRoutine());
        }

        public void StopSpawning()
        {
            if (spawningCoroutine != null) StopCoroutine(spawningCoroutine);
        }

        /// <summary>
        /// Manages the spawning of asteroid waves at regular intervals, increasing difficulty over time.
        /// </summary>
        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(2f);

            float _difficultyTimer = 0f;
            while (true)
            {
                for (int i = 0; i < currentAsteroidsPerWave; i++)
                {
                    SpawnAsteroid();
                }

                Debug.Log($"[AsteroidSpawner] Spawned Wave {difficultyLevel} of {currentAsteroidsPerWave} asteroids.");

                yield return new WaitForSeconds(waveInterval);

                _difficultyTimer += waveInterval;
                if (_difficultyTimer >= difficultyIncreaseInterval)
                {
                    _difficultyTimer = 0f;
                    IncreaseDifficulty();
                }
            }
        }

        /// <summary>
        /// Speeds up spawning by reducing the spawn interval.
        /// </summary>
        private void IncreaseDifficulty()
        {
            currentAsteroidsPerWave += waveDifficultyIncrease;
            difficultyLevel++;
            Debug.Log($"[AsteroidSpawner] Difficulty increased! Next wave will have {currentAsteroidsPerWave} asteroids.");
        }

        /// <summary>
        /// Create and spawn a new asteroid at a random position within the spawn radius.
        /// </summary>
        private void SpawnAsteroid()
        {
            int _randomIndex = Random.Range(0, spawnableSizes.Length);
            AsteroidSO.AsteroidSize _randomSize = spawnableSizes[_randomIndex];

            Vector2 _spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 _spawnPoint = _spawnDirection * spawnRadius;

            if (mainCamera != null)
            {
                Vector3 _viewportPoint = mainCamera.WorldToViewportPoint(_spawnPoint);
                if (_viewportPoint.x > 0 && _viewportPoint.x < 1 && _viewportPoint.y > 0 && _viewportPoint.y < 1)
                {
                    _spawnPoint *= 2;
                }
            }

            GameObject _asteroid = AsteroidFactory.Create(_randomSize);
            if (_asteroid != null)
            {
                _asteroid.transform.position = _spawnPoint;
            }
        }
    }
}