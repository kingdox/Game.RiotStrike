#region Access
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OWOButton = OWORefresh.Button;
using OWOInput = OWORefresh.Input;
using OWOText = OWORefresh.Text;
using OWO;
using XavHelpTo;
# endregion
namespace MenuScene
{
    /// <summary>
    /// Manages the inputField to set the ip and the conection with
    /// <seealso cref="OWOSytem"/>
    /// </summary>
    public class OWOManager : MonoBehaviour
    {
        #region Variables
        private const string KEY_SUCCESS="owo_connection_success";
        private const string KEY_FAILED="owo_connection_failed";
        [Header("OWO Manager")]
        public RefreshController refresh;

        public Action OnEnter;
        #endregion
        #region Events
        private void OnEnable()
        {
            Subscribe();
        }
        private void Start()
        {
            refresh.GetInput(OWOInput.IP).text.Print();
            refresh
                .GetButton(OWOButton.CONNECT)
                .onClick
                .AddListener(TryConection)
            ;

        }
        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Start subscribing if is enter or is connect
        /// </summary>
        private void Subscribe()
        {
            OWOSytem.OnConnectResult += ShowResultConection;
        }
        /// <summary>
        /// Do the unsubscriptions wether is disabled
        /// </summary>
        private void UnSubscribe()
        {
            OWOSytem.OnConnectResult -= ShowResultConection;
        }



        /// <summary>
        /// Try to connect with <see cref="OWOSytem"/>
        /// </summary>
        public void TryConection() => OWOSytem.Connect(refresh.GetInput(OWOInput.IP).text);

        /// <summary>
        /// Shows the result based on the event <seealso cref="OWOSytem"/>
        /// </summary>
        public void ShowResultConection(bool result)
        {
            Text txt = refresh.GetText(OWOText.RESULT_CONECTION);

            txt.color = result ? Color.green : Color.red;

            refresh.Translate(OWOText.RESULT_CONECTION,
                result
                ? KEY_SUCCESS
                : KEY_FAILED
            );


        }

        #endregion
    }
}
