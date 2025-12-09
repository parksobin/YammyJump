using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSc : MonoBehaviour
{
    [SerializeField]
    private GameObject Object;
    private bool check = false;
    bool touch = false;

    public void SetEmphasis()
    {
        if (!check)
        {
            StartCoroutine("Emphasis", Object);
            check = true;
        }
        else
        {
            StopCoroutine("Emphasis");
            check = false;
        }
    }

    private IEnumerator Emphasis(GameObject gameObject)
    {
        float increase = 0.2f;

        while (true)
        {
            /*
            while (gameObject.GetComponent<Transform>().localScale.y > 3.0f)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0
                                                                , gameObject.transform.localScale.y - increase);
                yield return new WaitForSeconds(0.05f); //Å©±â°¡ ¹Ù²î´Â ¼Óµµ
            }
            yield return new WaitForSeconds(0.05f); //Áß°£¿¡ ¸ØÃß´Â ÅÒ
            */


            while (gameObject.GetComponent<Transform>().localScale.y < 7.0f )
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + 0
                                                                , gameObject.transform.localScale.y + increase);
                yield return new WaitForSeconds(0.02f); //Å©±â°¡ ¹Ù²î´Â ¼Óµµ
            }
            yield return new WaitForSeconds(0.05f); //Áß°£¿¡ ¸ØÃß´Â ÅÒ
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touch = true;
        }
    }
}
