using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    WaitForSeconds oneSec;

    public Transform[] spawnPositions;
    CharacterManager charM;
    LevelUI levelUI;

    public int maxTurns = 2;
    int currentTurn = 0;

   // public bool countdown;
  //  public int maxTurnTimer = 30;
   // int currentTimer;
   // float internalTimer;

    void Start()
    {
        charM = CharacterManager.GetInstance();
        levelUI = LevelUI.GetInstance();

        oneSec = new WaitForSeconds(1);

       // levelUI.AnnouncerTextLine1.gameObject.SetActive(false);
       // levelUI.AnnouncerTextLine2.gameObject.SetActive(false);

        StartCoroutine("StartGame");
    }

    void FixedUpdate()
    {
        if(charM.players[0].playerStates.transform.position.x < charM.players[1].playerStates.transform.position.x)
        {
            charM.players[0].playerStates.lookRight = false;
            charM.players[1].playerStates.lookRight = true;
        }

        else
        {
            charM.players[0].playerStates.lookRight = true;
            charM.players[1].playerStates.lookRight = false;
            

        }

    }

   // void Update()
  //  {

      //  if (countdown)
      //  {
            //HandleTurnTimer();
     //   }

  //  }

    /*void HandleTurnTimer()
    {

        //levelUI.LevelTimer.text = currentTimer.ToString();

        internalTimer += Time.deltaTime;

        if(internalTimer > 1)
        {
            currentTimer--;
            internalTimer = 0;

        }

        if(currentTimer <= 0)
        {
            EndTurnFunction(true);
            countdown = false;

        }

    }
    */

    IEnumerator StartGame()
    {
        yield return CreatePlayers();

        yield return InitTurn();

    }

    IEnumerator CreatePlayers()
    {
        for(int i = 0; i < charM.players.Count; i++)
        {
            GameObject go = Instantiate(charM.players[i].playerPrefab, spawnPositions[i].position, Quaternion.identity) as GameObject;

            charM.players[i].playerStates = go.GetComponent<StateManager>();

            charM.players[i].playerStates.healthSlider = levelUI.healthSliders[i];
        }

        yield return null;

    }


    IEnumerator InitTurn()
    {
        levelUI.matchStart.enabled = false;
        levelUI.Round2.enabled = false;
        levelUI.beginningSprite1.enabled = false;
        levelUI.finalRound.enabled = false;
        levelUI.KO.enabled = false;

        // currentTimer = maxTurnTimer;
        //   countdown = false;

        yield return InitPlayers();

        yield return EnableControl();


    }

    IEnumerator InitPlayers()
    {
        for(int i = 0; i < charM.players.Count; i++)
        {

            charM.players[i].playerStates.health = 100;
            charM.players[i].playerStates.dontMove = false;
            //charM.players[i].playerStates.handleAnim.anim.Play("Idle");
            charM.players[i].playerStates.transform.GetComponent<Animator>().Play("Idle");
            charM.players[i].playerStates.transform.position = spawnPositions[i].position;

        }

        yield return null;
    }


    IEnumerator EnableControl()
    {
        DisableControl();
        if (currentTurn == 0)
        {
            levelUI.beginningSprite1.enabled = true;

            yield return new WaitForSeconds(8.30f);


     
        }

       if(currentTurn == 1)
        {
            //levelUI.Round2.enabled = true;
            yield return new WaitForSeconds(3.05f);
        }

       if(currentTurn == 2)
        {
            //levelUI.finalRound.enabled = true;
            yield return new WaitForSeconds(2f);
        }

        levelUI.matchStart.enabled = true;
        Animation anim = this.levelUI.matchStart.GetComponent<Animation>();
        anim.Play("FIGHT");

        for (int i = 0; i < charM.players.Count; i++)
        {
            if(charM.players[i].playerType == PlayerBase.PlayerType.user)
            {
                InputHandler ih = charM.players[i].playerStates.gameObject.GetComponent<InputHandler>();
                ih.playerInput = charM.players[i].inputId;
                ih.enabled = true;

            }

            //If it's an AI character
           if(charM.players[1].playerType == PlayerBase.PlayerType.ai)
            {
                AICharacter ai = charM.players[1].playerStates.gameObject.GetComponent<AICharacter>();
                ai.enabled = true;
                InputHandler ih = charM.players[1].playerStates.gameObject.GetComponent<InputHandler>();
                ih.playerInput = charM.players[1].inputId;
                ih.enabled = false;
                //assign the enemy states to be the one from the opposite player
                ai.enStates = charM.returnOppositePlayer(charM.players[1]).playerStates;
            }

        }
        yield return oneSec;
        
        

    }


    public void EndTurnFunction(bool timeOut = false)
    {
       // countdown = false;

        //levelUI.LevelTimer.text = maxTurnTimer.ToString();

        if (timeOut)
        {
            //levelUI.AnnouncerTextLine1.gameObject.SetActive(true);
           
        }

        else
        {
           // levelUI.AnnouncerTextLine1.gameObject.SetActive(true);
            //levelUI.KO.enabled = true;
        }

        DisableControl();

        StartCoroutine("EndTurn");
        
    }

    void DisableControl()
    {


        for (int i = 0; i < charM.players.Count; i++)
        {
            charM.players[i].playerStates.ResetStateInputs();

            if(charM.players[i].playerType == PlayerBase.PlayerType.user)
            {
                charM.players[i].playerStates.GetComponent<InputHandler>().enabled = false;

            }

        }

    }


    IEnumerator EndTurn()
    {

        yield return oneSec;
        yield return oneSec;
        yield return oneSec;
        levelUI.KO.enabled = false;
       
        
        //if(vPlayer == null)
        //{

            //levelUI.AnnouncerTextLine1.text = "Draw";


       // }

       // else
        //{
            //levelUI.AnnouncerTextLine1.text = vPlayer.playerId + "Wins";

        //}

       // if (vPlayer != null)
        //{

           // if(vPlayer.playerStates.health == 100)
           // {
               // levelUI.AnnouncerTextLine2.gameObject.SetActive(true);
               // levelUI.AnnouncerTextLine2.text = "Flawless Victory";

         //   }
    
       // }

        currentTurn++;
        FindWinningPlayer();
        bool matchOver = isMatchOver();

        if (!matchOver)
        {

            StartCoroutine("InitTurn");
        }

        else
        {

            for(int i = 0; i < charM.players.Count; i++)
            {

                charM.players[i].score = 0;
                charM.players[i].hasCharacter = false;

            }

            SceneManager.LoadScene("character select");
        }

    }

    bool isMatchOver()
    {
        for (int i = 0; i < charM.players.Count; i++)
        {
            if (charM.players[i].score < maxTurns)
            {

                return false;

            }

            else
            {
                return true;
            }


        }

        return false;
    }



    void FindWinningPlayer()
    {
       // PlayerBase retVal = null;

       // StateManager targetPlayer = null;

        //if(charM.players[0].playerStates.health != charM.players[1].playerStates.health)
        //{
            if(charM.players[0].playerStates.health < charM.players[1].playerStates.health)
            {
            charM.players[1].score++;
                //targetPlayer = charM.players[1].playerStates;
                levelUI.AddWinIndicator(1);

            }
            else
            {
                charM.players[0].score++;
                //targetPlayer = charM.players[0].playerStates;
                levelUI.AddWinIndicator(0);

            }


            //retVal = charM.returnPlayerFromStates(targetPlayer);
       // }

        

    }

    public static LevelManager instance;

    public static LevelManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {

        instance = this;
    }
}
