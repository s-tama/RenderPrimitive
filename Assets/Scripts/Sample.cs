using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Sample : MonoBehaviour
{
    [SerializeField] Material _material = null;
    [SerializeField] Font _font = null;
    Mesh _mesh;

    void Start()
    {
        _mesh = CreateQuad();
        DrawText("Hello World!", Color.red);
    }

    void OnRenderObject()
    {
        //DrawRectAngle(new Rect(-1, -1, 2, 2), true, Color.red);
    }

    public void DrawText(string str, Color color)
    {
        TextGenerationSettings settings = new TextGenerationSettings();
        settings.textAnchor = TextAnchor.UpperLeft;
        settings.color = color;
        settings.generationExtents = new Vector2(500, 200);
        settings.pivot = Vector2.zero;
        settings.richText = true;
        settings.font = _font;
        settings.fontSize = 32;
        settings.fontStyle = FontStyle.Normal;
        settings.verticalOverflow = VerticalWrapMode.Overflow;
        settings.horizontalOverflow = HorizontalWrapMode.Overflow;

        TextGenerator generator = new TextGenerator();
        generator.Populate(str, settings);

        Debug.Log($"I generated: {generator.vertexCount} verts!");
    }

    public Mesh DrawRectAngle()
    {
        return null;
    }

    public void DrawRectAngle(Rect rect, bool isFilled, Color color)
    {
        Camera camera = Camera.current;

        Mesh mesh = new Mesh();
        //mesh.vertices = new Vector3[4]
        //{
        //    camera.ScreenToWorldPoint(new Vector3(rect.xMin, rect.yMin, 10)),
        //    camera.ScreenToWorldPoint(new Vector3(rect.xMax, rect.yMin, 10)),
        //    camera.ScreenToWorldPoint(new Vector3(rect.xMax, rect.yMax, 10)),
        //    camera.ScreenToWorldPoint(new Vector3(rect.xMin, rect.yMax, 10))
        //};
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
        mesh.triangles = new int[6]
        {
            0, 1, 2,
            2, 3, 0
        };

        // モデル行列
        Matrix4x4 trans = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        // ビュー行列
        Matrix4x4 view = camera.worldToCameraMatrix;

        // 射影行列
        float fov = 60f;
        float aspect = Screen.width / Screen.height;
        Matrix4x4 proj = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
        //Matrix4x4 proj = new Matrix4x4(
        //    new Vector4(2 / (float)Screen.width, 0, 0, 0),
        //    new Vector4(0, 2 / (float)Screen.height, 0, 0),
        //    new Vector4(0, 0, 1, 0),
        //    new Vector4(0, 0, 0, 1)
        //    );

        //_material.SetMatrix("_World", trans);
        //_material.SetMatrix("_View", view);
        //_material.SetMatrix("_Proj", proj);

        _material.SetColor("_Color", color);
        _material.SetPass(0);
        Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
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
