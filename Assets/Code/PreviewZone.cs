using UnityEngine;

namespace Code
{
    [ExecuteInEditMode]
    public class PreviewZone : Field
    {
        public override int GetFieldWidth()
        {
            return 4;
        }

        public override int GetFieldHeight()
        {
            return 4;
        }

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (Application.isEditor)
            {
                DrawDebugGizmo();
            }
        }

        public override void SetDebugElements()
        {
            var lTest = new Tetramino(Tetramino.Type.O, 2);
            SetTetramino(0, 0, lTest);
        }
    }
}
