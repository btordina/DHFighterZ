using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoOasis : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_oasis", LoadSceneMode.Single);
    }
}
