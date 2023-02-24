using UnityEngine;
using RPG.Core;
using System;

using RPG.Resources;

namespace RPG.Combat
{
    //���� �����͸� ��ũ���ͺ� ������Ʈ�� ����
    //���� ���� : ��Ÿ�, ������, ������, ���� �ִϸ��̼�

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

        //���� ���Ⱑ ����տ� ���� �������� Ȯ��
        public Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        //���⸦ �����ϴ� �� ��ġ�� ���� ����
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            //���� ������ ��ü
            if(weaponPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }

            //�ֿ� ���Ⱑ �⺻ ���ݸ���� ������ �����ϱ� ����
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            //���� ����� �ִٸ� �� ������� ��ü
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }

            //�⺻ ����� ����Ѵٸ�, �ش� ������ �ִϸ��̼��� ���
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        //���� ��ü ��, ������ ���� �ִ� ���� �ı�
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

        //���Ÿ� ���� �Ǻ�
        public bool HasProjectile()
        {
            return projectile != null;
        }

        //����ü �߻�
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, WeaponDamage);
        }
    }
}