using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public SpriteRenderer beginningSprite1;
    public SpriteRenderer matchStart;
    public SpriteRenderer Round1;
    public SpriteRenderer Round2;
    public SpriteRenderer finalRound;
    public SpriteRenderer KO;
    public Text LevelTimer;
    public Text AnnouncerTextLine1;
    public Text AnnouncerTextLine2;

    public Slider[] healthSliders;

    public GameObject[] winIndicatorGrids;
    public GameObject winIndicator;

    public static LevelUI instance;
    public static LevelUI GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    public void AddWinIndicator(int player)
        {
            GameObject go = Instantiate(winIndicator, transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(winIndicatorGrids[player].transform);

        }

    

}
