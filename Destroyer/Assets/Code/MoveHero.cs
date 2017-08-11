using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class MoveHero : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float moveForce = 365f;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float jumpForce = 1000f;
    private float distToGround;
    private bool facingRight = true;
    private bool jump = false;
    private bool grounded = false;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    [HideInInspector]
    public float H;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
    }

    void Update()
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        Vector2 center = transform.Find("BoxGroundCenter").transform.position;
        Vector2 width = transform.Find("BoxGroundCenterWidth").transform.position;
        bool groundLeft = Physics2D.OverlapBox(center, new Vector2(center.x - width.x, center.x - width.x), 0f, 1 << LayerMask.NameToLayer("Ground"));
        bool groundRight = Physics2D.OverlapBox(center, new Vector2(width.x - center.x, width.x - center.x), 0f, 1 << LayerMask.NameToLayer("Ground"));
        grounded = groundLeft || groundRight;
        if (groundLeft && groundRight)
        {
            grounded = Physics2D.OverlapBox(center, new Vector2((center.x - width.x) / 10f, (center.x - width.x) / 10f), 0f, 1 << LayerMask.NameToLayer("Ground"));
        }

        //grounded = 
        //    || 
        //public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask = DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity);
        
        #if (UNITY_EDITOR || UNITY_STANDALONE) 
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
        #endif
    }

    void FixedUpdate()
    {
        #if (UNITY_EDITOR || UNITY_STANDALONE)
        H = Input.GetAxis("Horizontal");
        #endif

        Move();

        if (jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (H > 0 && !facingRight) Flip();
        else if (H < 0 && facingRight) Flip();
    }

    private void Move()
    {
        rb.velocity = new Vector2(System.Math.Sign(H) * maxSpeed, rb.velocity.y);
        if (H > 0)
        { rb.velocity = new Vector2(moveForce, rb.velocity.y); }
        else if (H < 0)
        { rb.velocity = new Vector2(-moveForce, rb.velocity.y); }
        else if (grounded)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        { rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y); }
    }

    public void TryJump()
    { jump = grounded; }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
