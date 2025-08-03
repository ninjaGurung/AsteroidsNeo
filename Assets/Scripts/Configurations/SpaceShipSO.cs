/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Configurations
{
    /// <summary>
    /// Contains all configuration data for the player spaceship.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSpaceShipConfig", menuName = "Scriptable Objects/SpaceShip Configuration")]
    public class SpaceShipSO : ScriptableObject
    {
        [Header("Health")]
        public float MaxHealth = 5f;

        [Header("Movement")]
        public float Acceleration = 5f;
        public float MaxSpeed = 10f;
        public float RotationSpeed = 180f;
        public float LinearDrag = 0.5f;

        [Header("Shooting")]
        public float FireRate = 0.25f;
        public float HeavyAttackChargeTime = 3f;    //TODO: will add in future
    }
}
