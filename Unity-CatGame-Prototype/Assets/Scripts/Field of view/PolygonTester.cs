using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonTester : MonoBehaviour
{
    private MeshFilter filter;

    private void Awake()
    {
        gameObject.AddComponent(typeof(MeshRenderer));
        filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
    }

    public void SetMesh(Vector2[] points)
    {
        //// Create Vector2 vertices
        Vector2[] vertices2D = points;

        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        //gameObject.AddComponent(typeof(MeshRenderer));
        //MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;
    }

}
