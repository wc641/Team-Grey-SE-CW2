using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace VS.CW2RTS.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;
        //public LineRenderer lineRenderer;
        [SerializeField] private DrawProjectile line;

        public BasicUnit unitType;

        //[HideInInspector]
        public UnitStatTypes.Base baseStats;

        public UnitStatDisplay statDisplay;

        // combat vars
        private Collider[] rangeColliders;

        private bool unitIsMoving = false;
        public bool playerCommandToBeExecuted = false;

        public Transform aggroTarget;

        public UnitStatDisplay aggroUnit;

        public bool hasAggro = false;

        private float distance;

        private float attackCooldown;

        private Vector3 currentPos;
        private Vector3 lastPos;


        private Animator animator;

        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            baseStats = unitType.baseStats;
            statDisplay.SetStatDisplayBasicUnit(baseStats, true);
        }

        private void Update()
        {
            attackCooldown -= Time.deltaTime;

            currentPos = transform.position;
            if (currentPos == lastPos)
            {
                unitIsMoving = false;
                if (!hasAggro && !playerCommandToBeExecuted && !unitIsMoving)
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
                
            }
            else
            {
                unitIsMoving = true;
                playerCommandToBeExecuted = false;
            }

            if (hasAggro && !playerCommandToBeExecuted)
            {
                MoveToAggroTarget();
                Attack();             
            }

            lastPos = currentPos;
            animator.SetBool("isMoving", unitIsMoving);
            animator.SetBool("isAttacking", hasAggro);
        }

        public void MoveUnit(Vector3 destination)
        {
            navAgent.stoppingDistance = (0);
            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
                navAgent.SetDestination(destination);
            }
            else
            {
                navAgent.SetDestination(destination);
            }
        }

        private void CheckForEnemyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.eUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                aggroTarget = rangeColliders[i].gameObject.transform;  
                hasAggro = true;
                break;
            }
        }

        private void CheckForAllyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, baseStats.aggroRange, UnitHandler.instance.pUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                aggroTarget = rangeColliders[i].gameObject.transform;
                hasAggro = true;
                break;
            }
        }

        private void Attack()
        {
            if (attackCooldown <= 0 && distance <= baseStats.attackRange + 1)
            {
                aggroUnit = aggroTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                if (unitType.type == BasicUnit.unitType.Archer)
                {
                    ProjectileRaycast.Shoot(transform.position, aggroTarget.position);
                    line.SetupProjectile(transform.position, aggroTarget.position);
                }              
                if (unitType.type == BasicUnit.unitType.Healer)
                {
                    aggroUnit.Heal(baseStats.attack);
                }
                else
                {
                    aggroUnit.TakeDamage(baseStats.attack);
                }
                attackCooldown = baseStats.attackSpeedCooldown;
            }
        }

        public void MoveToAggroTarget()
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

                if (distance <= baseStats.aggroRange | playerCommandToBeExecuted)
                {
                    navAgent.SetDestination(aggroTarget.position);
                }
            }
        }
    }
}
