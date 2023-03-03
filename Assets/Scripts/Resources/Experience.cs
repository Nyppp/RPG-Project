using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoint = 0f;

        public void GainExp(float exp)
        {
            experiencePoint += exp;
        }
    }
}