using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    GameObject blake, car;

    Transform carDoor;

    Rigidbody2D rb, carRb;
    public bool inCar;
    public static bool nearDoor;
    

    public float carSpeed = 10f;
    float dir;
    public float carTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = blake.GetComponent<Rigidbody2D>();
        carRb = car.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //car enter
        if (nearDoor && Input.GetButtonDown("Fire4"))
        {
            inCar = true;
            blake.transform.parent = car.transform;
            blake.gameObject.SetActive(false);
        }

        //carspeed
        if (inCar)
        {
            carTimer += Time.deltaTime;
            if (Input.GetButton("Fire1") && carRb.velocity.x < 15)
                carRb.AddForce(transform.right * carSpeed);

            if (Input.GetButton("Fire6") && carRb.velocity.x > -3)
                carRb.AddForce(-transform.right * carSpeed);

            if (Input.GetButton("Fire4") && carTimer > .2f)
            {
                blake.gameObject.SetActive(true);
                carTimer = 0f;
                inCar = false;

            }
            

        }

    }


}
