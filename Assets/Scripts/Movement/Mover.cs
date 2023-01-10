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
        NavMeshAgent nav;

        float currentSpeed = 0f;

        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
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

        //�׺���̼� �Ž��� ���� �������� ���� �׺�Ž��� ������ ����
        public void MoveTo(Vector3 destination)
        {
            nav.isStopped = false;
            nav.destination = destination;
        }

        public void Cancel()
        {
            nav.isStopped = true;
        }

        //�׼� �����췯�� �̵� �׼����̶�� �˸�
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
    }
}
