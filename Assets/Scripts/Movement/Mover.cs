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

        //�÷��̾� ĳ������ �̵��ӵ� z���� ������ǥ �������� ��ȯ��Ų ����, �ִϸ����Ϳ� ��ġ ����ȭ
        //�̵��ӵ��� ���� �ִϸ��̼��� �������� �ٲ��(blend tree)
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
