using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health2 : MonoBehaviour
{

    public GameObject hehYouAgain;
    public float enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 100f;

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "hitbox")
        {
            enemyHealth -= 15;
        }
        if (enemyHealth <= 0)
        {
            Destroy(hehYouAgain);
        }

    }
}
