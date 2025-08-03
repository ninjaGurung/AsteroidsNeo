/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Configurations;

namespace SuperLaggy.AsteroidsNeo.Core.Player
{
    /// <summary>
    /// Handles all physics-based movement and rotation for the player ship.
    /// It receives commands from a controller, not directly from player input.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Tooltip("ScriptableObject containing the ship's movement parameters.")]
        [SerializeField] private SpaceShipSO shipConfig;

        [Tooltip("Drag applied when the player is actively braking.")]
        [SerializeField] private float brakingDrag = 2f;

        private Rigidbody2D rb;
        private float defaultDrag;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            if (shipConfig != null)
            {
                defaultDrag = shipConfig.LinearDrag;
                rb.linearDamping = defaultDrag;
            }
            else
            {
                Debug.LogError("[PlayerMovement] SpaceShipSO configuration is not assigned.");
            }
        }

        /// <summary>
        /// Applies forward force for thrust or increases drag for braking.
        /// </summary>
        /// <param name="thrustInput">The forward/backward input axis (e.g., from -1 to 1).</param>
        public void SetThrustAndBrake(float thrustInput)
        {
            if (shipConfig == null) return;

            if (thrustInput > 0)
            {
                rb.linearDamping = defaultDrag;
                rb.AddForce(transform.up * shipConfig.Acceleration * thrustInput * Time.fixedDeltaTime);
            }
            else if (thrustInput < 0)
            {
                rb.linearDamping = brakingDrag;
            }
            else
            {
                rb.linearDamping = defaultDrag;
            }

            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, shipConfig.MaxSpeed);
        }

        /// <summary>
        /// Applies torque to rotate the ship.
        /// </summary>
        /// <param name="rotationInput">The rotation input axis (e.g., from -1 to 1).</param>
        public void SetRotation(float rotationInput)
        {
            if (shipConfig == null || rotationInput == 0) return;

            rb.MoveRotation(rb.rotation - rotationInput * shipConfig.RotationSpeed * Time.fixedDeltaTime);
        }
    }
}