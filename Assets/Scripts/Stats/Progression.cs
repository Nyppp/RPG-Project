using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;


        //optimal code : dictinary Ÿ�� �������� Ž�� �ð� ����
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
            //Legacy code : ��� Ž���� ���� �ð��� ���� �ɸ�
            /*public float GetStat(Stat stat, CharacterClass characterClass, int level)
            {
                //��� ĳ���� Ŭ���� Ž��
                foreach (ProgressionCharacterClass progressionClass in this.characterClasses)
                {
                    if (progressionClass.characterClass != characterClass) continue;

                    //��� ���� Ž��
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

        //Ŭ����, ����ü�� ����ȭ -> Serializable ���, ���� ������ SerializeField �����ϰų� public���� ����
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