using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GLDebugDrawer : MonoBehaviour
{
    private struct Box
    {
        public Vector2[] corners;
        public Color color;
    }

    static private readonly List<Box> m_boxesToDraw = new List<Box>();

    static private Material m_lineMaterial;

    private void OnEnable()
    {
        CreateLineMaterial();
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    static void CreateLineMaterial()
    {
        if (!m_lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            m_lineMaterial = new Material(shader);
            m_lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            m_lineMaterial.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
            m_lineMaterial.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            m_lineMaterial.SetInt("_Cull", (int)CullMode.Off);
            // Turn off depth writes
            m_lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    public static void DrawBox(Vector2 center, Vector2 size, BoxTypes boxType)
    {
        Color boxColor = Color.white;
        switch (boxType)
        {
            case BoxTypes.Hitbox:
                boxColor = Color.red;
                break;
            case BoxTypes.Hurtbox:
                boxColor = Color.blue;
                break;
            case BoxTypes.CollisionBox:
                boxColor = Color.green;
                break;
            case BoxTypes.GroundedBox:
                boxColor = Color.yellow;
                break;
            case BoxTypes.ParryBox:
                boxColor = Color.magenta;
                break;
            default:
                break;
        }

        QueueBox(center, size, boxColor);
    }

    private static void QueueBox(Vector2 center, Vector2 size, Color color)
    {
        Vector2[] corners = new Vector2[4];

        Vector2 half = size * 0.5f;

        corners[1] = new Vector2(center.x - half.x, center.y + half.y);
        corners[0] = new Vector2(center.x - half.x, center.y - half.y);
        corners[2] = new Vector2(center.x + half.x, center.y + half.y);
        corners[3] = new Vector2(center.x + half.x, center.y - half.y);

        m_boxesToDraw.Add(new Box { corners = corners, color = color });
    }

    private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera.cameraType == CameraType.Game)
        {
            DrawQueuedBoxes(camera);
        }
    }

    private void DrawQueuedBoxes(Camera camera)
    {
        if (m_boxesToDraw.Count == 0 || m_lineMaterial == null) return;

        GL.PushMatrix();
        m_lineMaterial.SetPass(0);
        GL.LoadProjectionMatrix(camera.projectionMatrix);
        GL.modelview = camera.worldToCameraMatrix;

        GL.Begin(GL.LINES);

        foreach (var box in m_boxesToDraw)
        {
            GL.Color(box.color);
            for (int i = 0; i < 4; i++)
            {
                GL.Vertex(box.corners[i]);
                GL.Vertex(box.corners[(i + 1) % 4]);
            }
        }

        GL.End();
        GL.PopMatrix();

        m_boxesToDraw.Clear();
    }
}
