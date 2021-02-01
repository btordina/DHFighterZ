using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SelectScreenManager : MonoBehaviour
{
    public int numberOfPlayers = 1;
    public List<PlayerInterfaces> p1Interfaces = new List<PlayerInterfaces>();
    public portraitInfo[] portraitPrefabs;
    public int maxX;
    public int maxY;
    portraitInfo[,] charGrid;

    public GameObject portraitCanvas;

    bool loadLevel;
    public bool bothPlayersSelected;

    CharacterManager charManager;


    #region Singleton
    public static SelectScreenManager instance;

    public static SelectScreenManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;

    }
    #endregion

    void Start()
    {
        charManager = CharacterManager.GetInstance();
        numberOfPlayers = charManager.numberOfUsers;

        charGrid = new portraitInfo[maxX, maxY];

        int x = 0;
        int y = 0;

        portraitPrefabs = portraitCanvas.GetComponentsInChildren<portraitInfo>();

        for(int i = 0; i < portraitPrefabs.Length; i++)
        {
            portraitPrefabs[i].posX += x;
            portraitPrefabs[i].posY += y;

            charGrid[x, y] = portraitPrefabs[i];

            if(x < maxX - 1)
            {
                x++;
            }
            else
            {
                x = 0;
                y++;
            }

        }


    }

    void Update()
    {

        if (!loadLevel)
        {
            for(int i = 0; i < p1Interfaces.Count; i++)
            {
                if(i < numberOfPlayers)
                {

                   /* if (Input.GetButtonUp("Fire1" + charManager.players[i].inputId))
                    {

                        p1Interfaces[i].playerBase.hasCharacter = false;

                    }
                    */
                    if (!charManager.players[i].hasCharacter)
                    {

                        p1Interfaces[i].playerBase = charManager.players[i];

                        HandleSelectorPosition(p1Interfaces[i]);
                        HandleSelectScreenInput(p1Interfaces[i], charManager.players[i].inputId);
                        HandleCharacterPreview(p1Interfaces[i]);

                    }

                }
                else
                {

                    charManager.players[i].hasCharacter = true;
                }
            }

        }

        if (bothPlayersSelected)
        {
            Debug.Log("loading");
            StartCoroutine("LoadLevel");
            loadLevel = true;

        }
        else
        {
            if(charManager.players[0].hasCharacter && charManager.players[1].hasCharacter)
            {
                bothPlayersSelected = true;
            }
        }


    }



    void HandleSelectScreenInput(PlayerInterfaces p1, string playerId)
    {
        #region Grid Navigation

        float vertical = Input.GetAxis("Vertical" + playerId);
        if(vertical != 0)
        {
            if (!p1.hitInputOnce)
            {
                if (vertical > 0)
                {
                    p1.activeY = (p1.activeY > 0) ? p1.activeY - 1 : maxY - 1;

                }
                else
                {
                    p1.activeY = (p1.activeY < maxY - 1) ? p1.activeY + 1 : 0;


                }

                p1.hitInputOnce = true;
            }

        }

        float horizontal = Input.GetAxis("Horizontal" + playerId);
        if (horizontal != 0)
        {
            if (!p1.hitInputOnce)
            {
                if (horizontal > 0)
                {
                    p1.activeX = (p1.activeX < maxX - 1) ? p1.activeX + 1 : 0;
                    

                }
                else
                {

                    p1.activeX = (p1.activeX > 0) ? p1.activeX - 1 : maxX - 1;

                }
                p1.timerToReset = 0;
                p1.hitInputOnce = true;
            }

        }

        if(vertical == 0 && horizontal == 0)
        {
            p1.hitInputOnce = false;

        }


        if (p1.hitInputOnce)
        {
            p1.timerToReset += Time.deltaTime;

            if(p1.timerToReset > 0.8f)
            {
                p1.hitInputOnce = false;
                p1.timerToReset = 0;

            }

        }


        #endregion

        if(Input.GetButtonUp("Fire1"+ playerId))
        {
            p1.createdCharacter.GetComponentInChildren<Animator>().Play("C");

            p1.playerBase.playerPrefab = charManager.returnCharacterwithID(p1.activePortrait.characterId).prefab;

            p1.playerBase.hasCharacter = true;

        }

     

    }



    void HandleSelectorPosition(PlayerInterfaces p1)
    {

        p1.selector.SetActive(true);

        p1.activePortrait = charGrid[p1.activeX, p1.activeY];

        Vector2 selectorPosition = p1.activePortrait.transform.localPosition;
        selectorPosition = selectorPosition + new Vector2(portraitCanvas.transform.localPosition.x, portraitCanvas.transform.localPosition.y);

        p1.selector.transform.localPosition = selectorPosition;

    }


    void HandleCharacterPreview(PlayerInterfaces p1)
    {
        if(p1.previewPortrait != p1.activePortrait)
        {
            if(p1.createdCharacter != null)
            {

                Destroy(p1.createdCharacter);
            }

            GameObject go = Instantiate(CharacterManager.GetInstance().returnCharacterwithID(p1.activePortrait.characterId).prefab, p1.charVisPos.position, Quaternion.identity) as GameObject;

            
            p1.createdCharacter = go;
            
            p1.previewPortrait = p1.activePortrait;
            

            if (!string.Equals(p1.playerBase.playerId, charManager.players[0].playerId))
            {
                p1.createdCharacter.GetComponent<StateManager>().lookRight = true;
                

            }
            p1.createdCharacter.GetComponent<StateManager>().dontMove = true;

        }

    }

    IEnumerator LoadLevel()
    {

        for (int i = 0; i < charManager.players.Count; i++)
        {
            if(charManager.players[i].playerType == PlayerBase.PlayerType.ai)
            {
                if(charManager.players[i].playerPrefab == null)
                {
                    int ranValue = Random.Range(0, portraitPrefabs.Length);

                    charManager.players[i].playerPrefab = charManager.returnCharacterwithID(portraitPrefabs[ranValue].characterId).prefab;
                    Debug.Log(portraitPrefabs[ranValue].characterId);


                }

            }

        }

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("stage select", LoadSceneMode.Single);

    }




    [System.Serializable]
    public class PlayerInterfaces
    {
        public portraitInfo activePortrait;
        public portraitInfo previewPortrait;
        public GameObject selector;
        public Transform charVisPos;
        public GameObject createdCharacter;


        public int activeX;
        public int activeY;

        public bool hitInputOnce;
        public float timerToReset;

        public PlayerBase playerBase;

    }




}
