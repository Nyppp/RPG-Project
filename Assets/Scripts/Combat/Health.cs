using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
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

            if(health <= 0)
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
        }
    }
}