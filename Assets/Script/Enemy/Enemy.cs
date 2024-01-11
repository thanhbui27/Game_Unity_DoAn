using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IAgent
{
    protected GameObject Player;
    public float _speed = 50;
    public float chaseDistanceThreshold = 1f, attackDistanceThreshold = 0.3f;
    public int _health = 5;
    public int _maxHp = 5;
    public float attackDelay = 1;
    public float passTime = 1;
    public bool _target = true;
    public bool _isLive = true;
    public int _armor = 0;
    public float _dame = 1;
    public int _exp = 1;
    public int _level = 1;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D collider2D;
    protected SpriteRenderer spriteRenderer;
    protected CanvasEnemy CanvasController;
    public EnemyModel enemyModel;
    public List<GameObject> listDropItem = new List<GameObject>();


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

    public void Awake()
    {
        enemyModel = new EnemyModel(_speed, _isLive, _maxHp, _health, _level, _armor, _dame, _exp, chaseDistanceThreshold, attackDistanceThreshold);
    }

    // Start is called before the first frame update
    public void Start()
    {
        Player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool("isLive", enemyModel._isLive);
        CanvasController = GetComponentInChildren<CanvasEnemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        InitEnemy();
    }

    public void InitEnemy()
    {
        int lv = this.enemyModel._level;
        this.enemyModel._maxHP *= lv;
        this.enemyModel._health *= lv;
        this.enemyModel._dame *= lv;
        this.enemyModel._exp *= lv;
    }

    // Update is called once per frame
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
            if (FindAnimation("fire-run-right"))
            {
                animator.SetBool("run-right", true);
                animator.SetBool("run-left", false);

            }
            else
            {
                spriteRenderer.flipX = false;
            }



        }
        else
        {
            if (FindAnimation("fire-run-left"))
            {
                animator.SetBool("run-right", false);
                animator.SetBool("run-left", true);

            }
            else
            {
                spriteRenderer.flipX = true;
            }


        }

        if (distance < enemyModel.chaseDistanceThreshold)
        {
            if (distance <= enemyModel.attackDistanceThreshold)
            {
                // attack the player
                rb.AddForce(enemyModel._speed * Vector2.zero);
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
                Vector2 v = (Player.transform.position - transform.position);
                rb.AddForce(v.normalized * enemyModel._speed * Time.fixedDeltaTime);
                animator.SetBool("isMoving", true);
            }
        }
        if (distance > enemyModel.chaseDistanceThreshold)
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
        enemyModel._health -= dame;
        CheckIsDie();
        animator.SetTrigger("hit");
        CanvasController.UpdateHP(enemyModel._health, enemyModel._maxHP);
    }

    public void OnHit(float dame, Vector2 knock)
    {
        enemyModel._health -= dame;
        CheckIsDie();
        animator.SetTrigger("hit");
        CanvasController.UpdateHP(enemyModel._health, enemyModel._maxHP);
        rb.AddForce(knock);
    }

    public void CheckIsDie()
    {
        if (enemyModel._health <= 0)
        {
            enemyModel.EnemyDie();
            animator.SetBool("isLive", enemyModel._isLive);
        }

    }

    public void Death()
    {
        Debug.Log("die");
        if (listDropItem.Count > 0)
        {
            DropItem();
        }
        Player.GetComponent<PlayerController>().TextCT(this.enemyModel._exp.ToString(), Color.cyan);
        Player.GetComponent<PlayerController>().IncreaExp(this.enemyModel._exp);
        AudioManager.instance.PlaySFX("monster kill");
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
                        Vector2 direction = (col[i].gameObject.transform.position - transform.position).normalized;
                        direction = direction * 2;
                        AudioManager.instance.PlaySFX("monster attack");
                        agent.OnHit(this.enemyModel._dame, direction);
                    }
                }

            }
        }

    }

    public void DropItem()
    {
        float lengthList = listDropItem.Count - 1;
        int randomNumber = (int)Mathf.Round(Random.Range(0, lengthList));

        Instantiate(listDropItem[randomNumber], gameObject.transform.position, Quaternion.identity);
    }

    public bool HasParameter(string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    public AnimationClip FindAnimation(string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}
