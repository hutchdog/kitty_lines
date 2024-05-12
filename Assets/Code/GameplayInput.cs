using Unity.VisualScripting;
using UnityEngine;

namespace Code
{
    public class GameplayInput
    {
        private KeyCode pressedKey = KeyCode.None;
        private bool pressedDrop = false;
        public bool MoveLeft => pressedKey == KeyCode.LeftArrow;
        public bool MoveRight => pressedKey == KeyCode.RightArrow;
        public bool Drop => pressedDrop;
        public bool Rotate => pressedKey == KeyCode.UpArrow;
        
        public void Update(float dt)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pressedKey = KeyCode.LeftArrow;
                Debug.LogFormat("<b><color=green>Key</color></b> {0} was pressed.", pressedKey.ToShortString());
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                pressedKey = KeyCode.RightArrow;
                Debug.LogFormat("<b><color=green>Key</color></b> {0} was pressed.", pressedKey.ToShortString());
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                pressedDrop = true;
                Debug.LogFormat("<b><color=green>Key</color></b> {0} was pressed.", pressedKey.ToShortString());
            }
            else
            {
                pressedDrop = false;
                Debug.LogFormat("<b><color=green>Key</color></b> {0} was unpressed.", KeyCode.DownArrow.ToShortString());
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                pressedKey = KeyCode.UpArrow;
                Debug.LogFormat("<b><color=green>Key</color></b> {0} was pressed.", pressedKey.ToShortString());
            }

            if (Input.GetKeyDown(KeyCode.None))
            {
                pressedKey = KeyCode.None;
            }
        }

        public void Reset()
        {
            pressedKey = KeyCode.None;
        }
    }
}
