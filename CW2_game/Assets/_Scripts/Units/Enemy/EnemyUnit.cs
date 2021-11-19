using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace VS.CW2RTS.Units.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;

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
                CheckForEnemyTargets();
            }
            else
            {
                Attack();
                MoveToAggroTarget();
            }
        }

        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                aggroTarget = rangeColliders[i].gameObject.transform;
                aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                hasAggro = true;
                break;
            }
            
        }

        private void Attack()
        {
            if (attackCooldown <= 0 && distance <= baseStats.attackRange + 1)
            {   
                aggroUnit.TakeDamage(baseStats.attack);
                attackCooldown = baseStats.attackSpeedCooldown;
            }
        }

        private void MoveToAggroTarget()
        {
            if (aggroTarget == null)
            {
                navAgent.SetDestination(transform.position);
                hasAggro = false;
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

