using UnityEngine;

namespace RPG.Combat
{
    //무기 데이터를 스크립터블 오브젝트로 저장
    //포함 정보 : 사거리, 데미지, 프리펩, 공격 애니메이션

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] float weaponRange = 1f;
        public float WeaponRange
        {
            get { return weaponRange; }
        }

        [SerializeField] float weaponDamage = 5f;
        public float WeaponDamage
        {
            get { return weaponDamage; }
        }
        
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if(weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}