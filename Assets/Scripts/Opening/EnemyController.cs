using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject blake, hehYouAgain, bulletPrefab, bigbulletPrefab;
 
    Transform triggerFight;
    public Transform firePoint;

   

    public static bool fightStart;



    public float timer;
    public float shootTimer;
    public float waitingTime = 3f;
    public float jumpForce = 5f;
    public float counter = 0f;

    public float bulletForce = 5f;

    private Rigidbody2D enemyRb;
   
    private Vector2 positionDisplacement;
    private Vector2 positionOrigin;
    private float _timePassed;

    public bool fired;
    public bool playedSound;
    public bool canJump;
    public bool fireBig;

    public AudioSource banter1src, banter2src, banter3src, lasersrc, battlemusic1;


    // Start is called before the first frame update
    void Start()
    {
         enemyRb = hehYouAgain.GetComponent<Rigidbody2D>();

        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        banter1src = allMyAudioSources[0];
        banter2src = allMyAudioSources[1];
        banter3src = allMyAudioSources[2];
        lasersrc = allMyAudioSources[3];
        battlemusic1 = allMyAudioSources[4];
       


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
          shootTimer += Time.deltaTime;
    

            if (!banter1src.isPlaying && !playedSound)
            {
                banter1src.Play();
                playedSound = true;
                StartCoroutine(WaitTime());
               
            }

            if (timer > 2.5)
            {
                canJump = true;
            }
       


            //enemyjump
            if (timer > waitingTime && counter <= 4 && canJump && !fireBig)
            {
                enemyRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                timer = 0;
                counter += 1;

                if (counter == 2 && enemyRb.velocity.y < 0)
                {
                    Fire();
                }
            }
            //enemywalk
            if (!canJump && !fireBig)
            {
                _timePassed += Time.deltaTime;
                hehYouAgain.transform.position = Vector3.Lerp(positionOrigin, positionOrigin + positionDisplacement,
                Mathf.PingPong(_timePassed, 2));
            }

            if (enemyRb.velocity.y == 0)
            {
                canJump = false;
            }

            //enemy attack

            if (shootTimer > 3 && counter > 1 && counter <= 4 && !fireBig)
            {
                Fire();
                shootTimer = 0;
                banter2src.Play();
            }

            if (shootTimer > 3 && !fireBig)
            {
                shootTimer = 0;
               
                    Fire();
            }

            //enemysuper attack
            if (timer > waitingTime -.1f && counter > 4)
            {
                FireBig();
                counter = 0;
                timer = 0;
                
            }


        }


    }

    
    private void Fire()
    {
      Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        fired = true;
    }
    
    private void FireBig()
    {
        fireBig = true;
        StartCoroutine(ChargeTime());
        
    }

    IEnumerator ChargeTime()
    {
        banter3src.Play();
        lasersrc.Play();
        yield return new WaitForSeconds(4f);
        lasersrc.Stop();
        Instantiate(bigbulletPrefab, firePoint.position, firePoint.rotation);
        fireBig = false;
        shootTimer = 0;



    }

    IEnumerator WaitTime()
    {
        
        yield return new WaitForSeconds(2.9f);
        battlemusic1.Play();


    }

}
