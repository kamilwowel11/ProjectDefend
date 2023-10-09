using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDefend.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName ="Create New Unit")]
    public class Unit : ScriptableObject
    {
        public enum unitType
        {
            Builder,
            Tank,
        };
        [Header("Unit settings")]
        [Space(15)]

        public bool isPlayerUnit;
        public unitType type;
        public string Name;
        public GameObject unitPrefab;

        [Space(40)]
        [Header("Unit stats")]
        [Space(15)]

        public UnitStatsType.Base statUnit;
    }
}

