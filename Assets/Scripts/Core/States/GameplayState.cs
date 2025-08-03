/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 18th Jul 2025
 */

using UnityEngine;

namespace SuperLaggy.AsteroidsNeo.Core.States
{
    /// <summary>
    /// Represents the active gameplay state.
    /// Manages the gameplay HUD, player input, and the pause/resume logic.
    /// </summary>
    public class GameplayState : MonoBehaviour, IGameState
    {
        private readonly GameManager gameManager;

        /// <summary>
        /// Initializes the GameplayState with necessary dependencies.
        /// </summary>
        public GameplayState(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        /// <summary>
        /// Called when entering gameplay. Activates the HUD, resets score, and starts spawning enemies.
        /// </summary>
        public void Enter()
        {
            Debug.Log("[GameplayState] Entering Gameplay State");
            gameManager.GameplayHudPanel.SetActive(true);
            gameManager.GameplaySystems.SetActive(true);
            gameManager.MainMenuPanel.SetActive(false);
            gameManager.GameOverPanel.SetActive(false);
            gameManager.PausePanel.SetActive(false);
            Time.timeScale = 1f;

            GameEvents.TriggerPrepareNewGame();
        }

        /// <summary>
        /// Per-frame logic during gameplay, primarily checking for pause input.
        /// </summary>
        public void Execute()
        {
            bool _pauseInput = Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape);
            if (_pauseInput)
            {
                gameManager.TogglePause();
            }
        }

        /// <summary>
        /// Called when exiting gameplay. Stops spawners, deactivates panels, and resets time scale.
        /// </summary>
        public void Exit()
        {
            Debug.Log("[GameplayState] Exiting Gameplay State");
            Time.timeScale = 1f;

            GameEvents.TriggerGameSessionEnded();

            gameManager.GameplayHudPanel.SetActive(false);
            gameManager.PausePanel.SetActive(false);
            gameManager.GameplaySystems.SetActive(false);
        }
    }
}