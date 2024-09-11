using System;
using UnityEngine;
using UnityEngine.UI;

public class NotifyTextScript : MonoBehaviour
{
    private Text textComponent;
    public String text = "";


    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = text;
    }

    public void Log(String text) {
        this.text += "\n" + text;
    }
}
