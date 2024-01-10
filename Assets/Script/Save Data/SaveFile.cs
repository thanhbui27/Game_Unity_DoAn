using System;
using System.IO;
using UnityEngine;

public class SaveFile
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public SaveFile(string dirPath, string fileName)
    {
        this.dataDirPath = dirPath;
        this.dataFileName = fileName;
    }

    public GameData LoadGame()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData data = null;
        try
        {
            if (File.Exists(fullPath))
            {
                string dataToLoad = "";
                FileStream file = new FileStream(fullPath, FileMode.Open, FileAccess.ReadWrite);
                StreamReader streamReader = new StreamReader(file);
                dataToLoad = streamReader.ReadToEnd();
                streamReader.Close();
                data = JsonUtility.FromJson<GameData>(dataToLoad);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        return data;
    }

    public void SaveGame(GameData data)
    {
        try
        {
            string fullPath = Path.Combine(dataDirPath, dataFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataStore = JsonUtility.ToJson(data, true);

            FileStream file = new FileStream(fullPath, FileMode.Create);

            StreamWriter streamWriter = new StreamWriter(file);

            streamWriter.Write(dataStore);
            streamWriter.Close();


        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }



}
