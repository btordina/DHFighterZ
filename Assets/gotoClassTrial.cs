using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoClassTrial : MonoBehaviour
{
    public void NextScene()
    {
        
        SceneManager.LoadScene("stage_classtrial", LoadSceneMode.Single);
    }
}