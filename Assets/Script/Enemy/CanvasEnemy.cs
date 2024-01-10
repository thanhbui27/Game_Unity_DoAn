using UnityEngine;
using UnityEngine.UI;

public class CanvasEnemy : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    public void UpdateHP(float currentHp, float maxHP)
    {
        slider.value = currentHp / maxHP;


    }
}
