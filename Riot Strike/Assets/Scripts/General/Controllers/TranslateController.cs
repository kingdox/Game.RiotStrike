#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XavHelpTo;
# endregion
/// <summary>
/// Controls the translation asigned
/// </summary>
public class TranslateController : MonoBehaviour
{
    #region Variables
    public string key;
    public Text txt;
    //public RefreshControl refresh;
    //public bool startTranslate = true;
    #endregion
    #region Events
    private void Awake(){if (!txt) this.Component(out txt);}
    private void Start() {if (TranslateSystem.Inited) Translate();}
    private void OnEnable() => TranslateSystem.OnSetLanguage += Translate;
    private void OnDisable() => TranslateSystem.OnSetLanguage -= Translate;
    #endregion
    #region Methods
    /// <summary>
    /// Calls <seealso cref="TranslateSystem.Translate(in string)"/> to Translate the <seealso cref="txt"/>
    /// </summary>
    public void Translate() => txt.text = TranslateSystem.Translate(in key);
    #endregion
}
