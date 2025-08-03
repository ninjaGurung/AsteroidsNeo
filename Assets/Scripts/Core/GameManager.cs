/*
 * Authored by: Mahatva Gurung (SuperLaggy)
 * Date: 18th Jul 2025
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using SuperLaggy.AsteroidsNeo.Core.States;
using SuperLaggy.AsteroidsNeo.UI;
using SuperLaggy.AsteroidsNeo.Core.Obstacles;


namespace SuperLaggy.AsteroidsNeo.Core
{
    /// <summary>
    /// Manages the game's Finite State Machine (FSM).
    /// It pre-instantiates all possible game states and handles transitions
    /// based on events from the GameEvents system.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("UI Panels")]
        public GameObject MainMenuPanel;
        public GameObject GameplayHudPanel;
        public GameObject GameOverPanel;
        public GameObject PausePanel;

        [Header("Gameplay Objects")]
        public GameObject GameplaySystems;

        private Dictionary<Type, IGameState> states;
        private IGameState currentState;
        private bool isGamePaused = false;
        private ScoreManager scoreManager;

        void Awake()
        {
            var _uiManager = FindObjectOfType<UIManager>();
            var _asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
            scoreManager = FindObjectOfType<ScoreManager>();

            if (_uiManager == null || _asteroidSpawner == null)
            {
                Debug.LogError("[GameManager] A required manager dependency could not be found!");
            }

            states = new Dictionary<Type, IGameState>()
        {
            { typeof(MainMenuState), new MainMenuState(this, _uiManager) },
            { typeof(GameplayState), new GameplayState(this) },
            { typeof(GameOverState), new GameOverState(this, _uiManager) }
        };
        }

        /// <summary>
        /// Subscribes to the state change event when enabled.
        /// </summary>
        void OnEnable()
        {
            GameEvents.OnGameStateChangeRequest += ChangeState;
            GameEvents.OnPlayerDied += GoToGameOver;
        }

        /// <summary>
        /// Unsubscribes from the state change event when disabled.
        /// </summary>
        void OnDisable()
        {
            GameEvents.OnGameStateChangeRequest -= ChangeState;
            GameEvents.OnPlayerDied -= GoToGameOver;
        }

        /// <summary>
        /// Sets the initial state of the FSM.
        /// </summary>
        void Start()
        {
            // Set the initial state directly. No event needed for the very first state.
            //ChangeState(typeof(MainMenuState));
            GameEvents.TriggerGameStateChange(typeof(MainMenuState));
        }

        void Update()
        {
            currentState?.Execute();
        }

        /// <summary>
        /// Toggles the paused state of the game.
        /// This can be called from anywhere, like a UI button or player input.
        /// </summary>
        public void TogglePause()
        {
            // You can only pause during gameplay
            if (currentState is not GameplayState) return;

            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                Time.timeScale = 0f;
                PausePanel.SetActive(true);
                Debug.Log("[GameManager] Game Paused.");
            }
            else
            {
                Time.timeScale = 1f;
                PausePanel.SetActive(false);
                Debug.Log("[GameManager] Game Resumed.");
            }
        }

        /// <summary>
        /// Transitions the game to the GameOver state.
        /// </summary>
        private void GoToGameOver()
        {
            scoreManager?.SaveHighScore();
            GameEvents.TriggerGameStateChange(typeof(GameOverState));
        }

        /// <summary>
        /// The callback method that handles state transitions when an event is received.
        /// </summary>
        /// <param name="_newStateType">The Type of the new state to transition to.</param>
        private void ChangeState(Type _newStateType)
        {
            if (currentState != null && currentState.GetType() == _newStateType) return;

            currentState?.Exit();

            if (states.TryGetValue(_newStateType, out IGameState _newState))
            {
                currentState = _newState;
                currentState.Enter();
            }
            else
            {
                Debug.LogError($"[GameManager] State {_newStateType.Name} not found in the FSM.");
            }
        }
    }
}