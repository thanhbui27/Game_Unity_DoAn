using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TMP_Text currentHp = null;

    [SerializeField]
    private TMP_Text HpMax = null;

    [SerializeField]
    private Slider sliderExp;

    [SerializeField]
    private TMP_Text Level = null;

    public void UpdateExp(float currentexp, float maxexp)
    {
        sliderExp.maxValue = maxexp;

        sliderExp.value = currentexp;


    }

    public void initInfoHp(float currentHp, float maxHP)
    {

        this.currentHp.text = Mathf.Round(currentHp).ToString();
        this.HpMax.text = maxHP.ToString();
    }

    public void UpdateHP(float currentHp, float maxHP)
    {

        this.currentHp.text = Mathf.Round(currentHp).ToString();

        slider.value = currentHp / maxHP;


    }

    public void UpdateLevel(int level)
    {
        Level.text = level.ToString();
    }

}
