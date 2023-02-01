using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWraper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}

