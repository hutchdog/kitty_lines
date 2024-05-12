using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace Code
{
    public class GameDirector : MonoBehaviour
    {
        private static GameDirector instance;
        private GameState gameStateHolder;

        public GameState GameState
        {
            get => gameStateHolder;
            private set => gameStateHolder = value;
        }

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(this);
                return;
            }

            instance = this;
            DontDestroyOnLoad(instance);
        }

        public static GameDirector Instance()
        {
            if (instance == null)
                throw new Exception("GameDirector Instance object should exist on start scene!");
            return instance;
        }
        
        private void Start()
        {
            GameState = GameState.MainMenu;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        #region Game State management

        public void StartGame()
        {
            if (GameState == GameState.MainMenu)
            {
                SceneManager.LoadScene("Scenes/Gameplay");
                GameState = GameState.Game;
            }
        }

        #endregion
        
    }
}