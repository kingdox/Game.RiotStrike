#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo.Get;
using XavHelpTo;
using XavHelpTo.Set;
using XavHelpTo.Know;
using XavHelpTo.Change;
using System.Linq;
using Dat = Environment.Data;

#endregion
namespace GameScene
{
    /// <summary>
    /// Manager of the enemy generation, count and assignation of patrol and types
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        #region Variables
        private static EnemyManager _;
        private const float MIN = 60;
        private Transform[] patrols;
        private Transform[] patrolsB;
        private Transform[] patrolsA;
        private enum EPatrolFloors{
            PATROL_B = 0,
            PATROL_A = 1,
        }
        [Header("Enemy Manager")]
        public bool isGenerating = true;
        public int indexCurrentEnemy=0; // cambia mientras pasa el tiempo
        private float countCurrentTimeToGenerate =0;

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
            this.Singleton(ref _,false);

            enemiesKilled = 0;
            indexCurrentEnemy = 0;
            isGenerating = true;
        }
        private void Start()
        {
            TargetManager.GetParentPatrols.Components(out patrols);
            patrols[EPatrolFloors.PATROL_B.ToInt()].Components(out patrolsB);
            patrols[EPatrolFloors.PATROL_A.ToInt()].Components(out patrolsA);

            GenerateEnemy();
        }
        private void Update()
        {
            if (isGenerating){
                CheckGenerator();

                //Si el tiempo de duracion para la generacion de enemigo ha concluido de esta etapa
                // genera un enemigo y le asigna los elementos
                if (TargetManager.GetParentEnemies.childCount <= EnemyConfigs.MAX_GENERATION_ENEMIES
                    && EnemyConfigs.GENERATION_TIME.TimerIn(ref countCurrentTimeToGenerate)){
                    //$"in {EnemyConfigs.GENERATION_TIME}. Enemy generated, type: {EnemyConfigs.ID_ENEMY}".Print("black");
                    GenerateEnemy();
                }
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Is the <seealso cref="EnemyManager"/> Keeping generating enemies?
        /// </summary>
        public static bool KeepGenerating(bool condition) => (_.isGenerating = condition);
        /// <summary>
        /// Check if it stills in the correct generator data
        ///if is -1 then it does not try to go next pos
        /// </summary>
        private void CheckGenerator()
        {
            //siempre que no sea -1 la condición, si el tiempo es mayor que el minimo de minutos entonces avanzamos al siguiente minimo
            if (
                !EnemyConfigs.MIN_CONDITION_TIME.Equals(-1)
                && GameManager.GetCurrentTime
                > (EnemyConfigs.MIN_CONDITION_TIME * MIN)
            )
            {
                indexCurrentEnemy = Know.NextIndex(true, Dat.ENEMY_GENERATOR.Length, indexCurrentEnemy);
                //$"Buscando siguiente minimo, ahora en {EnemyConfigs.ID_ENEMY}".Print();
            }

        }


        /// <summary>
        /// Returns the current configuration used to generate the enemies
        /// </summary>
        private EnemyGeneratorData EnemyConfigs => Dat.ENEMY_GENERATOR[indexCurrentEnemy];

        /// <summary>
        /// Creates a enemy and warp randomly in the availables spawn points
        /// also it gets an AI dificulty based on the time (and the info in <seealso cref="EnemyGeneratorData"/>)
        /// Note: The enemy pref start disable so we will enable when it finishes
        /// </summary>
        [ContextMenu("Gen enemy")]
        public void GenerateEnemy()
        {
            //CREATE ENEMY
            Instantiate(pref_character,TargetManager.SpawnPoints.Any().position, Quaternion.identity, TargetManager.GetParentEnemies)
                .transform
                .Component(out IABody ia);

            //SET CHARACTER
            ia.character = characters_enemies[indexCurrentEnemy];
            //iaBody.character = characters_enemies.Where(c => c.idStat.Equals(EnemyConfigs.ID_ENEMY as Character);

            //SET IA STATS (PATROL, DELAYE, ETC )
            ia.patrol = GetRandomPatrol();
            ia.iaStat.percentCastSpell = EnemyConfigs.ENEMY_PROBABILITY_TO_CAST_SPELL;
            ia.iaStat.delayAttack = EnemyConfigs.ENEMY_DELAY_ATTACK.Range();

            ia.gameObject.SetActive(true);
            ia.iaActive = true;

            ia.OnDeath += delegate { enemiesKilled++; };
        }


        /// <summary>
        /// Gets a random patrol betweeen the existents
        /// </summary>
        public Transform GetRandomPatrol()
        {
            Transform[] patrol;
            patrol = Get.RandomBool
                ? patrolsA
                : patrolsB
            ;
            return patrol.Any();
        }   
        #endregion
    }
}
