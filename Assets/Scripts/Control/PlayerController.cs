using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using RPG.Core;
using RPG.Movement;

//제어 처리에 관련한 네임스페이스 선언
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

        //플레이어 입력에 대한 전반부 처리
        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        //전투 동작에 대한 처리
        private bool InteractWithCombat()
        {
            //땅, 지형지물을 일단 모두 충돌 결과로 불러온다.
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                //충돌 결과들을 모두 탐색하여 전투 가능한 오브젝트가 있으면, 전투 수행
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
            //마우스 클릭 지점에 레이캐스트를 쏴서 플레이어 네비게이션 매쉬를 사용해 캐릭터 이동
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    //직접 플레이어 오브젝트를 이동시키는 코드는 mover 스크립트에 작성(입력 제어와 실행 구간 분리)
                    move.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            //카메라 기준으로 레이를 쏴서 충돌 결과 반환
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}

