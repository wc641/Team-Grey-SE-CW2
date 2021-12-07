using UnityEngine;


namespace VS.CW2RTS.UI
{
    public class DontDestroyAudio : MonoBehaviour
    {
        static DontDestroyAudio instance = null;
        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}

