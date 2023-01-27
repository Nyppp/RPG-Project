using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.Core
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float rotateSpeed = 1f;
        [SerializeField] float zoomSpeed = 1f;

        [SerializeField] float maxZoom = 25f;
        [SerializeField] float minZoom = 10f;

        CinemachineVirtualCamera cinemachineCamera;
        CinemachineFramingTransposer cameraBody;

        private void Start()
        {
            cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
            cameraBody = cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        //플레이어 움직임 이후 후처리 카메라설정 (update -> lateupdate 라이프사이클)
        private void LateUpdate()
        {
            RotateCamera();
            ZoomCamera();
        }

        private void ZoomCamera()
        {
            cameraBody.m_CameraDistance += Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;

            if(cameraBody.m_CameraDistance < minZoom)
            {
                cameraBody.m_CameraDistance = minZoom;
            }

            if (cameraBody.m_CameraDistance > maxZoom)
            {
                cameraBody.m_CameraDistance = maxZoom;
            }
        }

        private void RotateCamera()
        {
            if(Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, rotateSpeed*Time.deltaTime, 0, Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, -rotateSpeed*Time.deltaTime, 0, Space.World);
            }
        }
    }
}