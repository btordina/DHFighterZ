using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("menu1");
    }
}
