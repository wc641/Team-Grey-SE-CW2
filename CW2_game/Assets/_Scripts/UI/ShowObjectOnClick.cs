using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VS.CW2RTS.UI
{
    public class ShowObjectOnClick : MonoBehaviour
    {
        public GameObject objectToShow;
        // Start is called before the first frame update
        void Start()
        {
            objectToShow.SetActive(false);
        }

        public void Show()
        {
            objectToShow.SetActive(true);
        }
    }
}