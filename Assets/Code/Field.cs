using UnityEngine;

namespace Code
{
    [ExecuteInEditMode]
    public class Field : MonoBehaviour
    {
        private Vector2Int currentPosition;
        
        virtual public int GetFieldWidth()
        {
            //TODO: refactor, reverse dependency?
            return (int)fieldView.fieldSize.x;
        }

        public float GetPhysicalWidth()
        {
            //TODO: Use a camera method here (or throw an exception)
            //Failsafe quit
            if (!fieldView)
                return 10.0f;

            return fieldView.fieldSize.x * fieldView.transform.localScale.x;
        }

        virtual public int GetFieldHeight()
        {
            //TODO: refactor, reverse dependency?
            return (int)fieldView.fieldSize.y;
        }

        public float GetPhysicalHeight()
        {
            //TODO: Use a camera method here (or throw an exception)
            //Failsafe quit
            if (!fieldView)
                return 24.0f;

            return fieldView.fieldSize.y * fieldView.transform.localScale.y;
        }

        public Vector3 GetPhysicalPosition()
        {
            return fieldView.transform.position;
        }

        public float GetCameraScale()
        {
            return (2.0f * cameraSize) / fieldView.fieldSize.y;
        }

        // So field is just an array
        private ViewElement[,] field = null;

        // Prefab links for child elements
        public GameObject cubePrefab;

        // Physical field gameObject
        public FieldView fieldView;

        // Camera size (to scale game field)
        protected float cameraSize;

        void Awake()
        {
            if (Camera.main)
            {
                cameraSize = Camera.main.orthographicSize;
            }
            else
            {
                // Random fail-safe value
                //TODO: Fix later 
                cameraSize = 12;
            }
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                DrawDebugGizmo();
            }
        }

        protected void DrawDebugGizmo()
        {
            var x = GetPhysicalPosition().x;
            var y = GetPhysicalPosition().y;

            var w = GetPhysicalWidth();
            var h = GetPhysicalHeight();
            Debug.DrawLine(new Vector3(x - w / 2.0f, y - h / 2.0f, 0), new Vector3(x - w / 2.0f, y + h / 2.0f, 0),
                Color.cyan);
            Debug.DrawLine(new Vector3(x - w / 2.0f, y + h / 2.0f, 0), new Vector3(x + w / 2.0f, y + h / 2.0f, 0),
                Color.cyan);
            Debug.DrawLine(new Vector3(x + w / 2.0f, y + h / 2.0f, 0), new Vector3(x + w / 2.0f, y - h / 2.0f, 0),
                Color.cyan);
            Debug.DrawLine(new Vector3(x + w / 2.0f, y - h / 2.0f, 0), new Vector3(x - w / 2.0f, y - h / 2.0f, 0),
                Color.cyan);
        }

        internal void ResetField()
        {
            if (field != null)
            {
                for (var i = 0; i < GetFieldWidth(); ++i)
                for (var j = 0; j < GetFieldHeight(); ++j)
                {
                    RemoveElement(i, j);
                }
            }

            field = new ViewElement[GetFieldWidth(), GetFieldHeight()];
            
            Debug.Log("<b><color=green>Field</color></b> was reset.");
        }

        public virtual void SetDebugElements()
        {
            CreateElement(3, 3, 2);
        }

        #region Single element ops

        protected ViewElement CreateElement(int posX, int posY, int fillValue)
        {
            if (!CheckFieldBorders(posX, posY))
                return null;
            
            var element = new ViewElement(cubePrefab);

            var fieldPos = new Vector2Int(posX, posY);
            const int size = 1;
            field[posX, posY] = element;
            //TODO: Remove this last magic offset, work in correct coords!
            element.CreateView(fieldView.transform, fieldPos.x - fieldView.fieldSize.x / 2,
                fieldPos.y - fieldView.fieldSize.y / 2, fillValue, size);
            return element;
        }

        protected void RemoveElement(int posX, int posY)
        {
            var element = field[posX, posY];
            if (element == null) return;

            element.DestroyView();
            field[posX, posY] = null;
        }

        protected bool CheckFieldBorders(int x, int y)
        {
            if (x < 0 || x >= GetFieldWidth())
                return false;

            if (y < 0 || y >= GetFieldHeight())
                return false;

            return true;
        }
        
        protected ViewElement GetElement(int x, int y)
        {
            if (!CheckFieldBorders(x, y))
                return null;

            return field[x, y];
        }

        protected bool CanMoveElement(int fromX, int fromY, int toX, int toY)
        {
            var elementFrom = GetElement(fromX, fromY);
            var elementTo = GetElement(toX, toY);

            if (elementFrom == null)
                return false;

            return elementTo == null;
        }

        protected bool MoveElement(int fromX, int fromY, int toX, int toY)
        {
            if (!CanMoveElement(fromX, fromY, toX, toY))
                return false;

            var elementFrom = GetElement(fromX, fromY);
            var fieldPos = new Vector2Int(toX, toY);
            
            elementFrom.MoveView(fieldPos.x - fieldView.fieldSize.x / 2,
                fieldPos.y - fieldView.fieldSize.y / 2);
            field[toX, toY] = elementFrom;
            field[fromX, fromY] = null;
            return true;
        }

        #endregion
        
        public void MoveLeft()
        {
        }
        
        public void MoveRight()
        {
        }
        
        public bool MoveDown()
        {
            return false;
        }
        
        public bool RotateFigure()
        {
            return false;
        }

        public int CheckLines()
        {
            return 0;
        }
    }
}
