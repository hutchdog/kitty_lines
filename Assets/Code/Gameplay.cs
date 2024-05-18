using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    public class Gameplay : MonoBehaviour
    {
        public Field gameField;

        public UI gameUI;

        private GameplayInput gameplayInput = new();
        private GameplayState gameplayState = GameplayState.Undefined;
        private float gameplayTimer = 0.0f;
        private float dropMultiplier = 1.0f;
        private int score = 0;
        
        private void Start()
        {
            if (!gameField)
            {
                throw new System.Exception("Game Field is missing!");
            }

            if (!gameUI)
            {
                throw new System.Exception("Game UI is missing!");
            }

            gameField.ResetField();
            
            gameUI.ResetUI();
            gameUI.SetScoreText(score);

            gameplayState = GameplayState.Init;
        }

        private void Update()
        {
            var time = Time.deltaTime;
            
            Debug.LogFormat("<b><color=blue>State is </color></b> {0}.", gameplayState.ToShortString());
            
            switch (gameplayState)
            {
                case GameplayState.Init:
                {
                    gameField.ResetField();
                    gameField.SetDebugElements();

                    var newTetraminoColor = Random.Range(1, 4);
                    
                    gameplayState = GameplayState.Drop;
                    gameplayTimer = 0.0f;
                    
                    break;
                }
                case GameplayState.Drop:
                {
                    var newTetraminoColor = Random.Range(1, 4);
                    
                    gameplayState = GameplayState.UpdateInput;
                    gameplayTimer = 0.0f;
                    
                    break;
                }
                case GameplayState.UpdateInput:
                {
                    gameplayTimer += time;
                    if (gameplayTimer >= 0.5f * dropMultiplier)
                    {
                        gameplayTimer = 0.0f;
                        gameplayState = GameplayState.UpdateDrop;
                    }
                    else
                    {
                        HandleInput(time);
                    }
                    break;
                }
                case GameplayState.UpdateDrop:
                {
                    gameplayTimer += time;
                    if (gameplayTimer >= 0.5f * dropMultiplier)
                    {
                        var movedDown = gameField.MoveDown();
                        gameplayTimer = 0.0f;
                        gameplayState = movedDown ? GameplayState.UpdateInput : GameplayState.CheckLines;
                    }
                    else
                    {
                        HandleInput(time);
                    }
                    break;
                }
                case GameplayState.CheckLines:
                {
                    gameplayTimer = 0.0f;
                    var lines = gameField.CheckLines();
                    score += lines;

                    gameplayState = GameplayState.Drop;
                    gameUI.SetScoreText(score);
                    
                    break;
                }
            }
        }

        private void HandleInput(float time)
        {
            gameplayInput.Update(time);
            if (gameplayInput.MoveLeft)
            {
                gameField.MoveLeft();
            }

            if (gameplayInput.MoveRight)
            {
                gameField.MoveRight();
            }
            
            if (gameplayInput.Rotate)
            {
                gameField.RotateFigure();
            }
            
            dropMultiplier = gameplayInput.Drop ? 0.1f : 1.0f;
            
            gameplayInput.Reset();
        }
    }
}