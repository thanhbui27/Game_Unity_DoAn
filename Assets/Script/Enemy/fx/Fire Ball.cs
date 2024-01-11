using UnityEngine;

public class FireBall : MonoBehaviour
{
    private PlayerController player;
    public float speed = 1f;
    public BossEnemy enemy;
    private Vector2 _lastPositionPlayer;
    private bool isHitPlayer = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-1.8f, 1.8f), 1.2f, 0);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        _lastPositionPlayer = player.transform.position;
        animator = GetComponent<Animator>();
        Vector3 enemyDirectionLocal = player.transform.InverseTransformPoint(transform.position);

        if (enemyDirectionLocal.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -128f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 165, -128f);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {


        transform.position = Vector2.MoveTowards(transform.position, _lastPositionPlayer, speed * Time.deltaTime);

        if (transform.position.x == _lastPositionPlayer.x && transform.position.y == _lastPositionPlayer.y)
        {
            if (!isHitPlayer)
            {
                animator.SetTrigger("bum");
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IAgent player = collision.GetComponent<IAgent>();
            if (player != null)
            {
                player.OnHit(enemy._dame);
                isHitPlayer = true;
                animator.SetTrigger("bum");
            }
        }
    }
    public void DestroyGame()
    {
        Destroy(gameObject);
    }

}
