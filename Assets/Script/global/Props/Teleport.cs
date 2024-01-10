using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public InventoryManager inventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var count = 0;
            var items = inventory.listOfUIItems;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemModel.Name == "Key Gold")
                {
                    count += 1;
                }

            }


            if (count == 1)
            {
                StartCoroutine(delayTime());
            }
            else
            {
                Debug.Log("Vui long tim kiem them da");
            }

        }
    }
    IEnumerator delayTime()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        print(Time.time);

    }
}
