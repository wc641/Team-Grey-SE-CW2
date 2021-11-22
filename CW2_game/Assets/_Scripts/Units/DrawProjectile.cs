using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VS.CW2RTS.Units
{
    public class DrawProjectile : MonoBehaviour
    {
        public LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetupProjectile(Vector3 start, Vector3 end)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            StartCoroutine(timeProjectile());
        }

        IEnumerator timeProjectile()
        {
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
            lineRenderer.enabled = false;
        }
    }
}

