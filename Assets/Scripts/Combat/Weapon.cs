using UnityEngine;
using RPG.Core;
using System;

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

        const string weaponName = "Weapon";

        //현재 무기가 어느손에 끼는 무기인지 확인
        public Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        //무기를 착용하는 손 위치에 무기 생성
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if(weaponPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        //무기 교체 시, 기존에 끼고 있던 무기 파괴
        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if(oldWeapon == null)
            {
                return;
            }

            oldWeapon.name = "DESTROYING";

            Destroy(oldWeapon.gameObject);
        }

        //원거리 무기 판별
        public bool HasProjectile()
        {
            return projectile != null;
        }

        //투사체 발사
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, WeaponDamage);
        }
    }
}