/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Core
{
    /// <summary>
    /// Wraps a GO's position around the screen edges.
    /// When the object goes off one side, it reappears on the opposite side.
    /// </summary>
    public class ScreenWrapper : MonoBehaviour
    {
        private Camera mainCamera;
        private Vector2 screenMin;
        private Vector2 screenMax;
        private float objectWidth;
        private float objectHeight;

        /// <summary>
        /// Caches the camera and calculates screen boundaries in world units.
        /// </summary>
        void Start()
        {
            mainCamera = Camera.main;

            screenMin = mainCamera.ScreenToWorldPoint(Vector3.zero);
            screenMax = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                objectWidth = spriteRenderer.bounds.extents.x;
                objectHeight = spriteRenderer.bounds.extents.y;
            }
        }

        /// <summary>
        /// Checks the object's position each frame and wraps it if necessary.
        /// </summary>
        void Update()
        {
            Vector3 _newPosition = transform.position;

            if (_newPosition.x < screenMin.x - objectWidth) _newPosition.x = screenMax.x + objectWidth;
            else if (_newPosition.x > screenMax.x + objectWidth) _newPosition.x = screenMin.x - objectWidth;

            if (_newPosition.y < screenMin.y - objectHeight) _newPosition.y = screenMax.y + objectHeight;
            else if (_newPosition.y > screenMax.y + objectHeight) _newPosition.y = screenMin.y - objectHeight;

            transform.position = _newPosition;
        }
    }
}