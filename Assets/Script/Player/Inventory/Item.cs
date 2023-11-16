using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public ItemModel InventoryItem;
    public int quantity;

    [SerializeField]
    private float duration = 0.3f;


    private InventoryManager InventoryManager;

    public Action<Event> pointClickEvent, pointClickRight;

    void Start()
    {
        InventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            InventoryManager.AddItem(InventoryItem,quantity);
            DestroyItem();
        }
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());

    }
    private IEnumerator AnimateItemPickup()
    {
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
    }


}
