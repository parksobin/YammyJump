using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contect_sand : MonoBehaviour
{
    public Rigidbody2D catus2_4_1;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            catus2_4_1.gravityScale = 1.0f;
        }
    }
}
