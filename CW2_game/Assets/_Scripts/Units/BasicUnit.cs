using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VS.CW2RTS.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        public enum unitType
        {
            Knight,
            Warrior,
            Archer
        };

        [Space(15)]
        [Header("Unit Settings")]

        public unitType type;
        public string unitName;
        public GameObject playerPrefab;
        public GameObject enemyPrefab;
        public GameObject icon;
        public float spawnTime;

        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(40)]

        public UnitStatTypes.Base baseStats;
    }
}


