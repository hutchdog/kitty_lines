using System;
using UnityEngine;

namespace Code
{
    public class CellView : MonoBehaviour
    {
        public Renderer meshRenderer;

        public Material[] materials;
        
        private void Awake()
        {
            if (materials == null || materials.Length != 2)
            {
                throw new Exception("Materials for cell views are empty or incomplete!");
            }
        }
        
        public void SetDark(bool dark)
        {
            if (meshRenderer)
            {
                if (dark)
                    meshRenderer.material = materials[0];
                else
                    meshRenderer.material = materials[1];
            }
        }
    }
}