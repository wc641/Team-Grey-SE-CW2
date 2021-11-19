using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VS.CW2RTS.Units.Player;


namespace VS.CW2RTS.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Arrow : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        public void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
        public void Update()
        {
            
            //navAgent.SetDestination(GetComponent<PlayerUnit>().aggroTarget.position);
        }
    }
}

