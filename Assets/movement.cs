using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb;
    private animation anim;

    public float speed = 5;
    public float jumpForce = 7;
    public float slideSpeed = 0.5f;
    public float wallJumpLerp = 2;
    public float dashSpeed = 10;
    public float superDashSpeed = 25;
    public float superJumpTimer = 0f;
    public float superJumpForce = 8f;


    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool usedDash;
    public bool usedSuperJump;
    public bool superJumpReady;
    public bool hasJumped;
   


    public int side = 1;



    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<animation>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, rb.velocity.y);


        if (x > 0)
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            anim.Flip(side);
        }



        // wallGrab Trigger

        if (coll.onWall && Input.GetButtonDown("Fire1"))
        {
            wallGrab = true;
        }

        // wallGrab ending 
        if (Input.GetButtonUp("Fire1") || Input.GetButtonDown("Jump") || !coll.onWall)
        {
            wallGrab = false;
            wallSlide = false;
        }



         // wallGrab
        if (wallGrab)
        {
           rb.gravityScale = 0;
           float speedModifier = y > 0 ? .2f : .3f;
           rb.velocity = new Vector2(0, yRaw * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 2f; // if not wallgrabbing gravity is normal
        }


       

        // trigger for wallSlide
         if (coll.onWall && !coll.onGround && x != 0 && !wallGrab)
        {
          wallSlide = true;
          WallSlide();
        }

        if (!coll.onWall || coll.onGround || wallGrab) // can't add || x = 0 condition because the private void makes x = 0?? I think
        {
           wallSlide = false;
        }



        // superjump
        if (Input.GetButtonDown("Jump") && superJumpReady && hasJumped)
        {
            usedSuperJump = true;
            SuperJump();
            
        }



        // jump else statement is walljump
        if (Input.GetButtonDown("Jump") && coll.onGround && !superJumpReady)
        {
            anim.SetTrigger("Jump");
            Jump(Vector2.up, false);
            superJumpTimer = 0f;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            anim.ResetTrigger("Jump");
           
        }


        if (Input.GetButtonDown("Jump") && wallSlide)
        {
            WallJump();

        }

        // trigger for dash
        if (Input.GetButton("Fire3") && !usedDash)
        {
            if (xRaw != 0 || yRaw != 0)
              Dash(xRaw, yRaw);
        }


     

        if (coll.onGround)
        {
            wallJumped = false;
            wallSlide = false;
            usedDash = false;
            superJumpTimer += Time.deltaTime;

        }

        if (rb.velocity.y <= 0)
        {
            usedSuperJump = false;
        }

        // checks ground touch and super jump ready
        if (superJumpTimer <= .2f && coll.onGround && hasJumped)
        {
            superJumpReady = true;
        }
        else
        {
            superJumpReady = false;
        }


    }

    private void SuperJump()
    {

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * superJumpForce;
        hasJumped = false;
        
    }



    private void Dash(float x, float y)
    {
        usedDash = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }


    IEnumerator DashWait()
    {
        rb.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        wallJumped = true;
       
        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 2f;
        GetComponent<BetterJump>().enabled = true;
        wallJumped = false;
       
    }

    private void Walk(Vector2 dir)
    {

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }
    

    private void WallJump()
    {

        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.2f));

        Vector2 wallJumpAngle = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallJumpAngle / 1.5f), true); // Vector.up / 1.5 + wallJumpAngle

        wallJumped = true;
    }

    IEnumerator DisableMovement(float time)
    {
        // canMove = false;
        yield return new WaitForSeconds(time);
        // canMove = true;
    }


    private void Jump(Vector2 dir, bool wall)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
        hasJumped = true;
        
    }

    private void WallSlide()
    {

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, Mathf.Clamp(rb.velocity.y, -slideSpeed, float.MaxValue));
    }
}