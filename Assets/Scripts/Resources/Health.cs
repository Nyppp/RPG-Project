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