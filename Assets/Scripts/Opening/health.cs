using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{

    public GameObject blake;

    public float blakehealth; 

    // Start is called before the first frame update
    void Start()
    {
       blakehealth = 100f;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "bullet(Clone)")
        {
            blakehealth -= 25;
        }
        if (blakehealth <= 0)
        {
            Destroy(blake);
        }
  
    }
}