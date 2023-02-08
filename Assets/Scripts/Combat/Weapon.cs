using UnityEngine;
using RPG.Core;

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

        //���� ���Ⱑ ����տ� ���� �������� Ȯ��
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