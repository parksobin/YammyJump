using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMG : MonoBehaviour
{
    //스테이지 오브젝트
    public GameObject Stage2_1;
    public GameObject Stage2_2;
    public GameObject Stage2_3;
    public GameObject Stage2_4;
    //back
    public GameObject BackBT;
    //js jump
    public GameObject GameButton;
    //게임 배경
    public GameObject DesertBackGround;
    //맵 배경
    public GameObject DesertMapBG;



    // Start is called before the first frame update
    void Start()
    {
        Stage2_1.SetActive(false);
        Stage2_2.SetActive(false);
        Stage2_3.SetActive(false);
        Stage2_4.SetActive(false);
        DesertMapBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //게임 기본 화면
    public void GameBasic()
    {
        DesertMapBG.SetActive(false);
        DesertBackGround.SetActive(true);
        GameButton.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void Start2_1()
    {
        GameBasic();
        Stage2_1.SetActive(true);
        Stage2_2.SetActive(false);
        Stage2_3.SetActive(false);
        Stage2_4.SetActive(false);
    }
    public void Start2_2()
    {
        GameBasic();
        Stage2_1.SetActive(false);
        Stage2_2.SetActive(true);
        Stage2_3.SetActive(false);
        Stage2_4.SetActive(false);
    }
    public void Start2_3()
    {
        GameBasic();
        Stage2_1.SetActive(false);
        Stage2_2.SetActive(false);
        Stage2_3.SetActive(true);
        Stage2_4.SetActive(false);
    }
    public void Start2_4()
    {
        GameBasic();
        Stage2_1.SetActive(false);
        Stage2_2.SetActive(false);
        Stage2_3.SetActive(false);
        Stage2_4.SetActive(true);
    }

}
