using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoFD : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_fd", LoadSceneMode.Single);
    }
}
