using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescription : MonoBehaviour
{
    public Image image;
    public TMP_Text name;
    public TMP_Text description;
    public void ShowInfoItem(Sprite image, string name, string description)
    {
        if (name != null && image != null && description != null)
        {
            this.image.sprite = image;
            this.name.text = name;
            this.description.text = description;
        }
    }



    public void close()
    {
        Destroy(gameObject);
    }
}
