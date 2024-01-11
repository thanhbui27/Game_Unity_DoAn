using UnityEngine;

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

    public void NewGame()
    {
        Time.timeScale = 1;
        DataSaveGameManager.instance.NewGame();
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

    public void CloseUi()
    {
        Time.timeScale = 1;
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void ExitGame()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }
}
