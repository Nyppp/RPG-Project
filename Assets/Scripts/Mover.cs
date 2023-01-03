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

    //플레이어 캐릭터의 이동속도 z값을 로컬좌표 기준으로 변환시킨 다음, 애니메이터에 수치 동기화
    //이동속도에 따라서 애니메이션의 움직임이 바뀐다(blend tree)
    private void UpdateAnimator()
    {
        Vector3 velocity = nav.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        currentSpeed = localVelocity.z;

        GetComponent<Animator>().SetFloat("forwardSpeed", (currentSpeed));
    }

    //마우스 클릭 지점에 레이캐스트를 쏴서 플레이어 네비게이션 매쉬를 사용해 캐릭터 이동
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
