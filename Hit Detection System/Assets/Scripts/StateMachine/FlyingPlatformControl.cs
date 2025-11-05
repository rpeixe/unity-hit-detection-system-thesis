using UnityEngine;

public class FlyingPlatformControl : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    [SerializeField] private float velocity;

    private Rigidbody2D plat_rb;
    private Transform plat_transform;

    private float directionX;
    private float directionY;
    private Vector2 direction;
    private int currentDestination;

    void Start()
    {
        plat_rb = GetComponent<Rigidbody2D>();
        plat_transform = GetComponent<Transform>();
        plat_transform.position = points[0].transform.position;

        currentDestination = 1;
        directionX = (points[1].transform.position.x - points[0].transform.position.x);
        directionY = (points[1].transform.position.y - points[0].transform.position.y);

        direction = new Vector2(directionX, directionY);
        plat_rb.linearVelocity = direction.normalized*velocity;
    }

    void Update()
    {
        if ((plat_transform.position == points[currentDestination].transform.position) || !(plat_transform.position.x > points[currentDestination].transform.position.x ^ Mathf.Sign(directionX) == 1) && !(plat_transform.position.y > points[currentDestination].transform.position.y ^ Mathf.Sign(directionY) == 1))
        {
            plat_transform.position = points[currentDestination].transform.position;

            currentDestination++;
            if (currentDestination >= points.Length)
            {
                currentDestination = 0;
                directionX = points[0].transform.position.x - points[points.Length - 1].transform.position.x;
                directionY = points[0].transform.position.y - points[points.Length - 1].transform.position.y;
            }
            else
            {
                directionX = points[currentDestination].transform.position.x - points[currentDestination - 1].transform.position.x;
                directionY = points[currentDestination].transform.position.y - points[currentDestination - 1].transform.position.y;
            }

            direction = new Vector2(directionX, directionY);
            plat_rb.linearVelocity = direction.normalized*velocity;
        }
    }
}
