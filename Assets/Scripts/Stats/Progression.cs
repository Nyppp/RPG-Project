using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in this.characterClasses)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level-1];
                }
            }
            return 0;
        }

        //클래스, 구조체의 직렬화 -> Serializable 사용, 내부 변수도 SerializeField 선언하거나 public으로 공개
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;
        }
    }
}