using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    
    public void UpdateHP(float currentHp, float maxHP)
    {
        slider.value = currentHp / maxHP;
    }

   
}
