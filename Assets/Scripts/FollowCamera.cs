using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;


    //�÷��̾� ������ ���� ��ó�� ī�޶��� (update -> lateupdate ����������Ŭ)
    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
