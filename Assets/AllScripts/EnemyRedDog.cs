using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRedDog : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // public Transform groundCheck;
    // public Transform wallCheck;

    // private bool detectGround;
    // private bool detectWall;
    // public float speed = 1;
    // private int direction = -1;
    // public LayerMask layerToCheck;
    void Start()
    {

    }
    // private void FixedUpdate()
    // {
    //     Filp();
    //     rb.linearVelocity = new Vector2(-1, rb.linearVelocity.y);
    // }

    // Update is called once per frame
    void Update()
    {

    }
    //     private void Filp()
    //     {
    //         detectGround = Physics2D.OverlapCircle(groundCheck.position, radius, layerToCheck);
    //         detectWall = Physics2D.OverlapCircle(wallCheck.position, radius, layerToCheck);

    //         if (!detectGround || detectWall)
    //         {
    //             direction *= -1;
    //             transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    //         }
    //     }
    //      public float radius;
    // 	  private void OnDrawGizmos()
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawWireSphere(groundCheck.position, radius);
    //         Gizmos.DrawWireSphere(wallCheck.position, radius);
    //     } 
    public float speed = 1;
    private int direction = -1;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask layerToCheck;

    private bool detectGround;
    private bool detectWall;
    public float radius;

    private void FixedUpdate()
    {
        Filp();
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }
    private void Filp() 
    { 
        detectGround = Physics2D.OverlapCircle(groundCheck.position, radius, layerToCheck);         
        detectWall = Physics2D.OverlapCircle(wallCheck.position, radius, layerToCheck);

        if (!detectGround || detectWall)
        {
            direction *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.DrawWireSphere(wallCheck.position, radius);
    } 
}
