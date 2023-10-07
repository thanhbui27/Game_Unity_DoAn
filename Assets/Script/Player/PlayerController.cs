using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float deplayAttack = 1f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movementInput;

    //private bool isAttack { get; set; } = false;
    private bool blockAttack { get; set; } = false;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("speed", movementInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * speed * Time.fixedDeltaTime);
    }

    void OnMovement(InputValue value)
    {
    
        movementInput = value.Get<Vector2>();
    }
    void OnAttack()
    {
        Attack();
    }

    public void Attack()
    {
        if (blockAttack)
        {
            Debug.Log("dang hoi skill");
            return;
        }

        animator.SetTrigger("isAttack");
        blockAttack = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(deplayAttack);
        blockAttack = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.SendMessage("OnHit", 1);
        }
       
    }
}
