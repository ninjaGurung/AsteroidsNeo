/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 18th Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.UI;

namespace SuperLaggy.AsteroidsNeo.Core.States
{
    /// <summary>
    /// Represents the main menu state of the game.
    /// Manages the main menu UI and initial setup.
    /// </summary>
    public class MainMenuState : IGameState
    {
        private readonly GameManager gameManager;
        private readonly UIManager uiManager;

        /// <summary>
        /// Initializes the MainMenuState with necessary dependencies.
        /// </summary>
        public MainMenuState(GameManager gameManager, UIManager uiManager)
        {
            this.gameManager = gameManager;
            this.uiManager = uiManager;
        }

        /// <summary>
        /// Called when entering the main menu state.
        /// </summary>
        public void Enter()
        {
            Debug.Log("[MainMenuState] Entering Main Menu State");
            gameManager.MainMenuPanel.SetActive(true);
            gameManager.GameplayHudPanel.SetActive(false);
            gameManager.GameOverPanel.SetActive(false);
            gameManager.PausePanel.SetActive(false);
            gameManager.GameplaySystems.SetActive(false);

            uiManager?.DisplayMainMenuHighscore();
            uiManager?.TutorialPanel.SetActive(false);
            // Example: Tell AudioManager to play menu music
        }

        public void Execute()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                uiManager?.ToggleTutorialPanel();
            }
        }

        /// <summary>
        /// Called when exiting the main menu state.
        /// </summary>
        public void Exit()
        {
            Debug.Log("[MainMenuState] Exiting Main Menu State");
            gameManager.MainMenuPanel.SetActive(false);
        }
    }
}
