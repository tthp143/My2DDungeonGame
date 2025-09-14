using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    private GatherInput gatherInput;
    private Rigidbody2D rb;   
    public float jumpForce;
    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    
    private bool grounded = false;
    private int direction = 1; // to right-hand side
    private Animator animator;

    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(speed * gatherInput.valueX, rb.linearVelocity.y);  
        SetAnimatorValues();
    }

    private void FixedUpdate()
    {
        Move();
        JumpPlayer();
        CheckStatus();
    }

    private void Move()
    {
        Flip();
        rb.linearVelocity = new Vector2(speed * gatherInput.valueX, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            direction *= -1;
        }
    }

    private void SetAnimatorValues()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("vSpeed", rb.linearVelocity.y);
        animator.SetBool("Grounded", grounded);
    }

    private void JumpPlayer()
    {
        if (gatherInput.jumpInput && grounded)
        {
            rb.linearVelocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.jumpInput = false;
    }

    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit;
    }
}
