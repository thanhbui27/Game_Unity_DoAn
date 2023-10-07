using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;

    public int knockBackForce = 5;

    public int maxHealth = 2;
    public int health = 2;
    public int minHealth = 0;
    public float TimeDelayToDie = 0.5f;

   
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
   
    public void OnHit(int damageTotal)
    {
        health -= damageTotal;
        PushBack();
      
        if (health <= 0)
        {
            health = minHealth;
            StartCoroutine(DeathDelay());
        }
    }

    private void PushBack()
    {
        Debug.Log("PushBack back");
        rb.MovePosition(rb.position * (knockBackForce * Time.fixedDeltaTime));
    }
    public IEnumerator DeathDelay()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(TimeDelayToDie);
        Death();
    }

    void Death()
    {
       
        Destroy(gameObject);
    }

}
