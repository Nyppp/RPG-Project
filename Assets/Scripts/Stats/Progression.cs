using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;


        //optimal code : dictinary 타입 저장으로 탐색 시간 감소
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            Buildlookup();


            float[] levels =  lookupTable[characterClass][stat];

            if (levels.Length < level)
            {
                return 0;
            }

            return levels[level - 1];
            //Legacy code : 모든 탐색에 대해 시간이 오래 걸림
            /*public float GetStat(Stat stat, CharacterClass characterClass, int level)
            {
                //모든 캐릭터 클래스 탐색
                foreach (ProgressionCharacterClass progressionClass in this.characterClasses)
                {
                    if (progressionClass.characterClass != characterClass) continue;

                    //모든 스탯 탐색
                    foreach (ProgressionStat progressionStat in progressionClass.stats)
                    {
                        if (progressionStat.stat != stat) continue;

                        if (progressionStat.levels.Length < level) continue;

                        return progressionStat.levels[level - 1];
                    }
                }
                return 0;
            }*/
        }

        private void Buildlookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach(ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                var statlookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionCharacterClass.stats)
                {
                    statlookupTable[progressionStat.stat] = progressionStat.levels;
                }


                lookupTable[progressionCharacterClass.characterClass] = statlookupTable;
            }
        }

        //클래스, 구조체의 직렬화 -> Serializable 사용, 내부 변수도 SerializeField 선언하거나 public으로 공개
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}