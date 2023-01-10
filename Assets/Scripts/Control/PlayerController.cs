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
        Fighter fighter;
        Mover move;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            move = GetComponent<Mover>();
        }

        //�÷��̾� �Է¿� ���� ���ݺ� ó��
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        //���� ���ۿ� ���� ó��
        private bool InteractWithCombat()
        {
            //��, ���������� �ϴ� ��� �浹 ����� �ҷ��´�.
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                //�浹 ������� ��� Ž���Ͽ� ���� ������ ������Ʈ�� ������, ���� ����
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (fighter.CanAttack(target) != true) continue;

                if (Input.GetMouseButtonDown(1))
                {
                    fighter.Attack(target);
                    
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
                    move.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            //ī�޶� �������� ���̸� ���� �浹 ��� ��ȯ
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}

