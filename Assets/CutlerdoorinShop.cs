using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlerdoorinShop : MonoBehaviour
{
    public bool nearCutlerDoor;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Equals("blake"))
        {
            CutlershopController.nearCutlerDoor = true;
        }

    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.name.Equals("blake"))
        {
            CutlershopController.nearCutlerDoor = false;
        }

    }
}
