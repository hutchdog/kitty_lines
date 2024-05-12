using UnityEngine;

namespace Code
{
    [ExecuteInEditMode]
    public class Field : MonoBehaviour
    {
        private Tetramino currentFigure;

        private Vector2Int currentPosition;
        
        public Tetramino Figure => currentFigure;  
        
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
            currentFigure = new Tetramino(Tetramino.Type.L, 1);
            SetTetramino(4, 0, currentFigure);
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

        #region Whole tetramino ops

        protected void SetTetramino(int posX, int posY, Tetramino tetramino)
        {
            currentPosition.x = posX;
            currentPosition.y = posY;
            
            //TODO: Safety checks & skips
            int[,] figure = tetramino.GetFigure();

            for (int i = 0; i < 4; ++i)
            for (int j = 0; j < 4; ++j)
            {
                if (figure[i, j] != 0)
                    CreateElement(posX + i, posY + j, tetramino.GetFill());
            }
        }

        protected void RemoveTetramino(int posX, int posY, Tetramino tetramino)
        {
            //TODO: Safety checks & skips
            var figure = tetramino.GetFigure();

            for (var i = 0; i < 4; ++i)
            for (var j = 0; j < 4; ++j)
            {
                if (figure[i, j] != 0)
                    RemoveElement(posX + i, posY + j);
            }
        }

        protected bool CheckMoveTetramino(int posX, int posY, Tetramino tetramino)
        {
            //TODO: Safety checks & skips
            var figure = tetramino.GetFigure();

            for (var i = 0; i < 4; ++i)
            for (var j = 0; j < 4; ++j)
            {
                if (figure[i, j] != 0 && CheckFieldBorders(i + posX, j + posY) == false)
                    return false;
                
                if (figure[i, j] != 0 && GetElement(i + posX, j + posY) != null)
                    return false;
            }

            return true;
        }
        
        protected bool CheckRotateTetramino(int posX, int posY, Tetramino tetramino)
        {
            //TODO: Safety checks & skips
            var figure = tetramino.GetFigure();

            var newFigure = new int[4, 4];
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < 4; ++j)
                {
                    newFigure[i, j] = figure[j, 3 - i];
                }
            }
            
            for (var i = 0; i < 4; ++i)
            for (var j = 0; j < 4; ++j)
            {
                if (newFigure[i, j] != 0 && CheckFieldBorders(i + posX, j + posY) == false)
                    return false;
                
                if (newFigure[i, j] != 0 && GetElement(i + posX, j + posY) != null)
                    return false;
            }

            return true;
        }
        
        #endregion

        public void MoveLeft()
        {
            RemoveTetramino(currentPosition.x, currentPosition.y, currentFigure);
            if (CheckMoveTetramino(currentPosition.x - 1, currentPosition.y, currentFigure))
            {
                Debug.LogFormat("<b><color=yellow>Figure</color></b> moved left.");
                SetTetramino(currentPosition.x - 1, currentPosition.y, currentFigure);
            }
            else
            {
                SetTetramino(currentPosition.x, currentPosition.y, currentFigure);
            }
        }
        
        public void MoveRight()
        {
            RemoveTetramino(currentPosition.x, currentPosition.y, currentFigure);
            if (CheckMoveTetramino(currentPosition.x + 1, currentPosition.y, currentFigure))
            {
                Debug.LogFormat("<b><color=yellow>Figure</color></b> moved right.");
                SetTetramino(currentPosition.x + 1, currentPosition.y, currentFigure);
            }
            else
            {
                SetTetramino(currentPosition.x, currentPosition.y, currentFigure);
            }
        }
        
        public bool MoveDown()
        {
            RemoveTetramino(currentPosition.x, currentPosition.y, currentFigure);
            if (CheckMoveTetramino(currentPosition.x, currentPosition.y - 1, currentFigure))
            {
                Debug.LogFormat("<b><color=yellow>Figure</color></b> moved down.");
                SetTetramino(currentPosition.x, currentPosition.y - 1, currentFigure);
                return true;
            }

            SetTetramino(currentPosition.x, currentPosition.y, currentFigure);
            return false;
        }
        
        public bool RotateFigure()
        {
            if (currentFigure == null) return false;
            RemoveTetramino(currentPosition.x, currentPosition.y, currentFigure);
            if (CheckRotateTetramino(currentPosition.x, currentPosition.y, currentFigure))
            {
                Debug.LogFormat("<b><color=yellow>Figure</color></b> rotated.");
                currentFigure.Rotate();
                
            }
            
            SetTetramino(currentPosition.x, currentPosition.y, currentFigure);
            return false;
        }

        public void Drop(Tetramino tetramino)
        {
            if (tetramino == null) return;
            currentFigure = tetramino;
            SetTetramino(4, 20, currentFigure);
        }

        public void Set(int posX, int posY, Tetramino tetramino)
        {
            if (tetramino == null) return;
            currentFigure = tetramino;
            SetTetramino(posX, posY, tetramino);
        }
        
        public int CheckLines()
        {
            var lines = 0;
            for (var y = GetFieldHeight(); y >= 0; --y)
            {
                var elementCount = 0;
                for (var x = 0; x < GetFieldWidth(); ++x)
                {
                    if (GetElement(x, y) != null)
                    {
                        ++elementCount;
                    }
                }

                if (elementCount != GetFieldWidth()) continue;

                ++lines;
                
                for (var x = 0; x < GetFieldWidth(); ++x)
                {
                    RemoveElement(x, y);
                }

                for (var ty = y; ty < GetFieldHeight() - 1; ++ty)
                {
                    for (var x = 0; x < GetFieldWidth(); ++x)
                    {
                        MoveElement(x, ty + 1, x, ty);
                    }
                }
            }
            return lines;
        }
    }
}
