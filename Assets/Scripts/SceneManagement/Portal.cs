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
            //비동기 씬 로드 -> 기존 포탈 오브젝트를 통해 특정 동작을 수행할 수 있다
            DontDestroyOnLoad(this);
            print("씬 로드 시작");

            //비동기 씬 로드가 마무리 될 때 까지 스레드에서 진행중인 모든 작업 보류
            yield return SceneManager.LoadSceneAsync(currentSceneIdx);

            Portal otherPortal = GetOtherPortal();
            LocatePlayerToSpawnPoint(otherPortal);

            print("씬 로드 완료");
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

