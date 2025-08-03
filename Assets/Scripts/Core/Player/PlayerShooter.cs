/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Core.Factories;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Player
{
    /// <summary>
    /// Manages the player's shooting mechanics based on commands from a controller.
    /// Handles firing ammos at a specified fire rate using a factory and pool system.
    /// </summary>
    public class PlayerShooter : MonoBehaviour
    {
        [Header("Configuration")]
        [Tooltip("ScriptableObject containing the ship's shooting data.")]
        [SerializeField] private SpaceShipSO shipConfig;

        [Tooltip("The factory responsible for creating and pooling ammo objects.")]
        [SerializeField] private AmmoFactory ammoFactory;

        [Header("Object References")]
        [Tooltip("The transform where ammo will be spawned.")]
        [SerializeField] private Transform firePoint;

        public bool IsShooting { get; set; }

        private float lastFireTime;

        void Update()
        {
            if (IsShooting)
            {
                HandleShooting();
            }
        }

        /// <summary>
        /// Checks the fire rate timer and calls the FireAmmo method if allowed.
        /// </summary>
        private void HandleShooting()
        {
            if (shipConfig == null) return;

            if (Time.time > lastFireTime + shipConfig.FireRate)
            {
                FireAmmo();
                lastFireTime = Time.time;
            }
        }

        /// <summary>
        /// Requests an ammo from the factory and sets its initial position and rotation.
        /// </summary>
        private void FireAmmo()
        {
            if (ammoFactory == null)
            {
                Debug.LogError("[PlayerShooter] AmmoFactory is not assigned on PlayerShooter.");
                return;
            }

            GameObject _ammo = ammoFactory.Create();
            if (_ammo != null && firePoint != null)
            {
                _ammo.GetComponent<AmmoController>()?.Fire(firePoint.position, firePoint.rotation);
                GameEvents.TriggerWeaponFired();
            }
        }
    }
}