using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //Observer Pattern의 기반 동작이 되는 ActionScheduler 클래스
    //플레이어가 무슨 동작(이동, 공격)을 하는지 전달받고, 현재 상태가 무엇인지 저장한다.
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if(action == currentAction) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            
            currentAction = action;
        }
    }
}
