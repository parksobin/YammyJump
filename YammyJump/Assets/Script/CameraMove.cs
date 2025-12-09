using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;  // 캐릭터의 Transform을 할당
    public float smoothSpeed = 0.125f;  // 카메라 이동 속도 (원하는 값으로 조절)
    public Vector2 minPosition = new Vector2(-23.65f, -5.31f);  // 최소 x, y 좌표
    public Vector2 maxPosition = new Vector2(23.64f, 5.41f);    // 최대 x, y 좌표

    void LateUpdate()
    {
        if (target != null)
        {
            // 캐릭터의 위치를 기반으로 이동할 위치 계산
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y),
                transform.position.z
            );

            // 부드러운 이동을 위해 Lerp 사용
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 카메라 위치 갱신
            transform.position = smoothedPosition;
        }
    }
}
