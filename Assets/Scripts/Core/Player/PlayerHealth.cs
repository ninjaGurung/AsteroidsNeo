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
    /// Manages the player's health, damage-taking, and invincibility state.
    /// </summary>
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("Configuration")]
        public SpaceShipSO ShipConfig;

        [Header("Component References")]
        public GameObject PlayerVisuals;
        public Collider2D ShipCollider;

        [Space]
        public float InvincibilityDuration = 3f;
        public ParticleSystem BlastEffect;

        private float currentHealth;
        private bool isInvincible = false;

        void Awake()
        {
            if (ShipConfig != null)
            {
                currentHealth = ShipConfig.MaxHealth;
            }
            else
            {
                Debug.LogError("[PlayerHealth] ShipConfig not assigned!");
            }
        }

        void OnEnable()
        {
            GameEvents.OnPrepareNewGame += ResetState;
        }

        void OnDisable()
        {
            GameEvents.OnPrepareNewGame -= ResetState;
        }

        void Start()
        {
            GameEvents.TriggerPlayerHealthUpdated(currentHealth);
        }

        /// <summary>
        /// Reduces player health and handles the consequences.
        /// </summary>
        public void TakeDamage(float _amount)
        {
            if (isInvincible) return;

            currentHealth -= _amount;
            GameEvents.TriggerPlayerHealthUpdated(currentHealth);
            Debug.Log($"[PlayerHealth] Damage: {_amount}. Health remaining: {currentHealth}");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameEvents.TriggerPlayerDied();

                PlayerDeathEffects();
            }
            else
            {
                StartCoroutine(InvincibilityRoutine());
            }
        }

        // Interface implementation
        public void TakeDamage(int _amount, bool _isHeavyAttack)
        {
            TakeDamage((float)_amount);
        }

        private IEnumerator InvincibilityRoutine()
        {
            isInvincible = true;
            GameEvents.TriggerPlayerInvincibilityStarted();

            yield return new WaitForSeconds(InvincibilityDuration);

            GameEvents.TriggerPlayerInvincibilityEnded();
            isInvincible = false;
        }

        public void PlayerDeathEffects()
        {
            if (PlayerVisuals) PlayerVisuals.SetActive(false);
            if (ShipCollider) ShipCollider.enabled = false;
            if (BlastEffect) BlastEffect.Play();
        }

        public void ResetState()
        {
            if (ShipConfig == null) return;
            currentHealth = ShipConfig.MaxHealth;
            isInvincible = false;

            if (PlayerVisuals != null && !PlayerVisuals.activeInHierarchy) PlayerVisuals.SetActive(true);
            if (ShipCollider) ShipCollider.enabled = true;
            if (BlastEffect)
            {
                BlastEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }

            GameEvents.TriggerPlayerHealthUpdated(currentHealth);
        }
    }
}