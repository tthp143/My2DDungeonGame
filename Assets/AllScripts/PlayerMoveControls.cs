using System.Collections;
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
    private bool knockBack = false;

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
        CheckStatus();

        if (knockBack) return;
        
        Move();
        JumpPlayer();
        
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
    
    public IEnumerator KnockBack(float forceX, float forceY,float duration, Transform otherObject)
    {
        int knockBackDirection;
        if(transform.position.x < otherObject.position.x)
        {
            knockBackDirection = -1;
        }
        else
        {
            knockBackDirection = 1;
        }

        knockBack = true;
        rb.linearVelocity = Vector2.zero;
        Vector2 theForce = new Vector2(forceX * knockBackDirection, forceY);
        rb.AddForce(theForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);
        knockBack = false;
        rb.linearVelocity = Vector2.zero;
    }
}
