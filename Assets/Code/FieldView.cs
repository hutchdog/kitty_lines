using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class FieldView : MonoBehaviour
    {
        private MeshFilter meshFilter;

        private MeshRenderer meshRenderer;

        private Mesh mesh;

        private Material defaultMaterial;

        public Vector2 fieldSize;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            mesh = new Mesh();

            meshRenderer = GetComponent<MeshRenderer>();

            defaultMaterial = new Material(Shader.Find("Diffuse"));

            //TODO: Failsafe settings, use camera sizes
            if (fieldSize.x != 0 && fieldSize.y != 0) return;

            fieldSize.x = 10;
            fieldSize.y = 12;
        }

        private void Start()
        {
            var vertices = new Vector3[4];
            var width = 0.5f * fieldSize.x;
            var height = 0.5f * fieldSize.y;

            vertices[0] = new Vector3(-width, height, 0);
            vertices[1] = new Vector3(width, height, 0);
            vertices[2] = new Vector3(-width, -height, 0);
            vertices[3] = new Vector3(width, -height, 0);

            mesh.SetVertices(vertices);

            Vector3[] normals = new Vector3[4];
            normals[0] = new Vector3(0, 0, -1);
            normals[1] = new Vector3(0, 0, -1);
            normals[2] = new Vector3(0, 0, -1);
            normals[3] = new Vector3(0, 0, -1);

            mesh.SetNormals(normals);

            var indices = new int[6];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 2;
            indices[4] = 1;
            indices[5] = 3;

            mesh.SetIndices(indices, MeshTopology.Triangles, 0);

            meshFilter.mesh = mesh;

            meshRenderer.material = defaultMaterial;
        }

        private void Update()
        {
        }
    }
}