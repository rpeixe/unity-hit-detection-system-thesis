using UnityEngine;

public class FighterPhysicsController : MonoBehaviour
{
    [SerializeField] private Vector2 m_groundCastBox;
    [SerializeField] private float m_groundCastDistance;

    [SerializeField] private int m_groundSpeed;
    [SerializeField] private int m_airSpeed;
    [SerializeField] private float m_jumpSpeed;

    private Rigidbody2D m_rb;
    private LayerMask m_platformLayerMask;

    public bool GravityEnabled { get; set; } = true;
    public bool IsFlipped { get; private set; } = false;
    public bool IsMovingUp => m_rb.linearVelocityY > 0;
    public Vector2 Position
    {
        get
        {
            return m_rb.position;
        }
        set
        {
            m_rb.position = value;
        }
    }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_platformLayerMask = LayerMask.GetMask("Platform");
    }

    //private void LateUpdate()
    //{
    //    Color groundedBoxColor = Color.yellow;

    //    GLDebugDrawer.DrawBox(transform.position + m_groundCastDistance * Vector3.down, m_groundCastBox, BoxTypes.GroundedBox);
    //}

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, m_groundCastBox, 0f, Vector2.down, m_groundCastDistance, m_platformLayerMask);

        return raycastHit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + m_groundCastDistance * Vector3.down, m_groundCastBox);
    }

    public void Walk(float inputX)
    {
        m_rb.linearVelocityX = m_groundSpeed * inputX;
    }

    public void Stop()
    {
        m_rb.linearVelocityX = 0;
    }

    public void Jump()
    {
        m_rb.linearVelocityY = m_jumpSpeed;
    }
}
