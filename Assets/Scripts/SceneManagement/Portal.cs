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
        //포탈끼리 연결하는 태그(같은 태그끼리 이동)
        enum DestinationIdentifier
        {
            A,B,C,D,E
        }

        int sceneCount;
        int currentSceneIdx;

        float time = 0f;
        TextMesh textmesh;

        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] int nextScene = -1;
        [SerializeField] float fadeTime = 1f;
        [SerializeField] float waitTime = .5f;

        private void Start()
        {
            sceneCount = SceneManager.sceneCountInBuildSettings - 1;
            currentSceneIdx = SceneManager.GetActiveScene().buildIndex;

            textmesh = GetComponent<TextMesh>();
        }

        private void Update()
        {
            time += Time.deltaTime;
            textmesh.text = destination.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                currentSceneIdx = nextScene;

                //SceneManager.LoadScene(currentSceneIdx);
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (nextScene == -1)
            {
                print("포탈을 타고 갈 맵이 지정되지 않음");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();

            //비동기 씬 로드 -> 기존 포탈 오브젝트를 통해 특정 동작을 수행할 수 있다
            DontDestroyOnLoad(this);

            print("씬 로드 시작");
            float currentTime = time;

            //비동기 씬 로드가 마무리 될 때 까지 스레드에서 진행중인 모든 작업 보류 + 페이드 인, 아웃으로 씬 로딩 간 화면을 가림
            yield return fader.FadeOut(fadeTime);
            yield return SceneManager.LoadSceneAsync(currentSceneIdx);

            //씬이 로드된 이후 후처리 작업(다른 맵에서의 플레이어 위치 조정)
            Portal otherPortal = GetOtherPortal();
            LocatePlayerToSpawnPoint(otherPortal);

            //조정이 끝나면 페이드인으로 화면을 보여준다.
            yield return new WaitForSeconds(waitTime);
            yield return fader.FadeIn(fadeTime);


            //오브젝트 수(레벨 규모)에 따른 로딩 시간 측정
            float completeTime = time;
            GameObject[] objects = FindObjectsOfType<GameObject>();

            print("씬 로드 완료, 로드 시간 : " + (completeTime - currentTime)*1000 + "ms / 씬 밀집도(오브젝트 수) : " + objects.Length );
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
            foreach (Portal pt in FindObjectsOfType<Portal>())
            {
                if (pt == this) continue;
                if (pt.destination != destination) continue;

                return pt;
            }

            return null;
        }
    }
}

