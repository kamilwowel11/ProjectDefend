using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectDefend.Units.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        [HideInInspector]
        public UnitStatsType.Base baseStats;

        private void OnEnable()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveUnit(Vector3 destination)
        {
            navAgent.SetDestination(destination);
        }
    }
}
