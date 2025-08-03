/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 18th Jul 2025
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SuperLaggy.AsteroidsNeo.Core;
using SuperLaggy.AsteroidsNeo.Core.Audio;
using SuperLaggy.AsteroidsNeo.Core.States;

namespace SuperLaggy.AsteroidsNeo.UI
{
    /// <summary>
    /// Manages all UI elements and their interactions. This class is responsible for updating the HUD, displaying final scores, and handlng all button clicks.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Gameplay HUD Elements")]
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI LivesText;

        [Header("Game Over Panel Elements")]
        public TextMeshProUGUI FinalScoreText;
        public TextMeshProUGUI HighScoreGameOverText;
        public TextMeshProUGUI HighScoreMainMenuText;

        [Header("Mute Button Elements")]
        public Button MainMenuMuteButton;
        public Button PauseMenuMuteButton;
        public Sprite MuteIcon;
        public Sprite UnmuteIcon;

        [Header("Misc")]
        public GameObject TutorialPanel;
        public GameObject CreditsPanel;

        private ScoreManager scoreManager;
        private AudioManager audioManager;

        void Awake()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        void OnEnable()
        {
            GameEvents.OnScoreUpdated += UpdateScoreText;
            GameEvents.OnPlayerHealthUpdated += UpdateLivesText;
            GameEvents.OnMuteStateChanged += UpdateMuteButtonIcons;
        }

        void OnDisable()
        {
            GameEvents.OnScoreUpdated -= UpdateScoreText;
            GameEvents.OnPlayerHealthUpdated -= UpdateLivesText;
            GameEvents.OnMuteStateChanged -= UpdateMuteButtonIcons;
        }

        private void UpdateScoreText(int _newScore)
        {
            if (ScoreText != null)
            {
                ScoreText.text = $"{_newScore}";
            }
        }

        private void UpdateLivesText(float _remainingLives)
        {
            if (LivesText != null)
            {
                LivesText.text = $"{_remainingLives}";
            }
        }

        public void DisplayMainMenuHighscore()
        {
            if (scoreManager == null) return;
            if (HighScoreMainMenuText != null) HighScoreMainMenuText.text = $"{scoreManager.HighScore}";
        }

        public void DisplayFinalScores()
        {
            if (scoreManager == null) return;
            if (FinalScoreText != null) FinalScoreText.text = $"{scoreManager.CurrentScore}";
            if (HighScoreGameOverText != null) HighScoreGameOverText.text = $"{scoreManager.HighScore}";
        }

        private void UpdateMuteButtonIcons(bool _isMuted)
        {
            Sprite _newSprite = _isMuted ? MuteIcon : UnmuteIcon;

            if (MainMenuMuteButton != null) MainMenuMuteButton.image.sprite = _newSprite;
            if (PauseMenuMuteButton != null) PauseMenuMuteButton.image.sprite = _newSprite;
        }

        #region Button Click Handlers
        /// <summary>
        ///  Requests a transition to GameplayState.
        /// </summary>
        public void OnPlayButton()
        {
            GameEvents.TriggerGameStateChange(typeof(GameplayState));
        }

        /// <summary>
        /// Requests a transition to GameplayState.
        /// </summary>
        public void OnRetryButton()
        {
            GameEvents.TriggerGameStateChange(typeof(GameplayState));
        }

        /// <summary>
        /// Requests a transition to MainMenuState.
        /// </summary>
        public void OnGoToMainMenuButton()
        {
            GameEvents.TriggerGameStateChange(typeof(MainMenuState));
        }

        public void OnOpenLinkedIn()
        {
            string linkedInUrl = "https://www.linkedin.com/in/ninjagurung/";
            Application.OpenURL(linkedInUrl);
        }

        public void ToggleTutorialPanel()
        {
            if (TutorialPanel != null)
            {
                bool _isActive = TutorialPanel.activeSelf;
                TutorialPanel.SetActive(!_isActive);
            }
        }

        public void ToggleCreditsPanel()
        {
            if (CreditsPanel != null)
            {
                bool _isActive = CreditsPanel.activeSelf;
                CreditsPanel.SetActive(!_isActive);
            }
        }

        public void OnMuteButton()
        {
            audioManager?.ToggleMute();
        }
        #endregion
    }
}