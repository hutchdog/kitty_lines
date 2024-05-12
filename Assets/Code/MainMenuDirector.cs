using UnityEngine;

namespace Code
{
    public class MainMenuDirector : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnPlayButtonPressed()
        {
            if (GameDirector.Instance().GameState == GameState.MainMenu)
            {
                GameDirector.Instance().StartGame();
            }
        }
    }
}
