using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

//ü�� Ŭ������ ��� ������ �⺻�� �� -> core ���ӽ����̽��� ��ġ
namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        //��� üũ
        public bool IsDead()
        {
            return isDead;
        }

        //������ ���
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print("ü�� : " + health);

            if (health <= 0)
            {
                Dead();
            }
        }

        //��� ó��
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