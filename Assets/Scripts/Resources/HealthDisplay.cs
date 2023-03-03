using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RPG.Combat;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        //�÷��̾� ü�¹ٿ� �ۼ�Ʈ ��ġ ǥ��
        [SerializeField] Slider playerhealthBar;
        [SerializeField] Text playerhealthText;

        //Ÿ��(����) ü�� ǥ��
        [SerializeField] Slider enemyhealthBar;
        [SerializeField] Text enemyhealthText;

        Health health;
        Fighter fighter;

        float playerhealthPercaentage;
        float enemyhealthPercentage;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            SetPlayerHealthPercentage();
            SetEnemyHealthPercentage();
        }

        void SetPlayerHealthPercentage()
        {
            playerhealthPercaentage = health.GetPercentage();
            playerhealthBar.value = playerhealthPercaentage;

            playerhealthText.text = String.Format("{0:0}%", playerhealthPercaentage);
        }

        void SetEnemyHealthPercentage()
        {
            if (fighter.GetTarget() == null || fighter.GetTarget().IsDead())
            {
                enemyhealthBar.gameObject.SetActive(false);
            }
            else
            {
                enemyhealthBar.gameObject.SetActive(true);

                enemyhealthPercentage = fighter.GetTarget().GetPercentage();
                enemyhealthBar.value = enemyhealthPercentage;

                enemyhealthText.text = String.Format("{0:0}%", enemyhealthPercentage);
            }
        }
    }
}
