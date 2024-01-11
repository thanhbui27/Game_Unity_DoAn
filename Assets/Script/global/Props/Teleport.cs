using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private InventoryManager inventory;
    private SpriteRenderer sprite;
    public Sprite newSprite;
    private BoxCollider2D collider2D;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
        inventory = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            var count = 0;
            var items = inventory.listOfUIItems;
            try
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Debug.Log(items[i].itemModel.Name);
                    if (items[i].itemModel.ID == 90 && items[i].itemModel != null)
                    {
                        count += 1;
                    }

                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }



            if (count >= 1)
            {
                sprite.sprite = newSprite;
                collider2D.isTrigger = true;
                StartCoroutine(delayTime());
            }
            else
            {
                Debug.Log("Không có chìa khoá");
            }

        }
    }
    IEnumerator delayTime()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        DataSaveGameManager.instance.SaveGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        print(Time.time);

    }
}
