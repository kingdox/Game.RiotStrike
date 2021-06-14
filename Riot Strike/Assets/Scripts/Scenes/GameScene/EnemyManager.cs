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


        [Space]
        [Header("Generate")]
        public Character[] characters_enemies;
        public GameObject pref_character;


        [Space]
        [Header("Stats")]
        public int enemiesKilled=0;



        #endregion
        #region Events
        private void Awake()
        {
            enemiesKilled = 0;
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            
        }
        #endregion
        #region Methods

        private void GenerateEnemy()
        {
            //TargetManager.Get.tr_parent_spawnPoints

        }


        #endregion
    }
}
