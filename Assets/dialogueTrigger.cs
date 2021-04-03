using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public bool talkCutler;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Equals("blake"))
        {
            CutlershopController.talkCutler = true;
        }

    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name.Equals("blake"))
        {
            CutlershopController.talkCutler = false;
        }

    }
}
