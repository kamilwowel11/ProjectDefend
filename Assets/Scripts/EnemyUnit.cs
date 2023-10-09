using ProjectDefend.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectDefend.Enemy.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyUnit : MonoBehaviour
    {
        private NavMeshAgent navAgent;

        public UnitStatsType.Base baseStats;

        private Collider[] rangColliders;
        private Transform aggroTarget;
        private bool hasAggro;
        private float distance;

        private void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!hasAggro)
            {
                CheckForEnemyTargets();
            }
            else
            {
                MoveToAggroTarget();
            }
        }

        private void CheckForEnemyTargets()
        {
            rangColliders = Physics.OverlapSphere(transform.position, baseStats.attackRange);

            for (int i = 0; i < rangColliders.Length; i++)
            {
                if (rangColliders[i].gameObject.layer == UnitHandler.Instance.playerUnitsLayer)
                {
                    aggroTarget = rangColliders[i].gameObject.transform;
                    hasAggro = true;
                    break;
                }
            }
        }

        private void MoveToAggroTarget()
        {
            distance = Vector3.Distance(aggroTarget.position, transform.position);
            navAgent.stoppingDistance = (baseStats.attackRange + 1);

            if(distance <= baseStats.attackAggro)
            {
                navAgent.SetDestination(aggroTarget.position);
            }
        }
    }
}
