using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectDefend.Player;

namespace ProjectDefend.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler Instance;

        public LayerMask playerUnitsLayer;
        public LayerMask enemyUnitsLayer;

        [SerializeField]
        private Unit builder, tank;

        private void Awake()
        {
            playerUnitsLayer = LayerMask.NameToLayer("PlayerUnits");
            enemyUnitsLayer = LayerMask.NameToLayer("EnemyUnits");

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public (float cost, float attack, float attackRange,float attackAggro, float health, float healthArmor, float shield, float shieldArmor) GetBasicUnitStats(string type)
        {
            Unit basicUnit;
            switch (type)
            {
                case "builder":
                    basicUnit = builder;
                    break;
                case "tank":
                    basicUnit = tank;
                    break;
                default:
                    return (0, 0, 0, 0, 0, 0, 0,0);
            }
            return (basicUnit.statUnit.cost, basicUnit.statUnit.attack, basicUnit.statUnit.attackRange,basicUnit.statUnit.attackAggro, basicUnit.statUnit.health, basicUnit.statUnit.healthArmor, basicUnit.statUnit.shield, basicUnit.statUnit.shieldArmor);
        }

        public void SetBasicUnitStats(Transform type)
        {
            Transform playerUnits = PlayerManager.instance.playerUnits;
            Transform enemyUnits = PlayerManager.instance.enemyUnits;


            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0, child.name.Length - 1).ToLower();
                    var stats = GetBasicUnitStats(unitName);

                    if (type == playerUnits)
                    {
                        Player.PlayerUnit playerUnit = unit.GetComponent<Player.PlayerUnit>();

                        playerUnit.baseStats.cost = stats.cost;
                        playerUnit.baseStats.attack = stats.attack;
                        playerUnit.baseStats.attackRange = stats.attackRange;
                        playerUnit.baseStats.attackAggro = stats.attackAggro;
                        playerUnit.baseStats.health = stats.health;
                        playerUnit.baseStats.healthArmor = stats.healthArmor;
                        playerUnit.baseStats.shield = stats.shield;
                        playerUnit.baseStats.shieldArmor = stats.shieldArmor;
                    }
                    else if (type == enemyUnits)
                    {
                        ProjectDefend.Enemy.Units.EnemyUnit enemyUnit = unit.GetComponent<ProjectDefend.Enemy.Units.EnemyUnit>();

                        enemyUnit.baseStats.cost = stats.cost;
                        enemyUnit.baseStats.attack = stats.attack;
                        enemyUnit.baseStats.attackRange = stats.attackRange;
                        enemyUnit.baseStats.attackAggro = stats.attackAggro;
                        enemyUnit.baseStats.health = stats.health;
                        enemyUnit.baseStats.healthArmor = stats.healthArmor;
                        enemyUnit.baseStats.shield = stats.shield;
                        enemyUnit.baseStats.shieldArmor = stats.shieldArmor;
                    }


                }
            }
        }
    }
}

