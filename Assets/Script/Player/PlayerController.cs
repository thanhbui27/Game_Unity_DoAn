using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IAgent, IDataSaveGame
{
    public float deplayAttack = 1f;
    public float knockBackForce;
    public GameObject healthText;
    public GameObject EndSceen;
    private Vector2 movementInput;
    public CanvasController CanvasController;
    //private bool isAttack { get; set; } = false;
    private bool blockAttack { get; set; } = false;
    private Rigidbody2D rb;
    private Animator animator;
    public PlayerModel playerModel { get; set; }
    private GameData GameData;
    public bool TargetAttack { get; set; }

    public void Awake()
    {
        playerModel = new PlayerModel();
        playerModel.initItemBody();
        playerModel.initItemToBag();
    }

    public void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isLive", playerModel._isLive);
        CanvasController.initInfoHp(playerModel._health, playerModel._maxHP);
    }

    public void Update()
    {
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("speed", movementInput.sqrMagnitude);
        CanvasController.UpdateExp(playerModel._exp, playerModel.ExpToUpLevel());
    }


    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + movementInput * playerModel._speed * Time.fixedDeltaTime);
    }

    void OnMovement(InputValue value)
    {

        movementInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Attack();
    }

    public void UpdateUIInfo()
    {
        CanvasController.UpdateHP(playerModel._health, playerModel._maxHP);
        CanvasController.initInfoHp(playerModel._health, playerModel._maxHP);
        CanvasController.UpdateExp(playerModel._exp, playerModel._level * 10);
        CanvasController.UpdateLevel(playerModel._level);

    }
    public void UpLevel()
    {
        AudioManager.instance.PlaySFX("player uplevel");
        playerModel.UpLevel();
        CanvasController.UpdateHP(playerModel._health, playerModel._maxHP);
        CanvasController.initInfoHp(playerModel._health, playerModel._maxHP);
        this.TextCT("Up level " + playerModel._level, Color.blue);
    }
    public void Attack()
    {

        if (blockAttack)
        {

            return;
        }
        AudioManager.instance.PlaySFX("player attack");
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
                string nameGameObject = collision.gameObject.name.Split("-")[0];
                if (nameGameObject != null && nameGameObject.Equals("plant"))
                {
                    direction = direction * 0;
                    agent.OnHit(playerModel._dame, direction);
                }
                else
                {
                    direction = direction * knockBackForce;
                    agent.OnHit(playerModel._dame, direction);
                }


                TextCT("-" + playerModel._dame.ToString(), Color.cyan);
            }

        }

    }

    public void OnHit(float dame)
    {
        dame -= playerModel._armor;
        if (dame <= 0)
        {
            playerModel._health -= 1;
        }
        else
        {
            playerModel._health -= dame;

        }
        CheckIsDie();
        AudioManager.instance.PlaySFX("player hit");
        animator.SetTrigger("hit");
        CanvasController.UpdateHP(playerModel._health, playerModel._maxHP);
        TextCT("-" + dame.ToString(), Color.red);
    }

    public void OnHit(float dame, Vector2 knock)
    {
        dame -= playerModel._armor;
        if (dame <= 0)
        {
            playerModel._health -= 1;
        }
        else
        {
            playerModel._health -= dame;
        }
        CheckIsDie();
        AudioManager.instance.PlaySFX("player hit");
        animator.SetTrigger("hit");
        CanvasController.UpdateHP(playerModel._health, playerModel._maxHP);
        TextCT("-" + dame.ToString(), Color.red);

        rb.AddForce(knock);
    }

    public void IncreaExp(int exp)
    {
        playerModel._exp += exp;
        CanvasController.UpdateExp(playerModel._exp, playerModel._level * 10);
        if (playerModel._exp > playerModel.ExpToUpLevel())
        {
            UpLevel();
            CanvasController.UpdateLevel(playerModel._level);
            CanvasController.UpdateHP(playerModel._health, playerModel._maxHP);
        }
    }


    public void Death()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    public void OnEsc()
    {
        Instantiate(EndSceen);
        EndScreenManager.Instance.ActionGame(true);
    }
    public void RestoreHP(int hp)
    {
        if (playerModel._maxHP == playerModel._health)
        {
            TextCT("Hp đang đầy", Color.yellow);
        }
        else
        {
            if (playerModel._health + hp >= playerModel._maxHP)
            {
                playerModel._health = playerModel._maxHP;
            }
            else
            {
                playerModel._health += hp;
            }

            CanvasController.UpdateHP(playerModel._health, playerModel._maxHP);
        }
    }



    public void TextCT(string dame, Color color)
    {
        TMP_Text textTMP = Instantiate(healthText).GetComponent<TMP_Text>();

        textTMP.text = dame;

        textTMP.color = color;

        RectTransform textTransform = textTMP.GetComponent<RectTransform>();

        textTransform.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        Canvas canvs = GameObject.Find("CanvasController").GetComponent<Canvas>();

        textTransform.SetParent(canvs.transform);
    }

    public void getPowerItem(ItemModel item)
    {
        playerModel.getPowerItem(item, CanvasController);

    }

    public void subPowerItem(ItemModel item)
    {
        playerModel.subPowerItem(item, CanvasController);

    }

    public void CheckIsDie()
    {
        if (playerModel._health <= 0)
        {
            playerModel.PlayerDie();
            AudioManager.instance.PlaySFX("player die");
            animator.SetBool("isLive", playerModel._isLive);
        }

    }

    public void LoadData(GameData gameData)
    {
        Debug.Log("load file");
        playerModel = gameData.player;

        Debug.Log(gameData.player._health);
        UpdateUIInfo();
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log("save file");
        //gameData.player = playerModel;

    }
}
