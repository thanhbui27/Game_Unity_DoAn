using UnityEngine;

public class MonsterStand : Enemy
{


    void FixedUpdate()
    {
        if (Player == null)
        {
            return;
        }
        // tinh khoang cach
        float distance = Vector2.Distance(Player.transform.position, transform.position);

        // xac dinh vi tri player o ben phai hay trai 
        Vector3 enemyDirectionLocal = Player.transform.InverseTransformPoint(transform.position);

        if (enemyDirectionLocal.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;

        }
        // chaseDistanceThreshold khoang cach duoi theo
        if (distance < chaseDistanceThreshold)
        {
            // attackDistanceThreshold khoang cach tan cong
            if (distance <= attackDistanceThreshold)
            {
                // di chuyen the player
                rb.AddForce(_speed * Vector2.zero);
                // set animation di chuyen
                animator.SetBool("isMoving", false);
                // tan cong 
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
