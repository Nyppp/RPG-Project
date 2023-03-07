using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RPG.Core;

namespace RPG.Resources
{
    public class ExpDisplay : MonoBehaviour
    {
        [SerializeField] Text expText;

        Experience exp;

        void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            exp = player.GetComponent<Experience>();
        }

        void Update()
        {
            SetExp();
        }

        void SetExp()
        {
            expText.text = String.Format("EXP : {0}", exp.GetExperiencePoint());
        }
    }
}
