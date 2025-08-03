/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 19th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Core.Player
{
    /// <summary>
    /// Provides player input based on Unity's legacy Input Manager (Keyboard/Mouse).
    /// </summary>
    public class PlayerInputProvider : MonoBehaviour, IInputProvider
    {
        public Vector2 MoveInput { get; private set; }
        public bool IsShooting { get; private set; }

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";

        void Update()
        {
            MoveInput = new Vector2(Input.GetAxis(HORIZONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS));
            IsShooting = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0);
        }
    }
}