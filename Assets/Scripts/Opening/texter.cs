using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class texter : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;

    void Start()
    {
        StartCoroutine(Type());
    }
    
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
