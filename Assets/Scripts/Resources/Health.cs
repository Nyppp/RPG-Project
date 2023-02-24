using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;
using RPG.Stats;
using RPG.Core;


namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        private void Start()
        {
            health = GetComponent<BaseStats>().GetHealth();
        }

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