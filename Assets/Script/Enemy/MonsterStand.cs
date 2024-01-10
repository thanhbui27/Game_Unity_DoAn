using UnityEngine;

public class MonsterStand : Enemy
{


    void FixedUpdate()
    {
        if (Player == null)
        {
            return;
        }

        float distance = Vector2.Distance(Player.transform.position, transform.position);
        Vector3 enemyDirectionLocal = Player.transform.InverseTransformPoint(transform.position);

        if (enemyDirectionLocal.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;

        }

        if (distance < chaseDistanceThreshold)
        {
            if (distance <= attackDistanceThreshold)
            {
                // attack the player
                rb.AddForce(_speed * Vector2.zero);
                animator.SetBool("isMoving", false);

                if (passTime >= attackDelay)
                {
                    passTime = 0;
                    Debug.Log("attack");
                    animator.SetTrigger("attack");
                }
            }
            else
            {
                //chasing the player
                //Vector2 v = (Player.transform.position - transform.position);
                //rb.AddForce(v.normalized * speed * Time.fixedDeltaTime);
                //animator.SetBool("isMoving", true);
            }
        }
        if (distance > chaseDistanceThreshold)
        {
            rb.AddForce(Vector2.zero);
            animator.SetBool("isMoving", false);

        }
        if (passTime <= attackDelay)
        {
            passTime += Time.deltaTime;
        }

    }
}
