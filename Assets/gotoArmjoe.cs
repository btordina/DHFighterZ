using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoArmjoe : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_armjoe", LoadSceneMode.Single);
    }
}
