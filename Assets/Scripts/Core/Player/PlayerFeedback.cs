/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.Core.Audio;

namespace SuperLaggy.AsteroidsNeo.Core.Player
{
    /// <summary>
    /// Manages all visual and audio feedback for the player ship.
    /// Listens to game events to trigger effects like particles, sound, and transparency.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerFeedback : MonoBehaviour
    {
        [Header("Component References")]
        public TrailRenderer ThrustTrail;
        public SpriteRenderer ShipSprite;
        public Collider2D Collider;

        [Header("Feedback Configuration")]
        public Color InvulnerableColor = new Color(1f, 1f, 1f, 0.5f);

        private Color originalColor;
        private IInputProvider inputProvider; // To check for thrust input
        private AudioManager audioManager;

        void Awake()
        {
            //ShipSprite = GetComponent<SpriteRenderer>();
            inputProvider = transform.parent.GetComponent<IInputProvider>();
            audioManager = FindObjectOfType<AudioManager>();

            originalColor = ShipSprite.color;

            if (ThrustTrail != null)
            {
                ThrustTrail.emitting = false;
            }
        }

        void OnEnable()
        {
            GameEvents.OnPlayerInvincibilityStarted += HandleInvulnerabilityStarted;
            GameEvents.OnPlayerInvincibilityEnded += HandleInvulnerabilityEnded;
        }

        void OnDisable()
        {
            GameEvents.OnPlayerInvincibilityStarted -= HandleInvulnerabilityStarted;
            GameEvents.OnPlayerInvincibilityEnded -= HandleInvulnerabilityEnded;
        }

        void Update()
        {
            HandleThrustEffect();
        }

        private void HandleThrustEffect()
        {
            if (ThrustTrail == null || inputProvider == null) return;

            bool isThrusting = inputProvider.MoveInput.y > 0;
            if (ThrustTrail.emitting != isThrusting)
            {
                ThrustTrail.emitting = isThrusting;
            }

            if (isThrusting)
            {
                audioManager?.StartThrustLoop();
            }
            else
            {
                audioManager?.StopThrustLoop();
            }
        }

        private void HandleInvulnerabilityStarted()
        {
            if (ShipSprite != null) ShipSprite.color = InvulnerableColor;
            if (Collider != null) Collider.enabled = false;
        }

        private void HandleInvulnerabilityEnded()
        {
            if (ShipSprite != null) ShipSprite.color = originalColor;
            if (Collider != null) Collider.enabled = true;
        }
    }
}