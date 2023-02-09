using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

//체력 클래스는 모든 동작의 기본이 됨 -> core 네임스페이스에 배치
namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        //사망 체크
        public bool IsDead()
        {
            return isDead;
        }

        //데미지 계산
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print("체력 : " + health);

            if (health <= 0)
            {
                Dead();
            }
        }

        //사망 처리
        void Dead()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health;
        }
        public void RestoreState(object state)
        {
            float healthData = (float)state;
            health = healthData;

            if (health <= 0)
            {
                Dead();
            }
        }
    }
}