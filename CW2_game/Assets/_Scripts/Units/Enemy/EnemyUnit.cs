using UnityEngine;
using UnityEngine.AI;


namespace VS.CW2RTS.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;
        [SerializeField] private DrawProjectile line;

        public BasicUnit unitType;

        [HideInInspector]
        public UnitStatTypes.Base baseStats;

        public UnitStatDisplay statDisplay;

        private Collider[] rangeColliders;

        private Transform aggroTarget;

        private UnitStatDisplay aggroUnit;

        private bool hasAggro = false;

        private float distance;

        private float attackCooldown;

        private void Start()
        {
            baseStats = unitType.baseStats;
            statDisplay.SetStatDisplayBasicUnit(baseStats, false);
            navAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        private void Update()
        {  
            attackCooldown -= Time.deltaTime;

            if (!hasAggro)
            {
                if (unitType.type == BasicUnit.unitType.Healer)
                {
                    CheckForAllyTargets();
                }
                else
                {
                    CheckForEnemyTargets();
                }              
            }
            else
            {
                MoveToAggroTarget();
                Attack();
            }
        }

        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                if (i+1 > rangeColliders.Length && unitType.type == BasicUnit.unitType.Archer)
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

        private void CheckForAllyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.eUnitLayer);
            
            for (int i = 0; i < rangeColliders.Length;)
            {
                aggroTarget = rangeColliders[i].gameObject.transform;
                hasAggro = true;
                break;
            }
        }

        private void Attack()
        {
            if (attackCooldown <= 0 && distance <= baseStats.attackRange + 1 && aggroTarget != null)
            {
                aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                if (unitType.type == BasicUnit.unitType.Healer)
                {
                    aggroUnit.Heal(baseStats.attack);
                }
                else
                {
                    if (unitType.type == BasicUnit.unitType.Archer)
                    {
                        ProjectileRaycast.Shoot(transform.position, aggroTarget.position);
                        line.SetupProjectile(transform.position, aggroTarget.position);
                    }
                    aggroUnit.TakeDamage(baseStats.attack);
                }
                attackCooldown = baseStats.attackSpeedCooldown;
            }
        }

        private void MoveToAggroTarget()
        {
            if (aggroTarget == null)
            {
                navAgent.SetDestination(transform.position);
                hasAggro = false;
                aggroUnit = null;
            }
            else
            {
                distance = Vector3.Distance(aggroTarget.position, transform.position);
                navAgent.stoppingDistance = (baseStats.attackRange + 1);

                if (distance <= baseStats.aggroRange)
                {
                    navAgent.SetDestination(aggroTarget.position);
                }
            }
        }
    }

}

