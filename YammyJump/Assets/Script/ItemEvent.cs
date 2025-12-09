using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemEvent : MonoBehaviour
{
    private bool isSliding = false;
    private float slideDuration = 3f;
    private float slideTimer = 0f;
    private Rigidbody2D characterRigidbody;
    private PhysicsMaterial2D originalPhysicsMaterial;
    private int isMoving = 0;

    public Joystick joystick;
    public Image clear;
    public Image die;
    public GameObject sand;
    public Rigidbody2D sandRig;
    public Collider2D sandCol;
    public int goal;
    public int goalcount = 0;


    public GameObject SandShadow_1;
    public GameObject Cactus1_1;




    private void Start()
    {
        // 필요한 컴포넌트들을 가져옴
        characterRigidbody = GetComponent<Rigidbody2D>();
        originalPhysicsMaterial = characterRigidbody.sharedMaterial;
    }
    
    private void Update()
    {
        // 미끄러짐 타이머 업데이트
        if (isSliding)
        {
            slideTimer += Time.deltaTime;

            // 일정 시간이 지나면 미끄러짐 종료
            if (slideTimer >= slideDuration)
            {
                isSliding = false;
                slideTimer = 0f;

                // 마찰력을 다시 원래대로 복원
                if (originalPhysicsMaterial != null)
                {
                    characterRigidbody.sharedMaterial = originalPhysicsMaterial;
                }
            }
        }

        // Joystick의 Horizontal 값을 가져옴
        float horizontalInput = joystick.Horizontal;

        // 플레이어 위치를 현재 위치에서 이동
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime);

        // 좌우 판별
        if (horizontalInput > 0)
        {
            // 오른쪽으로 이동 중
            isMoving = 1;
        }
        else if (horizontalInput < 0)
        {
            // 왼쪽으로 이동 중
            isMoving = 2;
        }
        else
        {
            // 가로 방향으로 입력이 없음
            isMoving = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        // 충돌한 오브젝트가 바나나인지 확인
        if (other.CompareTag("banana"))
        {
            // 바나나와 충돌하면 마찰력을 0으로 설정
            if (originalPhysicsMaterial != null)
            {
                characterRigidbody.sharedMaterial = null;
            }

            // 일시적인 힘을 가해 미끄러지도록 함
            float slidingForce = 400f; // 미끄러질 때의 힘 (원하는 값으로 조절)
            if (isMoving == 1)
            {
                characterRigidbody.AddForce(new Vector2(slidingForce, 0f));
            }
            else if(isMoving == 2)
            {
                characterRigidbody.AddForce(new Vector2(-slidingForce, 0f));
            }


            // 미끄러지는 상태 시작
            isSliding = true;
            other.gameObject.SetActive(false);
        }
        */
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //오브젝트 / 태그 충돌시 코드 실행

        if (collision.gameObject.CompareTag("Item"))
        {
            goalcount += 1;
            print(goalcount);
            collision.gameObject.SetActive(false);
            if (goalcount == goal)
            {
                clear.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        /*
        if (collision.gameObject.CompareTag("Lava"))
        {
            die.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        */
        
        if (collision.gameObject.CompareTag("cactus"))
        {
            die.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        
    }


}
