using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup cg;

        private void Start()
        {
            cg = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeIn(float time)
        {
            while (cg.alpha > 0)
            {
                cg.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeOut(float time)
        {
            while (cg.alpha < 1)
            {
                cg.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

    }
}
