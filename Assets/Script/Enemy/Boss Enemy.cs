using UnityEngine;

public class BossEnemy : Enemy
{
    public float passTimeAttack2;
    public float attackDelayAttack2;
    public GameObject FireBall;
    private void FixedUpdate()
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
                    Debug.Log("attack 1");
                    animator.SetTrigger("attack 1");
                }
            }
            else
            {
                if (passTimeAttack2 >= attackDelayAttack2)
                {
                    passTimeAttack2 = 0;
                    Debug.Log("attack 2");
                    animator.SetTrigger("attack 2");
                }

            }
        }
        if (passTime <= attackDelay)
        {
            passTime += Time.deltaTime;
        }
        if (passTimeAttack2 <= attackDelayAttack2)
        {
            passTimeAttack2 += Time.deltaTime;
        }
    }

    public void Attack2()
    {
        Instantiate(FireBall);
    }
}
