using UnityEngine;

namespace VS.CW2RTS.UI.HUD
{
    public class Action : MonoBehaviour
    {
        public void OnClick()
        {
            ActionFrame.instance.StartSpawnTimer(name);
        }
    }
}

