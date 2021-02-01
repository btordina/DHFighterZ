using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoP4 : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_shadowworld", LoadSceneMode.Single);
    }
}
