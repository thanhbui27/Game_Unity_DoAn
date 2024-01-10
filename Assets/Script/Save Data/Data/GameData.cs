using System;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public PlayerModel player;
    public int SceneIndex;
    public GameData()
    {
        lastUpdated = DateTime.Now.Millisecond;
        SceneIndex = 0;
        player = new PlayerModel();

    }
}
