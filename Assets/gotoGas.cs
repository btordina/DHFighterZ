using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoGas : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_gaschamber", LoadSceneMode.Single);
    }
}
