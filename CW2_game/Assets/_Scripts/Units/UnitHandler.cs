using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VS.CW2RTS.Player;


namespace VS.CW2RTS.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private BasicUnit knight, warrior, archer;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake()
        {
            instance = this;
        }

        public UnitStatTypes.Base GetBasicUnitStats(string type)
        {
            BasicUnit unit;
            switch (type)
            {
                case "knight":
                    unit = knight;
                    break;
                case "player knight":
                    unit = knight;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "player warrior":
                    unit = warrior;
                    break;
                case "archer":
                    unit = archer;
                    break;
                case "player archer":
                    unit = archer;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
            return unit.baseStats;
        }
    }
}


