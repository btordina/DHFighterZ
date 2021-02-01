using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoMelty : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("playingfield", LoadSceneMode.Single);
    }
}
