using UnityEngine;

namespace VS.CW2RTS.Buildings.Player
{
    public class PlayerBuilding : MonoBehaviour
    {
        public BasicBuilding buildingType; 

        [HideInInspector]
        public BuildingStatTypes.Base baseStats;

        public Units.UnitStatDisplay statDisplay;

        private void Start()
        {
            baseStats = buildingType.baseStats;
            //statDisplay.SetStatDisplayBasicBuilding(baseStats, true);
        }
    }
}

