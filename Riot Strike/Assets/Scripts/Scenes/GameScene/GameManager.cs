#region Access
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
using XavHelpTo.Set;
using XavHelpTo.Get;
using XavHelpTo.Know;
using XavHelpTo.Change;
using Environment;
using RefreshText =  EndRefresh.Text;
# endregion
namespace GameScene
{
    /// <summary>
    /// Manager of the Game Scene
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private static GameManager _;

        private const float MINUTE = 60;

        [Header("Game Manager")]
        public GameObject obj_postProcessing;
        public AudioClip clip_music;

        [Header("Time to End")]
        private float currentTime=0;
        public float timerToEnd = 10 * MINUTE;
        private bool gameEnd = false;
        public Image img_time; // forma parte de los elementos de time

        [Space]
        [SerializeField] private bool isCheatOn = false;


        [Space(10)]
        [Header("UI Elements")] 
        public EGameModal currentModal = EGameModal.HUD;
        public Transform tr_parent_screens;
        private CanvasGroup[] canvaScreens;
        public ImageController imgCtrl_curtain;

        [Space(10)]
        [Header("Character")]
        public PlayerBody player;
        public Character[] characters;
        public bool IsPause { get; private set; } = false;


        [Space(10)]
        [Header("End Screen")]
        public GameObject pref_itemResult;
        public Transform tr_parent_itemResults;

        #endregion
        #region Events
        private void Awake()
        {
            this.Singleton(ref _, false);
            imgCtrl_curtain.gameObject.SetActive(true);
            Time.timeScale = 1;
            CursorSystem.Hide();
        }
        private void OnEnable(){Subscribe();}

        private void Start() {
            WarpPlayerTo(TargetManager.SpawnPoints.Any());


            SetPlayerCharacter(DataSystem.Get.characterSelected);
            player.gameObject.SetActive(true);

            tr_parent_screens.Components(out canvaScreens);
            StartCoroutine(ChangeModal(EGameModal.HUD, false));

            obj_postProcessing.SetActive(DataSystem.Get.switch_configs[ESwitchOpt.POST_PROCESSING.ToInt()]);
            AudioSystem.Play(clip_music);
        }
        private void Update()
        {
            CheckTimeToEnd();
            CheatInput();
        }
        private void OnDisable(){UnSuscribe();}
        #endregion
        #region MEthods
        /// <summary>
        /// Do the subscriptions of the actions
        /// </summary>
        private void Subscribe()
        {
            player.OnPause += Pause;
            player.OnDeath += GameEnd;
        }
        /// <summary>
        /// Do the unsuscriptions of the actions
        /// </summary>
        private void UnSuscribe()
        {
            player.OnPause -= Pause;
            player.OnDeath -= GameEnd;
        }
        /// <summary>
        /// Move the player to the position assigned
        /// </summary>
        /// <param name="pos"></param>
        private void WarpPlayerTo(Transform point) => player.transform.position = point.position;
        /// <summary>
        /// Assign he character of the player
        /// </summary>
        private void SetPlayerCharacter(int charIndex) => player.character = characters[charIndex];
        /// <summary>
        /// Pause the game or not
        /// </summary>
        public void Pause() {
            IsPause = !IsPause;

            Time.timeScale = (!IsPause).ToInt();

            if (IsPause)
            {
                CursorSystem.Show();
                StartCoroutine(ChangeModal(EGameModal.PAUSE, false));
            }
            else
            {
                CursorSystem.Hide();
                StartCoroutine(ChangeModal(EGameModal.HUD, false));
            }
        }
        /// <summary>
        /// Change the modal in GameManager
        /// Do a fade or not.
        /// dependency with <seealso cref="Utils.Fade(bool, CanvasGroup)"/>
        /// </summary>
        IEnumerator ChangeModal(EGameModal toModal, bool fade) {
            foreach (CanvasGroup c in canvaScreens)
            {
                //hides the others modals
                if (!c.Equals(GetCanvasOf(toModal))) StartCoroutine(Utils.Fade(true, c));
            }
            StartCoroutine(Utils.Fade(fade, GetCanvasOf(toModal)));
            yield return new WaitForEndOfFrame();
        }


        /// <summary>
        /// Returns the <seealso cref="CanvasGroup"/> of the specified element
        /// </summary>
        private CanvasGroup GetCanvasOf(EGameModal modal)
        {
            tr_parent_screens.GetChild(modal.ToInt()).Component(out CanvasGroup canvas);
            return canvas;
        }
        /// <summary>
        /// Go to the MenuScene
        /// </summary>
        public void GoToMenu()
        {
            Time.timeScale = 1;
            Scenes.MENU_SCENE.ToScene();
        }
        /// <summary>
        /// Play again
        /// </summary>
        public void PlayAgain(){
            Time.timeScale = 1;
            Scenes.GAME_SCENE.ToScene();
        }



        #region End MEthods
        /// <summary>
        /// Check the time and updates the images to give feedback of the status of the game
        /// </summary>
        private void CheckTimeToEnd()
        {
            if (!gameEnd)
            {
               img_time.fillAmount = 1- currentTime.PercentOf(timerToEnd, true);
                //pasará una sola vez puesto que se acabo el tiempo
                if (timerToEnd.TimerFlag(ref gameEnd,ref currentTime))
                {
                    img_time.fillAmount = 0;
                    GameEnd();
                }

            }
        }
        /// <summary>
        /// Displays the end of the Game
        /// starts the game over
        /// </summary>
        private void GameEnd()
        {
            if (gameEnd) return; // 🛡
            gameEnd = true;
            //"Starts game End".Print("blue");
            //ENEMIES KILLED
            InstantiateResultItem("end_results_enemiesKilled", FindObjectOfType<EnemyManager>().enemiesKilled.ToString());
            //TIME
            InstantiateResultItem("end_results_time", $"{ (currentTime / MINUTE).ToInt()} minutes");
            //SHOT
            InstantiateResultItem("end_results_shot", player.countAttacks.ToString());

            StartCoroutine(ChangeModal(EGameModal.END, false));
        }
        /// <summary>
        /// Creates the end item with a key to be translated and their own value
        /// </summary>
        private void InstantiateResultItem(string key, string value)
        {
            Instantiate(pref_itemResult, tr_parent_itemResults)
                .transform
                .Component(out RefreshController _refresh);

            //ENEMIES KILLED
            _refresh.Translate(RefreshText.TITLE, key);
            _refresh.RefreshText(RefreshText.VALUE, value);
        }
        #endregion
        #region Cheats Methods
        /// <summary>
        /// Check if the player use the cheat command
        /// </summary>
        private void CheatInput()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
            {
                isCheatOn = !isCheatOn;
                //"Cheats!".Print("magenta");
                StartCoroutine(Utils.Fade(!isCheatOn, GetCanvasOf(EGameModal.CHEAT)));

                if (isCheatOn) CursorSystem.Show();
                else CursorSystem.Hide();

            }
        }
        /// <summary>
        /// Change the character with one of the options displayed in the cheat window
        /// </summary>
        public void ChangePlayerCharacter(int i)
        {
            player.gameObject.SetActive(false);
            SetPlayerCharacter(i);
            player.SetStat();
            player.character.Init(player);
            player.gameObject.SetActive(true);
            player.StartVisual();

        }
        /// <summary>
        /// Modify the time
        /// </summary>
        public static void ChangeTimeScale(float percent) => Time.timeScale = percent;
        /// <summary>
        /// Change the life of the player
        /// </summary>
        public static void ChangePlayerLife(float percent) => _.ChangePlayerLife(in percent);
        private void ChangePlayerLife(in float percent)
        {
            int max = player.stat.RealHealth;
            //curamos por completo
            player.AddLife(max);
            player.AddLife(percent.QtyOf(max, true) - max);
        }
        /// <summary>
        /// Generates a random buff if exist space
        /// </summary>
        public void GenerateBuff()
        {
            FindObjectOfType<BuffManager>().GenerateBuff();
        }

        /// <summary>
        /// Add 1 point to the player stat
        /// </summary>
        public void AddPointStat(int i) => ChangePlayerStat(i, 1);
        /// <summary>
        /// Removes 1 point to the player stat
        /// </summary>
        public void RemovePointStat(int i) => ChangePlayerStat(i, -1);
        /// <summary>
        /// Can add or remove stats
        /// </summary>
        private void ChangePlayerStat(int i, int qty)
        {
            switch ((EStat)i)
            {
                case EStat.ATTACK:
                    player.stat.STRENGHT += qty;
                    break;
                case EStat.DEFENSE:
                    player.stat.DEFENSE += qty;
                    player.AddLife(0);
                    break;
                case EStat.SPEED:
                    player.stat.SPEED += qty;
                    break;
            }
        }
        /// <summary>
        /// Teleports the character to the assigned position
        /// </summary>
        public static void WarpTo(float index)
        {
            _.player.controller.enabled = false;
            _.WarpPlayerTo(TargetManager.SpawnPoints[index.ToInt()]);
            _.player.controller.enabled = true;
        }
        #endregion

        #endregion
    }
    }
