using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.Progress;

public class Sand : MonoBehaviour
{
    Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            this.rb.gravityScale = 1;
        }
    }
}
