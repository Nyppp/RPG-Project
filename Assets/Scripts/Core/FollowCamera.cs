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

        //�÷��̾� ������ ���� ��ó�� ī�޶��� (update -> lateupdate ����������Ŭ)
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