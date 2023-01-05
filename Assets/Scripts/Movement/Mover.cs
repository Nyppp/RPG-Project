using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

namespace RPG.Movement
{
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

        public void MoveTo(Vector3 destination)
        {
            nav.isStopped = false;
            nav.destination = destination;
        }

        public void Stop()
        {
            nav.isStopped = true;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }
    }

}
