using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VS.CW2RTS.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private BasicUnit worker, warrior, healer;

        private void Awake()
        {
            instance = this;
        }

        public (int cost, int attack, int attackRange, int health, int armour) GetBasicUnitStats(string type)
        {
            BasicUnit unit;
            switch (type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                case "healer":
                    unit = healer;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return (0, 0, 0, 0, 0);
            }
            return (unit.cost, unit.attack, unit.attackRange, unit.health, unit.armour);
        }

        public void SetBasicUnitStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = GetBasicUnitStats(unitName);
                    Player.PlayerUnit pU;

                    if (type == CW2RTS.Player.PlayerManager.instance.playerUnits)
                    {
                        // set unit stats in each unit 
                        pU = unit.GetComponent<Player.PlayerUnit>();
                        pU.cost = stats.cost;
                        pU.attack = stats.attack;
                        pU.attackRange = stats.attackRange;
                        pU.heath = stats.health;
                        pU.armour = stats.armour;
                    }
                    else if (type == CW2RTS.Player.PlayerManager.instance.enemyUnits)
                    {
                        // set enemy stats
                    }
                    
                }
            }
        }
    }

}


