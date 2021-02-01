﻿/// <summary>
/// This script use to fade GUI
/// </summary>

using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class AlphaText : MonoBehaviour
{

    public float speedFade;
    private float count;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        //Fade in-out press start
        count += speedFade * Time.deltaTime;

        GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(count) * 1.0f);

    }
}
