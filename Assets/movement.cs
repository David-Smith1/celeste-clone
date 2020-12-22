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

    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;

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
            wallSlide = false;
        }
        else
        {
            rb.gravityScale = 2.5f;
        }


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
        
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void WallSlide()
    {
    
        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;
        
        rb.velocity = new Vector2(push, -slideSpeed);
    }
    
    private void WallJump()
    {
      
       Vector2 wallJumpAngle = coll.onRightWall ? Vector2.left : Vector2.right;
       
    
    
    
    
    
    }
}
