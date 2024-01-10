using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberScenes;


    public void NextScenes()
    {
        SceneManager.LoadScene(numberScenes);
    }
}
