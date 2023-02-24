using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{ 
    //캐릭터의 레벨, 클래스, 체력 등의 스탯을 담은 클래스
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetHealth()
        {
            return progression.GetHealth(characterClass, startingLevel);
        }
    }
}