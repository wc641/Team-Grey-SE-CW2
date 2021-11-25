using UnityEngine;


namespace VS.CW2RTS.Units
{
    public class UnitStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base
        {
            public float cost, aggroRange, attackRange, attackSpeedCooldown, attack, health, armour;
        }
    }

}

