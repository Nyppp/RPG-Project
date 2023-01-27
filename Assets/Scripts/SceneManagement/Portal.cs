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
        //��Ż���� �����ϴ� �±�(���� �±׳��� �̵�)
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
                print("��Ż�� Ÿ�� �� ���� �������� ����");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();

            //�񵿱� �� �ε� -> ���� ��Ż ������Ʈ�� ���� Ư�� ������ ������ �� �ִ�
            DontDestroyOnLoad(this);

            print("�� �ε� ����");
            float currentTime = time;

            //�񵿱� �� �ε尡 ������ �� �� ���� �����忡�� �������� ��� �۾� ���� + ���̵� ��, �ƿ����� �� �ε� �� ȭ���� ����
            yield return fader.FadeOut(fadeTime);
            yield return SceneManager.LoadSceneAsync(currentSceneIdx);

            //���� �ε�� ���� ��ó�� �۾�(�ٸ� �ʿ����� �÷��̾� ��ġ ����)
            Portal otherPortal = GetOtherPortal();
            LocatePlayerToSpawnPoint(otherPortal);

            //������ ������ ���̵������� ȭ���� �����ش�.
            yield return new WaitForSeconds(waitTime);
            yield return fader.FadeIn(fadeTime);


            //������Ʈ ��(���� �Ը�)�� ���� �ε� �ð� ����
            float completeTime = time;
            GameObject[] objects = FindObjectsOfType<GameObject>();

            print("�� �ε� �Ϸ�, �ε� �ð� : " + (completeTime - currentTime)*1000 + "ms / �� ������(������Ʈ ��) : " + objects.Length );
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

