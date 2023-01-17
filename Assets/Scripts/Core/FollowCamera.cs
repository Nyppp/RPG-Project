using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float rotateSpeed = 1f;

        //플레이어 움직임 이후 후처리 카메라설정 (update -> lateupdate 라이프사이클)
        private void LateUpdate()
        {
            transform.position = target.position;

            RotateCamera();
        }

        private void RotateCamera()
        {
            if(Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, rotateSpeed*Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, -rotateSpeed*Time.deltaTime, 0);
            }
        }
    }
}