using UnityEngine;

public class PlayerAttackControls : MonoBehaviour
{
    private PlayerMoveControls playerMoveControls;
    private GatherInput gatherInput;
    private Animator animator;

    [Header("Attack Settings")]
    public PolygonCollider2D attackCollider;
    public float attackCooldown = 0.5f; // ✅ ปรับได้ใน Inspector
    private bool attackStarted = false;
    private bool isCooldown = false;

    void Start()
    {
        playerMoveControls = GetComponent<PlayerMoveControls>();
        gatherInput = GetComponent<GatherInput>();
        animator = GetComponent<Animator>();

        // ปิด collider ตอนเริ่มเกม
        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        // ✅ ตรวจว่าไม่มีคูลดาวน์ และยังไม่ได้เริ่มโจมตี
        if (gatherInput.tryAttack && !attackStarted && !isCooldown)
        {
            animator.SetBool("Attack", true);
            attackStarted = true;
            gatherInput.tryAttack = false;

            // เริ่มคูลดาวน์ทันที
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    public void ActivateAttack()
    {
        if (attackCollider != null)
            attackCollider.enabled = true;
    }

    public void ResetAttack()
    {
        animator.SetBool("Attack", false);
        attackStarted = false;
        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    // ✅ ฟังก์ชันคูลดาวน์
    private System.Collections.IEnumerator AttackCooldownRoutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(attackCooldown); // รอเวลาตามที่ตั้งไว้
        isCooldown = false;
    }
}
