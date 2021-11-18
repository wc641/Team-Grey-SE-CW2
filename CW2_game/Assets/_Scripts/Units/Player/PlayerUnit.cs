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

        public BasicUnit unitType;

        [HideInInspector]
        public UnitStatTypes.Base baseStats;

        public UnitStatDisplay statDisplay;

        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            baseStats = unitType.baseStats;
            statDisplay.SetStatDisplayBasicUnit(baseStats, true);
        }

        public void MoveUnit(Vector3 destination)
        {
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
    }
}
