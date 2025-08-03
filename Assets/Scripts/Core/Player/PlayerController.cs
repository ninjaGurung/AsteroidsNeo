/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Core.Player
{
    /// <summary>
    /// The main handler for the player spaceship. Gathers abstracted input and delegates actions to other components like PlayerMovement and PlayerShooter.
    /// </summary>
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerShooter), typeof(IInputProvider))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement movement;
        private PlayerShooter shooter;
        private IInputProvider input;
        private Rigidbody2D rb;

        void Awake()
        {
            movement = GetComponent<PlayerMovement>();
            shooter = GetComponent<PlayerShooter>();
            input = GetComponent<IInputProvider>();
            rb = GetComponent<Rigidbody2D>();
        }

        void OnEnable()
        {
            GameEvents.OnPrepareNewGame += ResetState;
        }

        void OnDisable()
        {
            GameEvents.OnPrepareNewGame -= ResetState;
        }

        void Update()
        {
            shooter.IsShooting = input.IsShooting;
        }

        void FixedUpdate()
        {
            movement.SetRotation(input.MoveInput.x);
            movement.SetThrustAndBrake(input.MoveInput.y);
        }

        public void ResetState()
        {
            if (!gameObject.activeInHierarchy) gameObject.SetActive(true);

            transform.position = Vector2.zero;
            transform.rotation = Quaternion.identity;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}