using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

using RPG.Core;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        int sceneCount;
        int currentSceneIdx;

        [SerializeField] Transform spawnPoint;

        private void Start()
        {
            sceneCount = SceneManager.sceneCountInBuildSettings - 1;
            currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                if(currentSceneIdx ==  sceneCount)
                {
                    currentSceneIdx = 0;
                }
                else
                {
                    ++currentSceneIdx;
                }

                //SceneManager.LoadScene(currentSceneIdx);
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            //�񵿱� �� �ε� -> ���� ��Ż ������Ʈ�� ���� Ư�� ������ ������ �� �ִ�
            DontDestroyOnLoad(this);
            print("�� �ε� ����");

            //�񵿱� �� �ε尡 ������ �� �� ���� �����忡�� �������� ��� �۾� ����
            yield return SceneManager.LoadSceneAsync(currentSceneIdx);

            Portal otherPortal = GetOtherPortal();
            LocatePlayerToSpawnPoint(otherPortal);

            print("�� �ε� �Ϸ�");
            Destroy(this.gameObject);
        }

        private void LocatePlayerToSpawnPoint(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            GameObject[] portal = GameObject.FindGameObjectsWithTag("Portal");

            foreach (var pt in portal)
            {
                if(pt != this.gameObject)
                {
                    return pt.GetComponent<Portal>();
                }
            }

            return null;
        }
    }
}

