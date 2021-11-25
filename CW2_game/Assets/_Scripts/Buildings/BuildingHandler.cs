using UnityEngine;

namespace VS.CW2RTS.Buildings
{
    public class BuildingHandler : MonoBehaviour
    {
        public static BuildingHandler instance;

        [SerializeField]
        private BasicBuilding barracks, enemyBasicBuilding, enemyAttackTower, enemyCore;

        public LayerMask eBuildingLayer;


        private void Awake()
        {
            instance = this;
        }

        public BuildingStatTypes.Base GetBasicBuildingStats(string type)
        {
            BasicBuilding building;
            switch (type)
            {
                case "barrack":
                    building = barracks;
                    break;
                case "enemyBasicBuildin":
                    building = enemyBasicBuilding;
                    break;
                case "enemyAttackTowe":
                    building = enemyAttackTower;
                    break;
                case "enemyCore":
                    building = enemyCore;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
            return building.baseStats;
        }
    }

}

