using UnityEngine;
using UnityEngine.UI;

public class ScaleItemSlot : MonoBehaviour
{
    public GameObject container;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float width = container.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2(width / 10.4f, width / 10.4f);
        if (newSize.y < 100 && newSize.x < 100)
        {
            container.GetComponent<GridLayoutGroup>().cellSize = newSize;
        }

    }
}
