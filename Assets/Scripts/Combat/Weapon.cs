using UnityEngine;
using RPG.Core;

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

        [SerializeField] Projectile projectile = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] bool isRightHanded = true;

        //현재 무기가 어느손에 끼는 무기인지 확인
        public Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if(weaponPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                Instantiate(weaponPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }


        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target);
        }
    }
}