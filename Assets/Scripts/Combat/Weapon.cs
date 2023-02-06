using UnityEngine;

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