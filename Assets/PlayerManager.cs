using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectDefend.Inputs;

namespace ProjectDefend.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance;

        public Transform playerUnits;
        public Transform enemyUnits;

        private void Start()
        {
            instance = this;
            Units.UnitHandler.Instance.SetBasicUnitStats(playerUnits);
            Units.UnitHandler.Instance.SetBasicUnitStats(enemyUnits);
        }

        void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }
}

