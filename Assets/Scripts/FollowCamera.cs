using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;


    //플레이어 움직임 이후 후처리 카메라설정 (update -> lateupdate 라이프사이클)
    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
