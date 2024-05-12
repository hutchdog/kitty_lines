using System;
using UnityEngine;

namespace Code
{
    public class FieldView : MonoBehaviour
    {
        public Vector2 fieldSize;

        public GameObject cellPrefab;
        
        private void Awake()
        {
            //TODO: Failsafe settings, use camera sizes
            if (fieldSize.x == 0 || fieldSize.y == 0)
            {
                throw new Exception("Field view sizes cannot be zero!");
            }

            if (cellPrefab == null)
            {
                return;
            }
            
            for (var y = 0; y < fieldSize.y; ++y)
            {
                var startIndex = y % 2;
                for (var x = 0; x < fieldSize.x; ++x)
                {
                    var cellElement = Instantiate(cellPrefab, transform);
                    cellElement.transform.SetLocalPositionAndRotation(new Vector3(x - fieldSize.x / 2, y - fieldSize.y / 2, 0.1f), Quaternion.identity);

                    var cellView = cellElement.GetComponent<CellView>();
                    cellView.SetDark((startIndex + x) % 2 == 0);
                }
            }
        }
    }
}