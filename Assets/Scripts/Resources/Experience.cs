using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoint = 0f;

        public void GainExp(float exp)
        {
            experiencePoint += exp;
        }

        public float GetExperiencePoint()
        {
            return experiencePoint;
        }


        public object CaptureState()
        {
            return experiencePoint;
        }

        public void RestoreState(object state)
        {
            this.experiencePoint = (float)state;
        }
    }
}