using UnityEngine;


namespace VS.CW2RTS.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private BasicUnit knight, warrior, archer, healer;

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
                case "healer":
                    unit = healer;
                    break;
                case "player healer":
                    unit = healer;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found or does not exist!");
                    return null;
            }
            return unit.baseStats;
        }
    }
}


