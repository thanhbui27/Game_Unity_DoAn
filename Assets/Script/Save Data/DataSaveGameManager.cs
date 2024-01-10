using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSaveGameManager : MonoBehaviour
{
    [Header("File Store config")]
    private GameData gameData;

    [SerializeField] private string fileName;
    private SaveFile savefile;
    public static DataSaveGameManager instance { get; private set; }

    [Header("Auto Saving Configuration")]
    [SerializeField] private float autoSaveTimeSeconds = 60f;

    public List<IDataSaveGame> dataSaveGames;
    private Coroutine autoSaveCoroutine;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log("instance is exist");
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        savefile = new SaveFile(Application.persistentDataPath, fileName);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataSaveGames = LoadAllIDataSaveGame();
        LoadGame();

        // start up the auto saving coroutine
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        this.gameData.player.initItemBody();
        this.gameData.player.initItemToBag();
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {

        this.gameData = savefile.LoadGame();
        if (this.gameData == null)
        {
            Debug.LogWarning("game data not found");
            //NewGame();
        }
        foreach (IDataSaveGame dataSaveGame in dataSaveGames)
        {
            Debug.Log("load game ne");
            dataSaveGame.LoadData(gameData);
        }

    }

    public void LoadDataGame()
    {
        SceneManager.LoadScene(this.gameData.SceneIndex);
    }

    public void SaveGame()
    {
        //save game
        foreach (IDataSaveGame dataSaveGame in dataSaveGames)
        {
            dataSaveGame.SaveData(ref gameData);
        }
        if (SceneManager.GetActiveScene().buildIndex != 6)
        {
            this.gameData.SceneIndex = SceneManager.GetActiveScene().buildIndex;
            savefile.SaveGame(gameData);
        }

    }



    public bool CheckFileLocal()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(fullPath))
        {
            return true;
        }

        return false;
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
    public List<IDataSaveGame> LoadAllIDataSaveGame()
    {
        IEnumerable<IDataSaveGame> dataSaveGameObject = FindObjectsOfType<MonoBehaviour>().OfType<IDataSaveGame>();

        return new List<IDataSaveGame>(dataSaveGameObject);
    }

}
