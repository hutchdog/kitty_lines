using UnityEngine;

namespace Code
{
    public class ViewElement
    {
        private readonly GameObject prefab;
        private GameObject viewObject;

        private float x;
        private float y;
        private int fillValue;

        public ViewElement(GameObject elementPrefab)
        {
            prefab = elementPrefab;
        }

        public void CreateView(Transform parent, float posX, float posY, int fillValue, float viewSize)
        {
            x = posX;
            y = posY;
            this.fillValue = fillValue;

            if (!prefab)
                throw new System.Exception("Cube prefab is missing!");

            viewObject = Object.Instantiate(prefab, parent);
            viewObject.transform.SetLocalPositionAndRotation(new Vector3(this.x, this.y, 0), Quaternion.identity);
            viewObject.transform.localScale = new Vector3(viewSize, viewSize, viewSize);

            var cubeView = viewObject.GetComponent<CubeView>();
            if (cubeView)
            {
                cubeView.SetFill(fillValue);
            }
        }

        public void DestroyView()
        {
            if (viewObject)
            {
                Object.Destroy(viewObject);
            }
        }

        public void MoveView(float posX, float posY)
        {
            x = posX;
            y = posY;
            if (viewObject != null)
            {
                viewObject.transform.SetLocalPositionAndRotation(new Vector3(this.x, this.y, 0), Quaternion.identity);
            }
        }
    }
}