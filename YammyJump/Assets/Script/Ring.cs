using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    Player grappling;
    public DistanceJoint2D joint2D;
    // Start is called before the first frame update
    void Start()
    {
        grappling = GameObject.Find("Player").GetComponent<Player>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ring"))
        {
            joint2D.enabled = true;
            grappling.isAttach = true;
        }
    }
}
