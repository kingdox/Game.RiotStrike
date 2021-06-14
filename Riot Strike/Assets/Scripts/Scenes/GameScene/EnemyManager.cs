#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo.Get;
using XavHelpTo;
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
        public int indexCurrentEnemy=0; // cambia mientras pasa el tiempo


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
            indexCurrentEnemy = 0;
        }
        private void Start()
        {
            TargetManager.GetParentPatrols.Components(out patrols);

            GenerateEnemy();
        }
        private void Update()
        {
            
        }
        #endregion
        #region Methods

        /// <summary>
        /// Creates a enemy and warp randomly in the availables spawn points
        /// also it gets an AI dificulty based on the time (and the info in <seealso cref="EnemyGeneratorData"/>)
        /// Note: The enemy pref start disable so we will enable when it finishes
        /// </summary>
        private void GenerateEnemy()
        {
            //CREATE ENEMY
            Instantiate(pref_character,TargetManager.SpawnPoints.Any().position, Quaternion.identity, TargetManager.GetParentEnemies)
                .transform
                .Component(out Body enemyBody);

            //SET CHARACTER
            enemyBody.character = characters_enemies[indexCurrentEnemy];

            //SET IA STATS (PATROL, DELAYE, ETC ) TODO

            enemyBody.gameObject.SetActive(true);
        }


        #endregion
    }
}
