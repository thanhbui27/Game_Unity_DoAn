using UnityEngine;

public class Chest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite img;
    public GameObject Mimic;
    public GameObject stoneMagic;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stoneMagic != null)
        {
            if (!img.name.Equals(spriteRenderer.sprite.name))
            {
                if (collision.tag == "Player" && collision.name == "Player")
                {
                    Debug.Log("mo ruong");
                    spriteRenderer.sprite = img;
                    Instantiate(stoneMagic, transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            Instantiate(Mimic, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


    }
}