using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField]
    GameObject blake, car, blakeInCar, trianglebutton, cutlersStore;

    Transform carDoor, TriggerFight;

    Rigidbody2D rb, carRb;
    public bool inCar;
    public static bool nearDoor;
    public static bool nearCutlerDoor;
    public static bool visitedShop;
    public static bool fightStart;
    public AudioSource town;

    Vector3 startpos;
    Vector3 shopspawn;

    public float carSpeed = 10f;
    float dir;
    public float carTimer = 0f;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        rb = blake.GetComponent<Rigidbody2D>();
        carRb = car.GetComponent<Rigidbody2D>();
        blakeInCar.gameObject.SetActive(false);

        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        town = allMyAudioSources[0];


        Vector3 pos = transform.position;
        trianglebutton.SetActive(false);

        startpos = blake.transform.position;
        shopspawn = new Vector3(cutlersStore.transform.position.x - 1, -5, 0);

        if (visitedShop)
        {
            startpos = shopspawn;
            blake.transform.position = shopspawn;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //car enter

        if (fightStart)
        {
            town.Stop();
        }

        if (nearDoor && Input.GetButtonDown("Fire4"))
        {
            inCar = true;
            blake.transform.parent = car.transform;
            blake.gameObject.SetActive(false);
            blakeInCar.gameObject.SetActive(true);
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
                blakeInCar.gameObject.SetActive(false);
                carTimer = 0f;
                inCar = false;

            }
            

        }

        if (nearCutlerDoor)
        {
            //display ENTER??
            trianglebutton.SetActive(true);
            pos = new Vector3(blake.transform.position.x - .06f, blake.transform.position.y + .6f, 0);
            trianglebutton.transform.position = pos;

            if (Input.GetButton("Fire4"))
            {
                blake.gameObject.SetActive(false);
                trianglebutton.SetActive(false);
                //code to change to cutler store scene
              
                SceneManager.LoadScene("CutlerStore");


            }
        }

        if (!nearCutlerDoor)
        {
            trianglebutton.SetActive(false);
        }


    }


}
