#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using XavHelpTo;
using Environment;
using UnityEngine.Events;
#endregion
/// <summary>
/// Manages all the information who exist in xml files and retireves his keys
/// </summary>
public class TranslateSystem : MonoBehaviour
{
    #region Variables
    private const string KEYNAME = "key";
    private string systemLanguage;
    private static TranslateSystem _;
    public Dictionary<string, string> dic_Lang;

    private bool inited;
    public static UnityAction OnSetLanguage;

    #region Debug Variables
        public const string PATH_LANG = "Lang/";
        public const string DEFAULT_LANG = "Spanish";
        [HideInInspector] public bool debug_mode = false;
        [HideInInspector] public string debug_folder = DEFAULT_LANG;
        [HideInInspector] public string debug_key = "lang";

    #endregion
    #endregion
    #region Events
    private void Awake() {
        inited = false;
        debug_mode = false;

        this.Singleton(ref _,true);
    }
    private void Start(){
        InitLang(DataSystem.Get.currentLang ); //TODO pasar a onEnable?
        //Traduce los que se suscribieron
        inited = true;
    }
    #endregion
    #region Methods
   
    /// <summary>
    /// Carga el archivo a buscar y lo inserta en un <see cref="TextAsset"/>
    /// </summary>
    private TextAsset LoadTextAsset(string folder, string fileName, string defaultName = default){
        TextAsset txt = Resources.Load(folder+fileName, typeof(TextAsset)) as TextAsset;
        if (!txt && (fileName == defaultName || defaultName == default)) return null;
        else return txt ?? LoadTextAsset(folder,defaultName);
    } 
    /// <summary>
    /// Carga el diccionario basado en el texto otorgado
    /// </summary>
    private Dictionary<string,string> LoadDictionary(TextAsset txt){
        if (txt is null) return null; //🛡
        Dictionary<string,string> dic = new Dictionary<string, string>();
        //Enumeramos los elementos
        IEnumerator elemEnum = LoadXml(txt).DocumentElement.GetEnumerator();
        while (elemEnum.MoveNext()){
            //linea actual de la fila
            XmlElement xmlItem = (XmlElement)elemEnum.Current;
            //Añadimos el key y su valor
            dic.Add(xmlItem.GetAttribute(KEYNAME), xmlItem.InnerText);
        }
        return dic;
    }
    /// <summary>
    /// Carga los <see cref="TextAsset"/> en un nuevo <seealso cref="XmlDocument"/>
    /// </summary>
    private XmlDocument LoadXml(TextAsset txt) {
        if (txt is null) return null; //🛡
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(txt.text);
        return xml;
    }
    /// <summary>
    /// Recupera el valor dentro de el archivo indicado usando un key
    /// </summary>
    private string GetValueIn(in Dictionary<string, string> dic, in string key) => !(dic is null) && dic.ContainsKey(key) ? dic[key] : key;


    #region  Public Methods
    /// <summary>
    /// Initializes the Language Dictionary
    /// </summary>
    public static void InitLang(string lang = default, string defaultLang = default) => _._InitLang(lang, defaultLang);
    public  void _InitLang(string lang = default, string defaultLang = default) 
    {
        systemLanguage = Application.systemLanguage.ToString();
        TextAsset txt = LoadTextAsset(
            PATH_LANG,
            lang ?? systemLanguage, // sets the lang or the system default lang
            defaultLang ?? DEFAULT_LANG //which lang set as default if the file does not exist?
        );
        dic_Lang = LoadDictionary(txt); // loads the Dictionary of the Lang
        //$"IDIOMA refresh".Print();
        RefreshTexts();
    }
    /// <summary>
    /// Makes a translation of the dictionary of language
    /// </summary>
    public static string Translate(in string key){
        string result = "";
        if (_ is null || _.dic_Lang is null){
            $"Error {nameof(TranslateSystem)} al cargar el key '{key}'".Print("red");
            return result; // 🛡
        }
        return _.GetValueIn(_.dic_Lang, key);
    }
    /// <summary>
    /// Invoca el refrescamiento para los suscritos
    /// </summary>
    [ContextMenu("Refrescar textos")]
    public void RefreshTexts() => OnSetLanguage?.Invoke();
    /// <summary>
    /// Show if is Inited
    /// </summary>
    public static bool Inited => !(_ is null) && _.inited;

    /// <summary>
    /// Set the internal value
    /// </summary>
    public void _EditorSetInstance() => _ = this;
    #endregion
    #endregion
}