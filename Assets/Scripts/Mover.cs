using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    [SerializeField] Transform target;
    NavMeshAgent nav;

    float currentSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    //�÷��̾� ĳ������ �̵��ӵ� z���� ������ǥ �������� ��ȯ��Ų ����, �ִϸ����Ϳ� ��ġ ����ȭ
    //�̵��ӵ��� ���� �ִϸ��̼��� �������� �ٲ��(blend tree)
    private void UpdateAnimator()
    {
        Vector3 velocity = nav.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        currentSpeed = localVelocity.z;

        GetComponent<Animator>().SetFloat("forwardSpeed", (currentSpeed));
    }

    //���콺 Ŭ�� ������ ����ĳ��Ʈ�� ���� �÷��̾� �׺���̼� �Ž��� ����� ĳ���� �̵�
    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit)
        {
            nav.destination = hit.point;
        }
    }
}
