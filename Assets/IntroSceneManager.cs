using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
   
    bool loadinglevel;
    bool init;

    public int activeElement;
    public GameObject menuObj;
    public ButtonRef[] menuOptions;

    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;

    }


    void PlaySound()
    {

        source.PlayOneShot(sound);
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadinglevel)
        {
            menuOptions[activeElement].selected = true;

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                menuOptions[activeElement].selected = false;

                if (activeElement > 0)
                {

                    activeElement--;
                }

                else
                {
                    activeElement = menuOptions.Length - 1;

                }

            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                menuOptions[activeElement].selected = false;

                if (activeElement < menuOptions.Length - 1)
                {

                    activeElement++;
                }

                else
                {
                    activeElement = 0;

                }


            }



            if(Input.GetKeyUp(KeyCode.Space)|| Input.GetButtonUp("Fire1"))
            {

                Debug.Log("load");
                loadinglevel = true;
                StartCoroutine("LoadLevel");
                menuOptions[activeElement].transform.localScale *= 1.05f;



            }
        }  


    }


    void HandleSelectedOption()
    {
        switch (activeElement)
        {
            case 0:
                CharacterManager.GetInstance().numberOfUsers = 2;
                CharacterManager.GetInstance().players[1].playerType = PlayerBase.PlayerType.user;
                
                break;
            case 1:
                CharacterManager.GetInstance().numberOfUsers = 1;
                CharacterManager.GetInstance().players[0].playerType = PlayerBase.PlayerType.user;
                CharacterManager.GetInstance().players[1].playerType = PlayerBase.PlayerType.ai;

                break;
            case 2:
                CharacterManager.GetInstance().numberOfUsers = 1;
                CharacterManager.GetInstance().players[1].playerType = PlayerBase.PlayerType.ai;
                break;
            case 3:
                break;
            case 4:
                Application.Quit();
                break;

        }


    }

    IEnumerator LoadLevel()
    {
        HandleSelectedOption();
        PlaySound();
        yield return new WaitForSeconds(0.6f);
        
        SceneManager.LoadSceneAsync("character select", LoadSceneMode.Single); 

    }


}
