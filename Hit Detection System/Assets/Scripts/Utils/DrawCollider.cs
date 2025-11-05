using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DrawCollider : MonoBehaviour
{
    private Collider2D m_collider;
    [SerializeField] private BoxTypes m_boxType;

    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        GLDebugDrawer.DrawBox(m_collider.bounds.center, m_collider.bounds.size, m_boxType);
    }
}
