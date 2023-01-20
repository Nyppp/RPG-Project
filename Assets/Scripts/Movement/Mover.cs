using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Combat;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent nav;
        Health health;

        float currentSpeed = 0f;

        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            nav.enabled = !health.IsDead();

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

        //네비게이션 매쉬를 통한 움직임을 위해 네비매쉬의 목적지 설정
        public void MoveTo(Vector3 destination, float fraction = 1f)
        {
            nav.isStopped = false;
            nav.speed = maxSpeed * Mathf.Clamp01(fraction);
            nav.destination = destination;
        }

        public void Cancel()
        {
            nav.isStopped = true;
        }

        //액션 스케쥴러에 이동 액션중이라고 알림
        public void StartMoveAction(Vector3 destination, float fraction = 1f)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, fraction);
        }
    }
}
