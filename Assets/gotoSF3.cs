using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoSF3 : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_sf3", LoadSceneMode.Single);
    }
}
