using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float responTime = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (weapon != null)
                {
                    other.GetComponent<Fighter>().EquipWeapon(weapon);
                    StartCoroutine(HideForSeconds(responTime));
                }
            }
        }

        //무기 픽업 후 지정 시간 뒤에 다시 리스폰
        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool flag)
        {
            this.GetComponent<SphereCollider>().enabled = flag;
            transform.GetChild(0).gameObject.SetActive(flag);

            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(flag);
            }
        }
    }
}
