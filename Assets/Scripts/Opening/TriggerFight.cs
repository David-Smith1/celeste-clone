using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFight : MonoBehaviour
{
    public bool fightStart;


    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Equals("blake"))
        {
            EnemyController.fightStart = true;
            Controller.fightStart = true;
        }

    }

    // Update is called once per frame
}