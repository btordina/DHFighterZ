using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoJojo : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_jojo", LoadSceneMode.Single);
    }
}
