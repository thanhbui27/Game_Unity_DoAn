using System.Collections;
using UnityEngine;

public class CheckGame : MonoBehaviour
{
    public GameObject screenWin;

    public float timeDelay = 5f;
    void Update()
    {
        if (GameObject.Find("Dragon Boss") == null && GameObject.Find("Dragon Boss 1.0") == null)
        {
            //it exists
            StartCoroutine(DelayDisplayScreen());
        }
    }

    IEnumerator DelayDisplayScreen()
    {
        yield return new WaitForSecondsRealtime(timeDelay);
        Instantiate(screenWin);
        Destroy(gameObject);
    }
}
