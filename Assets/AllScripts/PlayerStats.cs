using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxHealth;
    public float health;
    private bool canTakeDamage = true;
    private Animator anim;
    private  LogicScript logicScript;
    

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        health = maxHealth;
        logicScript = Object.FindFirstObjectByType<LogicScript>();
        logicScript.UpdatePlayerHP(health);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if(!canTakeDamage)
        {
            return;
        }

        // หยุดการเคลื่อนไหว
        

        health -= damage;
        anim.SetBool("Damage", true);
        logicScript.UpdatePlayerHP(health);
        if (health <= 0)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponentInParent<GatherInput>().DisableControls();
            Debug.Log("Player is dead");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            logicScript.ShowGameOverUI();
        }
        StartCoroutine(DamagePrevention());
    }
    
    private IEnumerator DamagePrevention() 
    {
         canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        if (health > 0)
        {
            canTakeDamage = true;
            anim.SetBool("Damage", false);
        }
        else 
        {
            anim.SetBool("Death", true);
        }
    }
}
