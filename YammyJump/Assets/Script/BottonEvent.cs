using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BottonEvent : MonoBehaviour
{
    private float ClickTime;
    private bool IsClick;

    private void Update()
    {
        if (IsClick)
        {
            ClickTime += Time.deltaTime;
        }
        else
        {
            ClickTime = 0;
        }
    }

    public void ButtonDown()
    {
        IsClick = true;
    }

    public void ButtonUp()
    { 
        IsClick = false;
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.Jump();
        print("일반 :  " + ClickTime + " 초");

        if (ClickTime >= 0.3)
        {
            print("길게 :  " + ClickTime + " 초");

        }
    } 
}
