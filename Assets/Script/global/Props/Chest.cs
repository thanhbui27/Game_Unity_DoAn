using UnityEngine;

public class Chest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite img;

    public GameObject stoneMagic;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
}