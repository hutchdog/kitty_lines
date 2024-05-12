using System;
using UnityEngine;

namespace Code
{
    public class CubeView : MonoBehaviour
    {
        public Renderer meshRenderer;

        public Material[] materials;
        
        private void Awake()
        {
            if (materials == null || materials.Length == 0)
            {
                throw new Exception("Materials for blocks are empty");
            }
        }
        
        public void SetFill(int fillValue)
        {
            if (fillValue is < 1 or > 3)
                return;

            if (meshRenderer)
            {
                meshRenderer.material = materials[fillValue - 1];
            }
        }
    }
}
