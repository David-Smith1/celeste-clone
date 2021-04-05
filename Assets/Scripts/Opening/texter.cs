using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class texter : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string sentence1;
    public string sentence2;
    public string sentence3;
    public string sentence4;
    public string sentence5;
    public string sentence6;
    public string sentence7;
    public string cutler;
    public string[] sentences;
    public float typingSpeed;
    public int index;

    void Start()
    {
        StartCoroutine(Type());
    }
    
    IEnumerator Type()
    {
        foreach(char letter in sentence1.ToCharArray())
        {
            textDisplay.text += cutler;
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }
}
