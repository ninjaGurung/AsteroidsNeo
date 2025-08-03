/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 20th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Configurations
{
    /// <summary>
    /// Configuration data for a specific type of asteroid.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAsteroidConfig", menuName = "Scriptable Objects/Asteroid Configuration")]
    public class AsteroidSO : ScriptableObject
    {
        public enum AsteroidSize { Large, Medium, Small }

        [Header("Properties")]
        public GameObject AsteroidPrefab;
        public AsteroidSize Size;
        public float MinSpeed = 1f;
        public float MaxSpeed = 3f;
        public int ScoreValue = 10;
        public float DamageToPlayer = 1f;

        [Header("Splitting Behavior")]
        public bool CanSplit = true;
        [Tooltip("The type of asteroid this one splits into. Leave null if it doesnt split.")]
        public AsteroidSO SplitAsteroidType;
        public int MinSplitCount = 2;
        public int MaxSplitCount = 4;
    }
}
