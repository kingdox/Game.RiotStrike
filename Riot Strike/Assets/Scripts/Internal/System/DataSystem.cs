#region Access
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Environment;
using XavHelpTo;
using StorageData;
#endregion
#region ### DataPass
/// <summary>
/// Encargado de ser la conexión de los datos guardados con las escenas
/// Este podrá cargar el ultimo archivo o guardar un archivo con sus datos
/// <para>Dependencias: <seealso cref="Data"/>, <seealso cref="SavedData"/>, <seealso cref="DataStorage"/></para>
/// </summary>
public class DataSystem : MonoBehaviour
{
    #region ####### VARIABLES
    private static DataSystem _;
    [Header("Saved Data")]
    [SerializeField]
    private SavedData savedData = new SavedData();
    #endregion
    #region ###### EVENTS
    private void Awake()
    {
        this.Singleton(ref _,true);
        _._SaveLoadFile(!File.Exists(Path));
    }
    #endregion
    #region ####### METHODS
    /// <returns>The path of the saved data</returns>
    private static string Path => Application.persistentDataPath + Data.savedPath;
    /// <summary>
    /// Save or loads the files
    /// </summary>
    private void _SaveLoadFile(in bool wantSave = false)
    {
        BinaryFormatter _formatter = new BinaryFormatter();
        FileStream _stream = new FileStream(Path, wantSave ? FileMode.Create : FileMode.Open);
        DataStorage _dataStorage;

        //Dependiendo de si va a cargar o guardar hará algo o no
        if (wantSave)
        {
            savedData.debug_savedTimes++;
            _dataStorage = new DataStorage(Get);
            _formatter.Serialize(_stream, _dataStorage);
            _stream.Close();
        }
        else
        {
            _dataStorage = _formatter.Deserialize(_stream) as DataStorage;
            _stream.Close();
            Set(_dataStorage.savedData);

            // refresh editor view
            #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
            #endif
        }
    }

    /// <summary>
    ///  Update the Data of <see cref="DataPass"/> in <seealso cref="SavedData"/> with <paramref name="newSavedData"/>
    /// </summary>
    public static void Set(SavedData newSavedData) => _.savedData = newSavedData;
    /// <returns>The Loaded data in <see cref="DataPass"/></returns>
    public static SavedData Get => _.savedData;
    /// <summary>
    /// Save the data
    /// </summary>
    public static void Save() => _._SaveLoadFile(true);
    /// <summary>
    /// Loaf the data
    /// </summary>
    public static void Load() => _._SaveLoadFile(false);
    /// <summary>
    /// Delete the file
    /// </summary>
    public static void Delete() => File.Delete(Path);


    [ContextMenu("Guardar los datos")] public void _Save() => Save();
    [ContextMenu("Eliminas el archivo")] public void _Delete() => Delete();
    #endregion
}
#endregion
#region DataStorage y SavedData
namespace StorageData
{
    /// <summary>
    /// Encargado de hacer que, con un constructor se agreguen los nuevos valores
    /// <para>Dependencias => <seealso cref="SavedData"/></para>
    /// </summary>
    [System.Serializable]
    public class DataStorage
    {
        //aquí se vuelve a colocar los datos puestos debajo...
        public SavedData savedData = new SavedData();
        //Con esto podremos guardar los datos de datapass a DataStorage
        public DataStorage(SavedData savedData) => this.savedData = savedData;
    }
}
/// <summary>
/// Este es el modelo de datos que vamos a guardar y manejar
/// para los archivos que se crean... Estos datos internos pueden cambiar para los proyectos...
/// <para>
///     Aquí almacenamos los datos internos del juego
/// </para>
/// </summary>
[System.Serializable]
public struct SavedData
{

    [Header("Internal Saved Data")]
        [Tooltip("Primera vez en jugar?")] public bool isFirstTime;
        [Tooltip("El jugador nesecita que se le muestre el tutorial al iniciar?")] public bool tutorialDone;
        [Tooltip("Puntaje de Logros obtenidos")]public int[] achievementsPoints;


    [Header("Options in Saved Data")]
        [Tooltip("decibeles guardado de musica")] public float musicPercent;
        [Tooltip("Porcentaje guardado de sonido")] public float soundPercent;
        [Tooltip("Porcentaje guardado de Sensibilidad del mouse")] public float sensibilityPercent;//
        [Tooltip("Idioma actual")] public string currentLang;
        [Tooltip("Qué tan sensible es el agarre?")]public float dragSensibility; // 1-10, if 0 => 3f defualt
        [Tooltip("Keys guardados del usuario, por defecto tendrá los del arreglo")] public string[] controlKeys;

        [Tooltip("Configurations that need boolean movements")]public bool[] switch_configs;


    //Extra Debug ?
    [Header("Debug Area")]
        [Tooltip("Para debug, saber las veces que se ha guardado")]public int debug_savedTimes;
};
#endregion
