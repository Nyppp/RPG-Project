using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Combat;
using RPG.Movement;

//제어 처리에 관련한 네임스페이스 선언
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        //플레이어 입력에 대한 전반부 처리
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
            //마우스 클릭 지점에 레이캐스트를 쏴서 플레이어 네비게이션 매쉬를 사용해 캐릭터 이동
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    //직접 플레이어 오브젝트를 이동시키는 코드는 mover 스크립트에 작성(입력 제어와 실행 구간 분리)
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

