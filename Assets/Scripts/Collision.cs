using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public float collisionRadiusWall = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset, leftOffset2, rightOffset2;
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadiusWall, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadiusWall, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset2, collisionRadiusWall, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset2, collisionRadiusWall, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadiusWall, groundLayer) || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset2, collisionRadiusWall, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadiusWall, groundLayer) || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset2, collisionRadiusWall, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadiusWall);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadiusWall);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset2, collisionRadiusWall);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset2, collisionRadiusWall);
    }
}