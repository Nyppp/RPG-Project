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
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
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
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            //���콺 Ŭ�� ������ ����ĳ��Ʈ�� ���� �÷��̾� �׺���̼� �Ž��� ����� ĳ���� �̵�
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    //���� �÷��̾� ������Ʈ�� �̵���Ű�� �ڵ�� mover ��ũ��Ʈ�� �ۼ�(�Է� ����� ���� ���� �и�)
                    GetComponent<RPG.Movement.Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
