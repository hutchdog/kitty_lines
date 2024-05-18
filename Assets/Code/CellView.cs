using System;
using UnityEngine;

namespace Code
{
    public class CellView : MonoBehaviour
    {
        public Renderer meshRenderer;

        public Material[] materials;

        private Camera mainCamera;
        
        private void Awake()
        {
            if (materials is not { Length: 2 })
            {
                throw new Exception("Materials for cell views are empty or incomplete!");
            }
            
            mainCamera = Camera.main;
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

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = mainCamera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.parent == transform)
                    {
                        Debug.LogFormat("<b><color=blue>Click on object </color></b> {0}.", this);    
                    }
                }
            }
        }
    }
}