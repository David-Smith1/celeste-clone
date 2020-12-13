﻿using System.Collections;
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
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        wallGrab = coll.onWall && Input.GetKey(KeyCode.LeftShift);

        if (wallGrab)
        {
            rb.gravityScale = 0;
            float speedModifier = y > -4.0 ? .5f : 1;
            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 2.5f;
        }



        if (coll.onWall && !coll.onGround && !wallGrab)
        {
            WallSlide();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpForce;
        }
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
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
