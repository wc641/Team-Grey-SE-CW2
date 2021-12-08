using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VS.CW2RTS.UI
{
    public class LevelSelector : SceneSwitcher
    {
        public Button[] levelSelectButtons;

        public void Start()
        {   
            int levelReached = PlayerPrefs.GetInt("levelReached", 2);

            for (int i = 0; i < levelSelectButtons.Length; i++)
            {
                if (i + 2 > levelReached)
                    levelSelectButtons[i].interactable = false;
            }
        }

        public void Update()
        {
            // If running debug build, delete prefs with P
            if (Debug.isDebugBuild)
                if(Input.GetKeyDown(KeyCode.P))
                    PlayerPrefs.DeleteAll();
        }
    }
}
