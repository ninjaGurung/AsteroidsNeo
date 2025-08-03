/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 18th Jul 2025
 */

using System;
using UnityEngine;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core
{
    /// <summary>
    /// A static class for managing global game events
    /// </summary>
    public static class GameEvents
    {
        // --- Game Events ---
        public static event Action<Type> OnGameStateChangeRequest;
        public static void TriggerGameStateChange(Type newStateType) => OnGameStateChangeRequest?.Invoke(newStateType);

        public static event Action OnPrepareNewGame;
        public static void TriggerPrepareNewGame() => OnPrepareNewGame?.Invoke();

        public static event Action OnGameSessionEnded;
        public static void TriggerGameSessionEnded() => OnGameSessionEnded?.Invoke();

        public static event Action OnNewHighScore;
        public static void TriggerNewHighScore() => OnNewHighScore?.Invoke();

        public static event Action<bool> OnMuteStateChanged;
        public static void TriggerMuteStateChanged(bool isMuted) => OnMuteStateChanged?.Invoke(isMuted);

        // --- Scoring Events ---
        public static event Action<int> OnScoreUpdated;
        public static void TriggerScoreUpdated(int newScore) => OnScoreUpdated?.Invoke(newScore);

        // --- Player Events ---
        public static event Action OnWeaponFired;
        public static void TriggerWeaponFired() => OnWeaponFired?.Invoke();

        public static event Action OnPlayerDied;
        public static void TriggerPlayerDied() => OnPlayerDied?.Invoke();

        public static event Action<float> OnPlayerHealthUpdated;
        public static void TriggerPlayerHealthUpdated(float currentHealth) => OnPlayerHealthUpdated?.Invoke(currentHealth);

        public static event Action OnPlayerInvincibilityStarted;
        public static void TriggerPlayerInvincibilityStarted() => OnPlayerInvincibilityStarted?.Invoke();

        public static event Action OnPlayerInvincibilityEnded;
        public static void TriggerPlayerInvincibilityEnded() => OnPlayerInvincibilityEnded?.Invoke();

        // --- Asteroid Events ---
        public static event Action<AsteroidSO, Vector3> OnAsteroidDestroyed;
        public static void TriggerAsteroidDestroyed(AsteroidSO _asteroidData, Vector3 _position) => OnAsteroidDestroyed?.Invoke(_asteroidData, _position);
    }
}