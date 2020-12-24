using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb;
    public float speed = 10;
    public float jumpForce = 10;
    public float slideSpeed = 3;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    public float superDashSpeed = 25;

    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
 HEAD
    public bool usedDash;


    //add box collider for just the head, sides
    //add circle collider for the feet

    // change rb collision detection from discrete to continuous

    // create physics material 2d, can adjust friction and bouncyness, SET FRICTION TO 0. ADD IT TO BOX COLLIDER OF SPRITE OBJECT

    //FIX COLLIDER PROBLEM

b764f1e961a863119de9f9b167efdc564be01518

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
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

<<<<<<< HEAD
        // wallGrab Trigger
        if (coll.onWall && Input.GetKey(KeyCode.LeftShift))
        {
            wallGrab = true;
        }

        // wallGrab ending 
        if (Input.GetKeyUp(KeyCode.LeftShift) || !coll.onWall)
        {
            wallGrab = false;
            wallSlide = false;
        }

        // wallGrab
        if (wallGrab)
        {
            rb.gravityScale = 0;
            float speedModifier = y > 0 ? .3f : .5f;
            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier)); 
=======
        if (coll.onWall && Input.GetKey(KeyCode.LeftShift))
        {
          wallGrab = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift || !coll.onWall)
        {
          wallGrab = false;
          wallSlide = false;
        }
        
// wallGrab
        if (wallGrab)
        {
            rb.gravityScale = 0;
            float speedModifier = y > -4.0 ? .5f : 1;
            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
>>>>>>> b764f1e961a863119de9f9b167efdc564be01518
            wallSlide = false;
        }
        else
        {
            rb.gravityScale = 2.5f; // if not wallgrabbing gravity is normal
        }


<<<<<<< HEAD
        // trigger for wallSlide
       // if (coll.onWall && !coll.onGround && x != 0 && !wallGrab)
        //{
          //  wallSlide = true;
          //  WallSlide();
        //}

        //if (!coll.onWall || coll.onGround || wallGrab) // can't add || x = 0 condition because the private void makes x = 0?? I think
        //{
         //   wallSlide = false;
        //}


        // jump else statement is walljump
        if (Input.GetKeyDown(KeyCode.Space) && coll.onGround)
        {
            Jump(Vector2.up, false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coll.onWall && !coll.onGround)
        {
            WallJump();

        }

        // trigger for dash
        if (Input.GetKeyDown(KeyCode.C) && !usedDash)
        {
            if (xRaw != 0 || yRaw != 0)
              Dash(xRaw, yRaw);
        }

        // personal mechanic

        if (Input.GetKeyDown(KeyCode.X) && wallGrab)
        {
            if (xRaw != 0 || yRaw != 0)
              SuperDash(xRaw);
        }

        if (coll.onGround)
        {
            wallJumped = false;
            wallSlide = false;
            usedDash = false;
        }

=======
// trigger for wallSlide
        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab) 
            {
                wallSlide = true;
                WallSlide();
            }
        }
        
// wallJump
        if (Input.GetKeyDown(KeyCode.Space) && coll.onWall && !coll.onGround)
        {
            rb.velocity = new Vector2(wallJumpLerp, jumpForce);
        }
        
// jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (coll.onGround)
              rb.velocity = new Vector2(rb.velocity.x, 0);
              rb.velocity += Vector2.up * jumpForce;
            
            if (coll.onWall && !coll.onGround)
              WallJump();
        }
        
>>>>>>> b764f1e961a863119de9f9b167efdc564be01518
    }


    private void SuperDash(float x)
    {
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, 0);

        rb.velocity += dir.normalized * superDashSpeed;

        StartCoroutine(SuperDashWait());

    }

    IEnumerator SuperDashWait()
    {
<<<<<<< HEAD
        rb.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        wallJumped = true;
        

        yield return new WaitUntil(() => coll.onWall = true);

        rb.gravityScale = 2.5f;
        GetComponent<BetterJump>().enabled = true;
        wallJumped = false;
        
    }

    private void Dash(float x, float y)
    {
        // commetn out for now // usedDash = true;

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    //IEnumerator pauses actions and enforces time contstraints

    IEnumerator DashWait()
    {
        rb.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        wallJumped = true;
       

        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 2.5f;
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
=======
    
        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
>>>>>>> b764f1e961a863119de9f9b167efdc564be01518
        }
        float push = pushingWall ? 0 : rb.velocity.x;
        
        rb.velocity = new Vector2(push, -slideSpeed);
    }
    
    private void WallJump()
    {
      
       Vector2 wallJumpAngle = coll.onRightWall ? Vector2.left : Vector2.right;
       
    
    
    
    
    
    }

    private void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.2f));

        Vector2 wallJumpAngle = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallJumpAngle / 1.5f), true); // Vector.up / 1.5 + wallJumpAngle sets the angle of "up" diagonal.

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
        rb.velocity += dir * jumpForce;  // this is adding jumpForce to vectordir.UP if walljumped
        wallJumped = false;
    }

    private void WallSlide()
    {

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }
}