/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core
{
    /// <summary>
    /// Manages the player's score and high score during gameplay.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        public int CurrentScore { get { return currentScore; } }
        public int HighScore { get { return highScore; } }
        private int currentScore;
        private int highScore;
        private const string HIGH_SCORE_KEY = "HighScore";

        void Awake()
        {
            highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        }

        void OnEnable()
        {
            GameEvents.OnAsteroidDestroyed += AddScore;
            //GameEvents.OnPlayerDied += SaveHighScore;
            GameEvents.OnPrepareNewGame += ResetScore;
        }

        void OnDisable()
        {
            GameEvents.OnAsteroidDestroyed -= AddScore;
            //GameEvents.OnPlayerDied -= SaveHighScore;
            GameEvents.OnPrepareNewGame -= ResetScore;
        }

        /// <summary>
        /// Updates the current score by adding the score value of the specified asteroid.
        /// </summary>
        private void AddScore(AsteroidSO _asteroidData, Vector3 _position)
        {
            currentScore += _asteroidData.ScoreValue;
            Debug.Log($"[ScoreManager] Score: {currentScore}");
            GameEvents.TriggerScoreUpdated(currentScore);
        }

        public void ResetScore()
        {
            currentScore = 0;
            GameEvents.TriggerScoreUpdated(currentScore);
        }

        /// <summary>
        /// Saves the current score as the new high score if it exceeds the existing high score.
        /// </summary>
        public void SaveHighScore()
        {
            if (currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
                PlayerPrefs.Save();
                GameEvents.TriggerNewHighScore();
                Debug.Log($"[ScoreManager] Congrats Pilot! New High Score: {highScore}");
            }
        }
    }
}