using System;
using TMPro;
using UnityEngine;

namespace Code
{
    public class UI : MonoBehaviour
    {
        public TextMeshProUGUI scoreTextContainer;

        private void Awake()
        {
            if (scoreTextContainer == null)
            {
                throw new Exception("Score text container is not set!");
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        internal void ResetUI()
        {
            Debug.Log("<b><color=green>UI</color></b> was reset.");
        }

        public void SetScoreText(int value)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(String.Format("{0,8:D8}", value));
            scoreTextContainer.text = sb.ToString();
        }
    }
}