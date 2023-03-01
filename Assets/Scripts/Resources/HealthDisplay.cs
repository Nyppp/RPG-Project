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
        //플레이어 체력바와 퍼센트 수치 표시
        [SerializeField] Slider playerhealthBar;
        [SerializeField] Text playerhealthText;

        //타겟(몬스터) 체력 표시
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
            if (fighter.GetTarget() == null)
            {
                enemyhealthText.text = "NONE";
            }
            else
            {
                enemyhealthPercentage = fighter.GetTarget().GetPercentage();
                enemyhealthText.text = String.Format("{0:0}%", enemyhealthPercentage);
            }
        }
    }
}
