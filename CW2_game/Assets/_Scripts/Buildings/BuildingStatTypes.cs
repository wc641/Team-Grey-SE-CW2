using UnityEngine;

namespace VS.CW2RTS.Buildings
{
    public class BuildingStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float health, armour, attack, attackRange, aggroRange, attackSpeedCooldown;
        }
    }

}

