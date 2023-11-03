using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IAgent
{
    public float speed = 1f;
    public float deplayAttack = 1f;
    public float knockBackForce;
    public bool isLive = true;
    public int _maxHP;
    public GameObject healthText;

    private Vector2 movementInput;

    public CanvasController CanvasController;
    //private bool isAttack { get; set; } = false;
    private bool blockAttack { get; set; } = false;
    private Rigidbody2D rb;
    private Animator animator;

    public int Level { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Heath {
        get
        {
            return _health;
        }
        set
        {
            if (value < 0)
            {
                animator.SetTrigger("hit");
            }
            _health = value;
            if (_health <= 0)
            {
                animator.SetBool("isLive", false);
            }
        }
    }

    public float _health;
    public bool TargetAttack { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float _dame;
    public float Dame { get { return _dame; } set { _dame = value; } }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isLive", isLive);
    }

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
            IAgent agent = collision.gameObject.GetComponent<IAgent>();
            if (agent != null)
            {
                Vector2 direction = (collision.gameObject.transform.position - transform.position).normalized;
                direction = direction * knockBackForce;
                agent.OnHit(Dame, direction);
                DameText("-" + Dame.ToString(),false);
            }
           
        }
       
    }

    public void OnHit(float dame)
    {
        Heath -= dame;
        CanvasController.UpdateHP(Heath, _maxHP);
        DameText("-" + dame.ToString(), true);
    }

    public void OnHit(float dame, Vector2 knock)
    {
        throw new System.NotImplementedException();
    }



    public void Death()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    public void DameText(string dame, bool isDamed)
    {
        TMP_Text textTMP = Instantiate(healthText).GetComponent<TMP_Text>();

        textTMP.text = dame;

        if(!isDamed)
        {
            textTMP.color = Color.cyan;
        }

        RectTransform textTransform = textTMP.GetComponent<RectTransform>();

        textTransform.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        Canvas canvs = GameObject.Find("CanvasController").GetComponent<Canvas>();

        textTransform.SetParent(canvs.transform);
    }
}
