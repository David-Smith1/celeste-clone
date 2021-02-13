using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutlershopController : MonoBehaviour
{
    [SerializeField]
    GameObject blake, trianglebutton;

    public static bool nearCutlerDoor;
    public static bool talkCutler;

    public bool visitedShop;


    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        Controller.visitedShop = true;

        Vector3 pos = transform.position;
        trianglebutton.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {

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
               
                SceneManager.LoadScene("OpeningScene");
            }
        }

        if (!nearCutlerDoor)
        {
            trianglebutton.SetActive(false);
        }

        if (talkCutler)
        {
            trianglebutton.SetActive(true);
            pos = new Vector3(blake.transform.position.x - .06f, blake.transform.position.y + .6f, 0);
            trianglebutton.transform.position = pos;

            if (Input.GetButton("Fire4"))
            {
                trianglebutton.SetActive(false);
               
              //function for cutler conversation
            }
        }

    }


}
