using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;

        Transform target;
        float timeSinceLastAttack = 0;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            //플레이어 공격 -> 공격 범위 밖에 있다면, 사거리에 올 떄 까지 쫓아감
            if (target == null) return;

            if (!GetInRange())
            {
                GetComponent<RPG.Movement.Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        //타겟과 거리 계산
        private bool GetInRange()
        {
            return Vector3.Distance(target.position, transform.position) <= weaponRange;
        }

        //액션스케쥴러에 공격 명령을 수행중이라고 알린 다음, 공격 타겟 설정
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        //애니메이션 이벤트 처리
        void Hit()
        {
            print("데미지 계산");
        }
    }
}