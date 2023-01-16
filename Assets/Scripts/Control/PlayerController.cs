using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using RPG.Core;
using RPG.Movement;

//���� ó���� ������ ���ӽ����̽� ����
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Fighter fighter;
        Health health;
        Mover move;

        private void Awake()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            move = GetComponent<Mover>();
        }

        //�÷��̾� �Է¿� ���� ���ݺ� ó��
        void Update()
        {
            if (health.IsDead()) return;

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
                if (target == null) continue;

                if (fighter.CanAttack(target.gameObject) != true)
                {
                    continue;
                }

                if (Input.GetMouseButton(1))
                {
                    fighter.Attack(target.gameObject);
                    
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

