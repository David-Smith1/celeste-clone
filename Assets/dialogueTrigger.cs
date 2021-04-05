using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;


    public void TriggerDialogue ()
    {
        if (coll.gameObject.name == "bullet(Clone)")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
