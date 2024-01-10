using UnityEngine;

public class UIManagerStart : MonoBehaviour
{
    public GameObject UiStart;
    public GameObject UISetting;
    public GameObject btncontinue;

    private void Start()
    {
        var btnUi = GameObject.Find("Btn UI");
        if (DataSaveGameManager.instance.CheckFileLocal())
        {
            GameObject btnctn = Instantiate(btncontinue, Vector3.zero, Quaternion.identity);
            btnctn.transform.SetParent(btnUi.transform);
            btnctn.transform.SetSiblingIndex(0);
        }

    }

    public void ToggleUiSetting()
    {
        if (!UISetting.active)
        {
            UiStart.SetActive(false);
            UISetting.SetActive(true);
        }
        else
        {
            UiStart.SetActive(true);
            UISetting.SetActive(false);
        }
    }

    public void NewGame()
    {

        DataSaveGameManager.instance.NewGame();
    }

    public void LoadGame()
    {
        DataSaveGameManager.instance.LoadDataGame();
    }

}
