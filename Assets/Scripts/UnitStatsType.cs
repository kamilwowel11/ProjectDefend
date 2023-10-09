using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDefend.Units
{
    public class UnitStatsType : ScriptableObject
    {
        [System.Serializable]
        public class Base // attackRange = aggroRange
        {
            public float cost, attackRange, attack, attackAggro, health, healthArmor, shield, shieldArmor;
        }
    }
}