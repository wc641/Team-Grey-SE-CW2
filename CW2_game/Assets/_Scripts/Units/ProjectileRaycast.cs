using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VS.CW2RTS.Units
{
    public static class ProjectileRaycast
    {
        public static void Shoot(Vector3 startPos, Vector3 endPos)
        {
            Vector3 shootDir = (startPos - endPos).normalized;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(endPos, shootDir);
        }
    }
}

