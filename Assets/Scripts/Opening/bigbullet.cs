﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigbullet : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = -transform.right * speed;
    }

    // Update is called once per frame

}
