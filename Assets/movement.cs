using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Collision coll;
    private Rigidbody2D rb;
    public float speed = 10;
    public float jumpForce = 10;
    public float slideSpeed = 5;

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

        if (coll.onWall && !coll.onGround)
        {
            WallSlide();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

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
        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
    }
 
}
