using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PrimitiveRenderer2D : MonoBehaviour
{
    [SerializeField] Material _material = null;
    Mesh _mesh;

    void Start()
    {
        _mesh = CreateQuad();
    }

    void OnRenderObject()
    {
        DrawRectAngle(new Rect(0, 0, 100, 100), true, Color.red);
    }

    public void DrawRectAngle(Rect rect, bool isFilled, Color color)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[4]
        {
            new Vector3(rect.xMin, rect.yMin, 0),
            new Vector3(rect.xMax, rect.yMin, 0),
            new Vector3(rect.xMax, rect.yMax, 0),
            new Vector3(rect.xMin, rect.yMax, 0)
        };
        mesh.uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        _material.SetColor("_Color", color);
        _material.SetPass(0);
        Graphics.DrawMeshNow(_mesh, transform.position, transform.rotation, LayerMask.NameToLayer("Default"));
    }

    Mesh CreateQuad()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[4]
        {
            new Vector3(-1, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, -1, 0)
        };
        mesh.uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };
        mesh.triangles = new int[6]
        {
            0, 1, 2,
            2, 3, 0
        };
        return mesh;
    }
}
