using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoWorld : MonoBehaviour
{
    public void NextScene()
    {

        
        SceneManager.LoadScene("stage_worldTourney", LoadSceneMode.Single);
    }
}
