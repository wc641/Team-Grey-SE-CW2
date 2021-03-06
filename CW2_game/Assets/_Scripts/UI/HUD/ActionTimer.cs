using System.Collections;
using UnityEngine;

namespace VS.CW2RTS.UI.HUD
{
    public class ActionTimer : MonoBehaviour
    {
        public static ActionTimer instance = null;

        public void Awake()
        {
            instance = this;
        }

        public IEnumerator SpawnQueueTimer()
        {
            if (ActionFrame.instance.spawnQueue.Count > 0)
            {
                Debug.Log($"Waiting for {ActionFrame.instance.spawnQueue[0]}.");

                yield return new WaitForSeconds(ActionFrame.instance.spawnQueue[0]);

                ActionFrame.instance.SpawnObject();  

                if(ActionFrame.instance.spawnQueue.Count > 0)
                {
                    StartCoroutine(SpawnQueueTimer());
                }
            }
        }
    }

}
