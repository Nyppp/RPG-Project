using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using RPG.Movement;

//���� ó���� ������ ���ӽ����̽� ����
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        //�÷��̾� �Է¿� ���� ���ݺ� ó��
        void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
            
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(1))
            {
                MoveToCursor();
            }
        }

        //���콺 Ŭ�� ������ ����ĳ��Ʈ�� ���� �÷��̾� �׺���̼� �Ž��� ����� ĳ���� �̵�
        public void MoveToCursor()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                //���� �÷��̾� ������Ʈ�� �̵���Ű�� �ڵ�� mover ��ũ��Ʈ�� �ۼ�(�Է� ����� ���� ���� �и�)
                GetComponent<RPG.Movement.Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}

