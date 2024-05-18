using System;
using UnityEngine;

namespace Code
{
    public class CubeView : MonoBehaviour
    {
        public Renderer meshRenderer;

        private static Material[] materials;
        
        private void Awake()
        {
            if (materials == null)
            {
                materials = new Material[4];

                materials[0] = new Material(Shader.Find("Diffuse"))
                {
                    color = Color.black // Just to find bugs? 
                };

                materials[1] = new Material(Shader.Find("Diffuse"))
                {
                    color = Color.red
                };

                materials[2] = new Material(Shader.Find("Diffuse"))
                {
                    color = Color.green
                };

                materials[3] = new Material(Shader.Find("Diffuse"))
                {
                    color = Color.blue
                };
            }
        }
        
        public void SetFill(int fillValue)
        {
            if (fillValue is < 1 or > 3)
                return;

            if (meshRenderer)
            {
                meshRenderer.material = materials[fillValue];
            }
        }
    }
}
