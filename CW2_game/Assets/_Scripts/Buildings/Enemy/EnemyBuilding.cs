using UnityEngine;
using UnityEngine.AI;
using VS.CW2RTS.Units;

namespace VS.CW2RTS.Buildings.Enemy
{
    public class EnemyBuilding : MonoBehaviour
    {
        private NavMeshObstacle navObstacle;

        public BasicBuilding buildingType;

        public BasicUnit unitType;

        [HideInInspector]
        public BuildingStatTypes.Base baseStats;

        public UnitStatDisplay statDisplay;

        [SerializeField] private DrawProjectile line;

        private Collider[] rangeColliders;

        private Transform aggroTarget;

        private UnitStatDisplay aggroUnit;

        private bool hasAggro = false;

        private float distance;

        private float attackCooldown;


        private void Start()
        {
            baseStats = buildingType.baseStats;
            statDisplay.SetStatDisplayBasicBuilding(baseStats, false);
            navObstacle = GetComponent<NavMeshObstacle>();
        }

        private void Update()
        {
            attackCooldown -= Time.deltaTime;

            if (buildingType.type == BasicBuilding.buildingType.EnemyAttackTower)
            {
                if (!hasAggro)
                {
                    CheckForEnemyTargets();
                }
                else
                {
                    Attack();
                }
            }
        }

        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                if (i + 1 > rangeColliders.Length && unitType.type == BasicUnit.unitType.Archer)
                {
                    i++;
                }
                else
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    hasAggro = true;
                    break;
                }
            }
        }

        private void Attack()
        {
            if (aggroTarget == null)
            {
                aggroUnit = null;
                hasAggro = false;
            }
            else
            {
                distance = Vector3.Distance(aggroTarget.position, transform.position);
            }

            if (distance <= baseStats.attackRange + 1 && aggroTarget != null)
            {
                if (attackCooldown <= 0)
                {
                    aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                    ProjectileRaycast.Shoot(transform.position, aggroTarget.position);
                    line.SetupProjectile(transform.position, aggroTarget.position);
                    aggroUnit.TakeDamage(baseStats.attack);
                    attackCooldown = baseStats.attackSpeedCooldown;
                }
            }
            else
            {
                aggroTarget = null;
                aggroUnit = null;
                hasAggro = false;

            }
        }
    }
}

