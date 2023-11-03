using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshDame : MonoBehaviour
{
    [SerializeField]
    private float speed = 30f;

    [SerializeField]
    private float TimeToLive = 1f;

    [SerializeField]
    private float TimeELapsed = 0.0f;

    Vector3 v3 = new Vector3(0, 1, 0);
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void Update()
    {
        TimeELapsed += Time.deltaTime;

        rectTransform.position += v3 * speed *  Time.deltaTime;

        if (TimeELapsed > TimeToLive)
        {
            Destroy(gameObject);
        }
    }
   
}
