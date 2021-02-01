using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonRef : MonoBehaviour
{

    public GameObject selectIndicator;

    public bool selected;
    // Start is called before the first frame update
    void Start()
    {
        selectIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        selectIndicator.SetActive(selected);
    }
}
