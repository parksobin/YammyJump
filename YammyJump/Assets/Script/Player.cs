using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.UIElements;

public class Player : MonoBehaviour
{
    public Joystick js; // 조이스틱 오브젝트를 저장할 변수.
    public float speed; // 조이스틱에 의해 움직일 오브젝트의 속도.
    private Vector2 CharScale; // 초기 스케일 값 저장.
    public float jumpPower;  //점프 값 입력 변수
    public Rigidbody2D rb;
    private bool isJumping = false;
    public Button jumpButton;

    bool isClick = false;
    bool isMove = false;

    int jumpcount = 0;
    public LineRenderer line;
    public Transform hook;
    Vector2 mousedir;
    bool isHookActive;
    bool isLineMax;
    public bool isAttach;
    public float arm;

    public Animator animator ;

    //0507  추가
    [SerializeField]
    private GameObject Object;
    private bool check = false;

    void Start()
    {
        CharScale = transform.localScale; // 초기 스케일 값을 저장

        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
        isAttach = false;

        animator = GetComponent<Animator>();
        animator.SetBool("Jump", false);
        animator.SetBool("Walk", false);
        Object.SetActive(false);
    }

    void Update()
    {
            GrappingHook();
    }

    void FixedUpdate()
    {
            CharMove();
    }

    private void CharMove()
    {
        if (isJumping == true)
        {
            animator.SetBool("Walk", false);
        }
        //스틱과 키보드의 방향 저장
        float horizontalInput = Input.GetAxisRaw("Horizontal") + js.Horizontal;
        // y의 좌표를 0으로 정해 좌우로만 이동할 수 있도록함
        Vector2 dir = new Vector3(horizontalInput, 0);
        //크기 1로 유지 
        dir.Normalize();
        //오브젝트를 dir 방향으로 이동
        transform.position = (Vector3)transform.position + (Vector3)dir * speed * Time.deltaTime;

        // 오른쪽으로 이동하면 스케일을 CharScale로 설정
        if (horizontalInput > 0)
        {
            
            transform.localScale = new Vector3(CharScale.x, CharScale.y);
            isMove = true;
            animator.SetBool("Walk", true);
        }
        // 왼쪽으로 이동하면 오브젝트의 모습을 좌우반전
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-CharScale.x, CharScale.y);
            isMove = true;
            animator.SetBool("Walk", true);
        }
        //가만히 있을 때
        else 
        {
            animator.SetBool("Walk", false);
            isMove = false;
        }

    }

    //점프
    public void Jump()
    {
        if (!isJumping)
        {
            animator.SetBool("Jump", true);
            rb.velocity = Vector3.up * jumpPower;
            isJumping = true;
            animator.SetBool("Walk", false);
        }
    }

    // groung 태그와 충돌 여부를 확인해 다시 점프를 수행 할 수 있도록 함
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("Ground");
            animator.SetBool("Jump", false);
            isJumping = false;

            // 훅을 땅에 닿았을 때 무조건 사라지도록 처리
            isHookActive = false;
            isLineMax = false;
            hook.gameObject.SetActive(false);
        }
    }
    /*
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
    */
    private IEnumerator Emphasis(GameObject gameObject)
    {
         float increase =0.1f;

        while (true)
        {
            //늘어나는 거
            while (gameObject.GetComponent<Transform>().localScale.x < 2.2f)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + +increase
                                                                , gameObject.transform.localScale.y + 0);
                yield return new WaitForSeconds(0.12f); //크기가 바뀌는 속도
            }
            yield return new WaitForSeconds(0.05f); //중간에 멈추는 텀
        }
    }

    public void GrappingHook() // 키보드 조작
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            if (jumpcount < 3)
            {
                jumpcount++;
            }
            print(jumpcount);
        }

        if (jumpcount >= 1 && !isJumping)
        {
            jumpcount = 0;
        }

        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (Input.GetKeyDown(KeyCode.Space) && !isHookActive && jumpcount > 1 && isMove) // 점프하고 훅 안 쓰고 점프 두번이면서 움직일때 훅 날림
        {
            hook.position = transform.position;
            mousedir = new Vector2(js.Horizontal, js.Vertical).normalized;
            isHookActive = true;
            hook.gameObject.SetActive(true);

            Object.SetActive(true);
            if (!check)
            {
                StartCoroutine("Emphasis", Object);
                check = true;
            }
        }

        if (isHookActive && !isLineMax && !isAttach && isMove)// 훅 날리고 줄 최대치 아니고 링에 안 닿았을 때
        {
            hook.Translate(mousedir * Time.deltaTime * 15);

            if (Vector2.Distance(transform.position, hook.position) > arm)
            {
                isLineMax = true;
            }
        }
        else if (isHookActive && isLineMax && !isAttach && isMove) // 훅 날리고 줄 최대치고 링에 안 닿았을 때
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 15);
            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                hook.gameObject.SetActive(false);
            }
        }
        else if (isAttach) // 손이 오브젝트에 붙었을 때
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isAttach = false;
                isHookActive = false;
                isLineMax = false;
                hook.GetComponent<Ring>().joint2D.enabled = false;
                hook.gameObject.SetActive(false);
            }
        }
    }

    public void OnClick() // 버튼 조작
    {
        isClick = true;

        if (isClick == true)
        {
            Jump();
            if (jumpcount < 3) { jumpcount++; }
            print(jumpcount);
        }

        if (jumpcount >= 1 && !isJumping && isMove) { jumpcount = 0; }

        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (isClick == true && !isHookActive && jumpcount > 1 && isMove)
        {
            hook.position = transform.position;
            mousedir = new Vector2(js.Horizontal, js.Vertical).normalized;
            isHookActive = true;
            hook.gameObject.SetActive(true);
        }

        if (isHookActive && !isLineMax && !isAttach && isMove)
        {
            hook.Translate(mousedir * Time.deltaTime * 15);

            if (Vector2.Distance(transform.position, hook.position) > arm)
            {
                isLineMax = true;
            }
        }

        else if (isHookActive && isLineMax && !isAttach && isMove)
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * 15);
            if (Vector2.Distance(transform.position, hook.position) < 0.1f)
            {
                isHookActive = false;
                isLineMax = false;
                hook.gameObject.SetActive(false);
            }
        }

        else if (isAttach)
        {
            if (isClick == true)
            {
                isAttach = false;
                isHookActive = false;
                isLineMax = false;
                hook.GetComponent<Ring>().joint2D.enabled = false;
                hook.gameObject.SetActive(false);
            }
        }
    }
}