using UnityEngine;
using VS.CW2RTS.InputManager;
using VS.CW2RTS.UI;

namespace VS.CW2RTS.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;
        public static int numberOfplayerUnits;
        public static bool enemyCoreDestroyed = false;
        public LevelEndScreen levelEndScreen;

        public Transform playerUnits;
        public Transform enemyUnits;

        public Transform playerBuildings;
        public Transform enemyBuildings;


        private void Awake()
        {
            instance = this;
            SetBasicStats(playerUnits);
            SetBasicStats(enemyUnits);
            SetBasicStats(playerBuildings);
            //SetBasicStats(enemy)
        }

        private void Start()
        {
            enemyCoreDestroyed = false;
        }

        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
            
            if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
                levelEndScreen.SetScreenToShow(false);

            if (enemyCoreDestroyed)
                levelEndScreen.SetScreenToShow(true);
        }

        public void SetBasicStats(Transform type)
        {
            foreach (Transform child in type)
            {
                foreach (Transform tf in child)
                {
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();
                    //var stats = Units.UnitHandler.instance.GetBasicUnitStats(unitName);

                    if (type == playerUnits)
                    {
                        // set unit stats in each unit 
                        Units.Player.PlayerUnit pU = tf.GetComponent<Units.Player.PlayerUnit>();
                        pU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }
                    else if (type == enemyUnits)
                    {
                        // set enemy stats
                        Units.Enemy.EnemyUnit eU = tf.GetComponent<Units.Enemy.EnemyUnit>();
                        eU.baseStats = Units.UnitHandler.instance.GetBasicUnitStats(name);
                    }

                    else if (type == playerBuildings)
                    {
                        Buildings.Player.PlayerBuilding pB = tf.GetComponent<Buildings.Player.PlayerBuilding>();
                        pB.baseStats = Buildings.BuildingHandler.instance.GetBasicBuildingStats(name);
                    }

                }
            }
        }
    }
}

