using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour, IAgent
{
    public Transform Player;
    public float speed;
    public float chaseDistanceThreshold = 1f, attackDistanceThreshold = 0.3f;
    public float _heath;
    public float _maxHp;
    public float attackDelay = 1;
    public float passTime = 1;
    public bool _target = true;
    public bool isLive = true;
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D collider2D;

    private CanvasController CanvasController;

    public List<GameObject> listDropItem = new List<GameObject>();

    public int Level { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float _dame;
    public float Dame { get { return _dame; } set { _dame = value; } }
    public float Heath { 
        get {
            return _heath;
        } 
        set { 
            if(value < 0)
            {
                animator.SetTrigger("hit");
 
            }
            _heath = value;
            if(_heath <= 0)
            {
                TargetAttack = false;
                animator.SetBool("isLive", false);
            }
        }
    }
    public bool TargetAttack
    {
        get { return _target; }
        set
        {
            _target = value;
            rb.simulated = value;
            collider2D.enabled = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isLive", isLive);
        CanvasController = GetComponentInChildren<CanvasController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player == null)
        {
            return;
        }

        float distance = Vector2.Distance(Player.transform.position, transform.position);

        if (distance < chaseDistanceThreshold)
        {

            if (distance <= attackDistanceThreshold)
            {
                // attack the player
                rb.AddForce(speed * Vector2.zero);
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
                Vector2 v = (Player.position - transform.position);
                rb.AddForce(v.normalized * speed * Time.fixedDeltaTime);
                animator.SetBool("isMoving", true);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistanceThreshold);
        Gizmos.DrawWireSphere(transform.position, attackDistanceThreshold);
    }

    public void OnHit(float dame)
    {
        Heath -= dame;
        animator.SetTrigger("hit");
        CanvasController.UpdateHP(Heath,_maxHp);
    }

    public void OnHit(float dame, Vector2 knock)
    {
        Heath -= dame;
        animator.SetTrigger("hit");
        CanvasController.UpdateHP(Heath,_maxHp);
        rb.AddForce(knock);
    }

    public void Death()
    {
        if(listDropItem.Count > 0)
        {
            DropItem();
        } 
        Destroy(gameObject);

    }

    public void AttackPlayer()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, attackDistanceThreshold);
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject.tag == "Player")
            {
                if (col[i].tag == "Player")
                {
                    IAgent agent = col[i].gameObject.GetComponent<IAgent>();
                    if (agent != null)
                    {
                        agent.OnHit(Dame);
                    }
                }

            }
        }
      
    }
  
    public void DropItem()
    {
        float lengthList = listDropItem.Count - 1;
        int randomNumber = (int)Mathf.Round(Random.Range(0, lengthList));

        Instantiate(listDropItem[randomNumber], gameObject.transform.position,Quaternion.identity);
    }
}
