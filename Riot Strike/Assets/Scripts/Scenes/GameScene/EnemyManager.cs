#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
namespace GameScene
{
    /// <summary>
    /// Manager of the enemy generation, count and assignation of patrol and types
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        #region Variables

        private Transform[] patrols;

        [Header("Enemy Manager")]
        public bool isGenerating = true;
        public int indexCurrentEnemy; // cambia mientras pasa el tiempo
        public GameObject[] prefs_enemy;

        [Space]
        [Header("Stats")]
        public int enemiesKilled=0;



        #endregion
        #region Events
        private void Awake()
        {
            enemiesKilled = 0;
        }
        private void Update()
        {
            
        }
        #endregion
        #region Methods

        private void GenerateEnemy()
        {

        }


        #endregion
    }
}
