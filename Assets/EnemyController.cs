using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject blake, hehYouAgain;

    Transform triggerFight;

    Rigidbody2D rb, guyRb;

    public static bool fightStart;
    public float speed = 1f;
    public float health = 100f;
    public float timer;
    float waitingTime = 3f;
    public float jumpForce = 5f;
    public float counter = 0f;
    public float bulletForce = 5f;

    private Rigidbody2D enemyRb;
    private Vector2 positionDisplacement;
    private Vector2 positionOrigin;
    private float _timePassed;

    public bool canJump;




    // Start is called before the first frame update
    void Start()
    {
        guyRb = hehYouAgain.GetComponent<Rigidbody2D>();
       

        float randomDistance = Random.Range(-3f, 3f);

        positionDisplacement = new Vector2(randomDistance, 0);
        positionOrigin = hehYouAgain.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
      
    
        if (fightStart)
        {
            timer += Time.deltaTime;

            if (timer > 2.5)
            {
                canJump = true;
            }
       


            //enemyjump
            if (timer > waitingTime && counter <= 4 && canJump)
            {
                guyRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                timer = 0;
                counter += 1;
            }
            //enemywalk
            if (!canJump)
            {
                _timePassed += Time.deltaTime;
                hehYouAgain.transform.position = Vector3.Lerp(positionOrigin, positionOrigin + positionDisplacement,
                Mathf.PingPong(_timePassed, 2));
            }

            if (guyRb.velocity.y == 0)
            {
                canJump = false;
            }


        }

        

        //
        //if (timer == 2)
        //{
          //  Fire();
        //}




        //enemysuper attack
        //if (timer > waitingTime && counter > 3)
        //{
          //  FireBig();
           // counter = 0;
           // timer = 0;
        //}

    }

    //
    //private void Fire()
//{
  //  GameObject newBullet = Instantiate(projectile, transform.position, transform.rotation);
    //newBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
//}

//private void FireBig()
//{
  //  GameObject BIGASSBULLET = Instantiate(projectile, transform.position, transform.rotation);
   // BIGASSBULLET.GetComponent<Rigidbody2D>().AddForce(-transform.right * (bulletForce + 1), ForceMode2D.Impulse);
//}    

}
