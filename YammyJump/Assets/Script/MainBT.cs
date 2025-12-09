using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBT : MonoBehaviour
{
    public GameObject MapBT;
    public GameObject StageMG;
    public Image die;
    public int b;
    public GameObject Pannel;
    public GameObject Back;
    public void Option()
    {
        Pannel.SetActive(true);
    }

    public void Option_Back()
    {
        Pannel.SetActive(false);
    }

    public void StageChoice()
    {
        SceneManager.LoadScene("StageChoice");
    }

    public void Main()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
    }


    public void Exit()
    {
        Application.Quit();
        print("Exit");
    }

    public void Jungle_Map()
    {
        SceneManager.LoadScene("Jungle_Map");
    }

    public void Stage1_1()
    {
        SceneManager.LoadScene("Stage1_1");
        Time.timeScale = 1.0f;
    }
    
    public void Desert_Map()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Stage2_1");
    }

}
