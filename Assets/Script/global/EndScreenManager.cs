using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public GameObject UiEnd;
    public GameObject UISetting;

    public static EndScreenManager Instance;


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void ActionGame(bool skipGame)
    {
        if (skipGame)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }
    }

    public void ToggleUiAudioSetting()
    {
        if (!UISetting.active)
        {
            UiEnd.SetActive(false);
            UISetting.SetActive(true);
        }
        else
        {
            UiEnd.SetActive(true);
            UISetting.SetActive(false);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(6);
    }
}
