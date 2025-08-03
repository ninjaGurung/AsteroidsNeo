/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 18th Jul 2025
 */

using UnityEngine;
using SuperLaggy.AsteroidsNeo.UI;

namespace SuperLaggy.AsteroidsNeo.Core.States
{
    /// <summary>
    /// Represents the game over state, shown after the player has lost.
    /// </summary>
    public class GameOverState : IGameState
    {
        private readonly GameManager gameManager;
        private readonly UIManager uiManager;

        public GameOverState(GameManager gameManager, UIManager uiManager)
        {
            this.gameManager = gameManager;
            this.uiManager = uiManager;
        }

        public void Enter()
        {
            Debug.Log("[GameOverState] Entering Game Over State");
            gameManager.GameOverPanel.SetActive(true);
            gameManager.MainMenuPanel.SetActive(false);
            gameManager.GameplayHudPanel.SetActive(false);
            gameManager.PausePanel.SetActive(false);
            gameManager.GameplaySystems.SetActive(false);

            uiManager?.CreditsPanel.SetActive(false);
            uiManager?.DisplayFinalScores();
        }

        public void Execute() { }

        public void Exit()
        {
            Debug.Log("[GameOverState] Exiting Game Over State");
            gameManager.GameOverPanel.SetActive(false);
        }
    }
}