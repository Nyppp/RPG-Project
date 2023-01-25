using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class ControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Start()
        {
            //Action �븮�ڿ� �Լ� �߰�
            //�÷��̾�� ��� Ÿ�Ӷ����� ����Ǵ� �߿�, �÷��̾� �Է��� ���ѽ�Ų��.
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;

            player = GameObject.FindWithTag("Player");
        }

        public void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        public void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
}
