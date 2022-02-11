using System.Collections.Generic;
using UnityEngine;


namespace EpicKarters
{
    public class Spike : MonoBehaviour
    {
        Mesh mesh;
        MeshRenderer meshRenderer;

        List<Vector3> vertices;
        List<int> triangles;

        [SerializeField] Material material;
        [SerializeField] float height = 3.0f, radius = 5.0f;
        [SerializeField] int segments = 7;
       
        Vector3 pos;
        float angle = 0.0f, angleAmount = 0.0f;
       
        
        void SpikeShaper()
        {
            gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = material;
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            vertices = new List<Vector3>();
            pos = new Vector3();

            angleAmount = 2 * Mathf.PI / segments;
            angle = 0.0f;

            pos.x = 0.0f;
            pos.y = height;
            pos.z = 0.0f;
            vertices.Add(new Vector3(pos.x, pos.y, pos.z));

            pos.y = 0.0f;
            vertices.Add(new Vector3(pos.x, pos.y, pos.z));

            for (int i = 0; i < segments; i++)
            {
                pos.x = radius * Mathf.Sin(angle);
                pos.z = radius * Mathf.Cos(angle);

                vertices.Add(new Vector3(pos.x, pos.y, pos.z));

                angle -= angleAmount;
            }

            mesh.vertices = vertices.ToArray();

            triangles = new List<int>();

            for (int i = 2; i < segments + 1; i++)
            {
                triangles.Add(0);
                triangles.Add(i + 1);
                triangles.Add(i);
            }

            triangles.Add(0);
            triangles.Add(2);
            triangles.Add(segments + 1);

            mesh.triangles = triangles.ToArray();

        }


    }
}

