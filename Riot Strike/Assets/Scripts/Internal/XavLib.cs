#region Imports
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using S = System;
using System.IO;
using UnityEditor;

#endregion
//namespace XavLib
//{
/// <summary>
/// Herramientas para facilitar a Xavier contra el codigo
/// <para>Aquí se poseerán funciones unicamente "static"</para>
/// </summary>
namespace XavHelpTo
{

    /// <summary>
    /// Herramienta para las funciones principales ó más frecuentes
    /// </summary>
    public static class Supply {

        //public static T Assign<T>(this T t, T t2) => t = t2;

        /// <summary>
        /// Get the type of the gameobject selected
        /// </summary>
        public static void Component<C, T>(this C gameobj, out T t, bool canAdd = true)
            where C : Component
            where T : Component
        {
            t = gameobj.GetComponent<T>();
            if (canAdd && Know.Know.IsNull(t))
            {
                if (t is GameObject) $"Hay problemas al traer {nameof(GameObject)}, traer en cambio {nameof(Transform)}".Print("red");

                t = gameobj.gameObject.AddComponent<T>();
            }
        }
        /// <summary>
        /// Return the specified components childs of the first level in order from the transform target
        /// </summary>
        public static void Components<T>(this Transform target, out T[] t)
        {
            Set.Set.NewIn(target.childCount, out t);
            for (int x = 0; x < target.childCount; x++)
            {
                t[x] = target.GetChild(x).GetComponent<T>();
            }
        }

        /// <summary>
        /// Remove all the childs
        /// </summary>
        public static void ClearChilds(this Transform t)
        {
            for (int i = 0; i < t.childCount; i++) Object.Destroy(t.GetChild(i).gameObject);
        }

        /// <summary>
        /// Check the status of a static reference of <typeparamref name="T"/> as a Singleton
        /// </summary>
        public static void Singleton<T>(this T @this, ref T @_, bool isMultiScene = true)
            where T : MonoBehaviour
        {
            if (@_ == null)
            {
                if (isMultiScene) Object.DontDestroyOnLoad(@this);
                @_ = @this;
            }
            else if (!Equals(@_, @this))
            {
                Object.Destroy(@this.gameObject); // @this.gameobject
            }
        }

        /// <summary>
        ///  Saca el porcentaje de la cantidad y el maximo en caso de tener
        /// </summary>
        /// <returns>El porcentaje de count sobre el max</returns>
        public static float PercentOf(this float count, float max, bool normalize = false) => count / max * (normalize ? 1 : 100);
        public static int PercentOf(this int count, int max) => (int)((float)count).PercentOf(max, false);
        public static float PercentOf(this float[] c, bool normalize = false) => c[0].PercentOf(c[1], normalize);
        public static Vector2 PercentOf(this Vector2 count, Vector2 max, bool normalize = false) => count / max * (normalize ? 1 : 100);
        /// <summary>
        /// Basado en el porcentaje obtienes el valor mediante un maximo establecido
        /// </summary>
        public static int QtyOf(this int percent, float max, bool isNormalized = false) => Change.Change.ToInt((max / 100) * percent * (isNormalized ? 100 : 1));
        public static float QtyOf(this float percent, float max, bool isNormalized = false) => (max / 100) * percent * (isNormalized ? 100 : 1);
        public static Vector2 QtyOf(this Vector2 percent, Vector2 max) => (max / 100) * percent;



        /// <summary>
        /// Debugs a thing, but you still using the chain to know things...
        /// </summary>
        public static T Print<T>(this T s, string color = "null")
        {
            Debug.Log($"{Look.Look.Debugging(color)} {s}"); return s;
        }




        /// <summary>
        /// Access in <seealso cref="Application.streamingAssetsPath "/> and Loads the object
        /// path = $"{Application.streamingAssetsPath}/{path}.json"
        /// </summary>
        public static T LoadJson<T>(this string path) => path.LoadJson(out T t);
        public static T LoadJson<T>(this string path, out T container)
        {
            path = $"{Application.streamingAssetsPath}/{path}.json";
            if (File.Exists(path)) {
                string jsonString = File.ReadAllText(path);
                container = JsonUtility.FromJson<T>(jsonString);
            }
            else
            {
                $"ERR => {nameof(LoadJson)}: No se encuentra en el path, esta en streaming? => {path}".Print("red");
                container = default;
            }
            return container;
        }
       


    }

    namespace Get
        {
        #region Get
        /// <summary>
        /// Herramienta de obtención de valores
        /// </summary>
        public static class Get {


            /// <summary>
            /// Gets all the types of the objects
            /// </summary>
            public static S.Type[] Types(params object[] objs) {
            int length = objs.Length;
            S.Type[] types = new S.Type[length];
            for (int x = 0; x < length; x++)
            {
                types[x] = objs.GetType();
            }
            return types;
            }


            
            /// <summary>
            /// Returns the components of this gameobject without any order
            /// </summary>
            public static void ComponentsWithoutOrder<T>(this GameObject gameobj, out T[] t) => t = gameobj.GetComponents<T>();
            
            /// <summary>
            /// Returns the components of this object and this component object whether it have 
            /// </summary>
            public static void ComponentsInChilds<T>(this GameObject gameObject, out T[] t) where T : Component => t = gameObject.GetComponentsInChildren<T>();



            /// <summary>
            /// Assign the value if is finded, else null
            /// <para>Returns true if it was finded</para>
            /// </summary>
            public static bool ChildWithTag(this Transform target, string tag, ref Transform t)
            {
                bool finded = false;
                for (int i = 0; i < target.childCount; i++)
                {
                    Transform child = target.GetChild(i);
                    if (child.CompareTag(tag))
                    {
                        t = child;
                        finded = true;
                        break;
                    }
                }

                if (!finded) t = null;

                return finded;
            }


            /// <summary>
            /// Devuelve el ancho del porcentaje para conocerlo basado en la pantalla
            /// <para>Usa <seealso cref="KnowPercentOfMax(float, float)"/></para>
            /// </summary>
            public static float ScreenWidth(this float w) => w.QtyOf(Screen.width);
            /// <summary>
            /// Devuelve el ancho del porcentaje para conocerlo basado en la pantalla
            /// <para>Usa <seealso cref="KnowPercentOfMax(float, float)"/></para>
            /// </summary>
            public static float ScreenHeight(this float h) => h.QtyOf(Screen.height);

            /// <summary>
            /// Devuelve el ancho y alto del vector de porcentaje basado enla pantalla
            /// <para>Usa <seealso cref="GetWidthOf(float)"/> y <seealso cref="GetHeightOf(float)"/></para>
            /// </summary>
            public static Vector2 ScreenSize(this Vector2 s) => new Vector2(s.x.ScreenWidth(), s.y.ScreenHeight());

            /// <returns>The screen in pixels</returns>
            public static Vector2 RectScreen() => new Vector2(Screen.width, Screen.height);

            /// <summary>
            /// sacas el alto de una camara o la camara activa por defecto
            /// </summary>
            /// <para>Dependencia con <seealso cref="Camera"/> </para>
            /// <returns>el alto de <seealso cref="Camera"/> en unidades de Unity</returns>
            public static float ScreenHeightUnit(Camera camera = null) => camera ? camera.orthographicSize * 2f : Camera.main.orthographicSize * 2f;

            /// <summary>
            /// Sacas el ancho de la pantalla basado en el alto de la camara 
            /// <para>Dependencia con <seealso cref="Screen"/> </para>
            /// </summary>
            /// <returns>el Ancho de <seealso cref="Camera"/> en unidades Unity</returns>
            public static float ScreenWidthUnit(float camHeight) => camHeight * (Screen.width / Screen.height);
            
            /// <summary>
            /// Obtienes el valor del rango dado 
            /// </summary>
            public static float Range(float[] range) => Random.Range(range[0], range[1]);
            public static int Range(int[] range) => Random.Range(range[0], range[1]);
            public static float Range(Vector2 range) => Random.Range(range[0], range[1]);

            public static T Range<T>(params T[] range) => range[ZeroMax(range.Length)];

            public static T Any<T>(this T[] t) => t[t.ZeroMax()];

            /// <summary>
            /// Returns a random value between the limits possitive and negative
            /// </summary>
            public static float MinusMax(float max) => Random.Range(-max, max);
            public static Vector2 MinusMax(this Vector2 max) => new Vector2(MinusMax(max.x), MinusMax(max.y));

            public static Vector3 MinusMax(Vector3 pos, float range, int blocked = -1){

                for (int x = 0; x < 3; x++){
                    if (blocked != x){
                        pos[x] += MinusMax(range);
                    }
                }
                return pos;
            }

            /// <summary>
            /// Tomas el valor entre el 0 y el maximo
            /// </summary>
            /// <returns></returns>
            public static int ZeroMax(this int max) => Random.Range(0, max);
            public static float ZeroMax(this float max) => Random.Range(0, max);
            public static int ZeroMax<T>(this T[] arr) => arr.Length.ZeroMax();

            public static float Max(this float value, float max) => value > max ? max : value;
            public static int Max(this int value, int max) => value > max ? max : value;
            public static Vector3 Max(this Vector3 value, float max) => new Vector3(value.x.Max(max), value.y.Max(max), value.z.Max(max));

            /// <returns>A value with the minimal or else the value</returns>
            public static float Min(this float value, float min) => min > value ? min : value;
            public static int Min(this int value, int min) => min > value ? min : value;
            public static Vector2 Min(this Vector2 value, float min) => new Vector2(value.x.Min(min), value.y.Min(min));
            public static Vector3 Min(this Vector3 value, float min) => new Vector3(value.x.Min(min), value.y.Min(min), value.z.Min(min));

            public static int[] Min(this int[] value, int min)
            {
                for (int m = 0; m < value.Length; m++){
                    value[m] = value[m].Min(min);
                }
                return value;
            }

            /// <summary>
            /// Shows the limit of the value
            /// <para>set a range <see cref="Min"/> <see cref="Max"/>, the max can be the <paramref name="range"/> or the <paramref name="limit"/> in case to be declared</para>
            /// </summary>
            public static float Limit(this float value, float range, float limit = default) => value.Min(-range).Max( limit == default ? range : limit );

            /// <summary>
            /// Coloca el valor puesto en caso de que sea null
            /// </summary>
            public static T Default<T>(this T value, T defaultVal) => value = Know.Know.IsNull(value) ? defaultVal : value;

            /// <summary>
            /// Sumamos los valores de un arreglo
            /// </summary>
            public static float SummAll(params float[] values){
                    float c = 0;
                    foreach (float val in values){c += val;}
                    return c;
            }
            public static float SummAll(this Vector3 values) => SummAll(Change.Change.ToArray(values));


            /// <summary>
            /// Gets a random boolean
            /// </summary>
            public static bool RandomBool => Random.Range(0, 2) == 0;


            public static Color RandomColor(float min = 0, float max = 1 )
            {
                Color c = new Color();
                for (int x = 0; x < 3; x++)
                {
                    c[x] = 1f.ZeroMax().Min(min).Max(max);
                }
                c[3] = 1;
                return c;
            }
        }

        #endregion
        }
    namespace Set
    {
    #region Set
    /// <summary>
    /// Herramienta para modificación del valor,devolviendo los cambios hechos al valor
    /// </summary>
    public static class Set
    {

        public static T[] ToArray<T>(params T[] t)
        {
            return t;
        }

        /// <summary>
        /// Añade un string a un arreglo de strings.
        /// </summary>
        public static T[] Push<T>(T value, params T[] values)
        {
            T[] newArr = new T[values.Length + 1];
            for (int x = 0; x < newArr.Length; x++) newArr[x] = (x == newArr.Length - 1) ? value : values[x];
            return newArr;
        }
        /// <summary>
        /// Pushes the array with the news values in the last
        /// </summary>
        public static T[] PushIn<T>(this T t, params T[] ts) => Set.Push(t, ts);
        public static T[] PushIn<T>(this T t, ref T[] ts) => ts = Set.Push(t, ts);
        public static T[] RemoveIn<T>(this int start, ref T[] ts)
        {
            T[] newArr = new T[0];
            for (int i = 0; i < ts.Length; i++)
            {
                if (i != start)
                {
                    ts[i].PushIn(ref newArr);
                }
            }
            ts = newArr;
            return newArr;
        }
        /// <summary>
        /// Creas una nueva dimension de arreglo del tipo que desees
        /// </summary>
        public static T[] NewIn<T>(this int qty, out T[] t) => t = new T[qty];

            
        /// <summary>
        /// Asignas el valor a positivo en caso de ser negativo
        /// </summary>
        public static float Positive(this float f) => f < 0 ? f * -1 : f;
        public static Vector2 Positive(this Vector2 f) => new Vector2(f.x.Positive(), f.y.Positive());
        public static float[] Positive(params float[] f) {
            for (int x = 0; x < f.Length; x++) f[x].Positive();
            return f;
        }

        /// <summary>
        /// Obtenemos el valor dentro de los limites de la unidad de 0 y 1
        /// <para>tambien puede psoeer decimales</para>
        /// </summary>
        public static float InUnitBounds(float v) => Mathf.Clamp(v, 0, 1);

        //int start = 0, int end = -1, string startText = "") {for (int x = start; x < (end.Equals(-1) ? text.Length : end + 1); x++) startText += text[x];return startText; }
        /// <summary>
        /// Une los caracteres de un arreglo de caracteres , puediendo añadir indices de inicio y fin
        /// <para>se puede implementar un texto inicial</para>
        /// </summary> 
        public static string Join(this string text, int start = 0, int end = -1, string startText = "") { for (int x = start; x < (end.Equals(-1) ? text.Length : end + 1); x++) startText += text[x]; return startText; }
        public static string Join(string[] texts, string startText = "") { for (int x = 0; x < texts.Length; x++) startText += texts[x]; return startText; }


        /// <summary>
        /// Buscamos el parametro del <see cref="Color"/> que vas a cambiar
        /// <para>  el parametro debe estar entre los rangos de los parametros de color</para>
        /// <para>  [R == 0,G == 1,B == 2,A == 3] --> iniciando en 0.</para>
        /// <para>   Si i es == -1 entonces aplica a (RGB)</para>
        /// <para>Dependencia con <seealso cref="Color"/> </para>
        /// </summary>
        /// <returns>Devuelve el <see cref="Color"/> con los cambios</returns>
        public static Color ColorParam(this Color c, int i, float val = 1)
        {
            //Si esta fuera de los limites del arreglo
            if (!Know.Know.IsOnBounds(i, 4))
            {
                if (i == -1)
                {
                    for (int x = 0; x < 3; x++) c[x] = val;
                }
                else
                {
                    Debug.LogError($"<color=red>Indice errado ?</color>, favor usar un enum de parametros de color o usarlo bien :(");
                }
            }
            else c[i] = val;

            return c;
        }
        public static Image ColorParam(this Image img, ColorType i, float val = 1)
        {
                
            img.color = ColorParam(img.color, (int)i, val);
            return img;
        }


        /// <summary>
        /// Actualizamos el arreglo para que posea el mismo tamaño que el nuevo,
        /// estos cambios pueden eliminar o añadir huecos, los nuevos iniciarán en 0
        /// </summary>
        public static int[] Length(in int[] oldArr,  int newLength){

            //Si es igual no se hace nada
            if (oldArr.Length == newLength) return oldArr;


            //revisamos quién es mas grande
            bool condition = oldArr.Length > newLength;

            int[] newArr = new int[condition
                ? (oldArr.Length - newLength)
                : (newLength)
            ];


            //si hay menos datos llenamos el nuevo arreglo
            //Puede que perdamos algunos valores
            if (condition)
            {
                for (int z = 0; z < newArr.Length; z++) newArr[z] = oldArr[z];
            }
            else
            {
                for (int z = 0; z < newArr.Length; z++)
                {
                    newArr[z] = z < oldArr.Length - 1
                        ? oldArr[z]
                        : 0
                    ;
                }
            }

            return newArr;

        }
        public static float[] Length(in float[] oldArr,  int newLength)
        {

            //Si es igual no se hace nada
            if (oldArr.Length == newLength) return oldArr;


            //revisamos quién es mas grande
            bool condition = oldArr.Length > newLength;

            float[] newArr = new float[condition
                ? (oldArr.Length - newLength)
                : (newLength)
            ];


            //si hay menos datos llenamos el nuevo arreglo
            //Puede que perdamos algunos valores
            if (condition)
            {
                for (int z = 0; z < newArr.Length; z++) newArr[z] = oldArr[z];
            }
            else
            {
                for (int z = 0; z < newArr.Length; z++)
                {
                    newArr[z] = z < oldArr.Length - 1
                        ? oldArr[z]
                        : 0
                    ;
                }
            }

            return newArr;
        }
        /// <summary>
        /// conocemos el valor cambiando en un tiempo
        /// <para>
        /// si este valor se sobrepasa entre 0 y 1 los ajusta
        /// </para>
        /// </summary>
        public static float UnitInTime(this float value, float toMax, float timeScale = 1) => Set.InUnitBounds(Mathf.MoveTowards(value, toMax, Time.deltaTime * timeScale * Set.Positive(toMax - value)));
        //public static float UnitInTime(float value, float toMax, float timeScale = 1) => Set.InUnitBounds(Mathf.MoveTowards(value, toMax, Time.deltaTime * timeScale * Set.Positive(toMax - value)));

        /// <summary>
        /// Llenamos un arreglo con el valor escogido
        /// </summary>
        public static T[] FillWith<T>(this T tValue, ref T[] ts)
        {
            for (int x = 0; x < ts.Length; x++)
            {
                ts[x] = tValue;
            }
            return ts;
        }
        public static T[] FillWith<T>(this int qty ,T tValue){
            T[] newT = new T[qty];
            return FillWith(tValue, ref newT);
        }
            /// <summary>
            /// Cambiamos los valores que son iguales de su mismo arreglo, cambiandolos con alguno entre el maximo
            /// <para> Devuelve un arreglo con los valores pero distintos en caso de encontrar repetidos</para>
            /// </summary>
            public static int[] DifferentIndexInEquals(int[] arr, int max)
            {
                //recorremos el arreglo que vamos a buscar distinciones
                for (int i = 0; i < arr.Length; i++)
                {
                    //almacenamos el indice a buscar
                    int indexToFind = arr[i];
                    //limpiamos el arreglo por buscar
                    arr[i] = -1;

                    if (indexToFind < max)
                    {
                        //revisamos si el indice a buscar está entre los demás achievements
                        while (Know.Know.IsEqualOf(indexToFind, arr))
                        {
                            indexToFind = Know.Know.DifferentIndex(max, indexToFind);
                        }
                    }
                    else
                    {
                        Debug.LogError("El Indice a buscar es superior al indice maximo permitido !");
                    }

                    arr[i] = indexToFind;
                }

                return arr;
            }


            /// <summary>
            /// Rounds the values
            /// </summary>
            public static float[] Round(this float[] vals){
                int length = vals.Length;
                for (int i = 0; i < length; i++) vals[i] = Change.Change.ToInt(vals[i]);
                
                return vals;
            }


        }
        #endregion
    }
    namespace Change
    {
        #region Change
    /// <summary>
    /// Herramienta para la alteración de cosas
    /// </summary>
    public static class Change{


        /// <summary>
        /// Cambiamos a la escena indicada en numerico
        /// </summary>
        /// <param name="index"></param>
        public static void SceneTo(int index) => SceneManager.LoadScene(index);
        public static void SceneTo(string name) => SceneManager.LoadScene(name);
        public static void ToScene(this int i) => Change.SceneTo(i);
        public static void ToScene(this string i) => Change.SceneTo(i);
        public static void ToScene<T>(this T i)
        {
            if (Know.Know.IsEnum(i)) Change.SceneTo(i.ToInt());
        }
        /// <summary>
        /// Activa o desactiva el <seealso cref="GameObject"/> basado en una condición
        /// <para>Dependencia con <seealso cref="GameObject"/> </para>
        /// </summary>
        public static void ActiveObject(in GameObject obj, bool condition) => obj.SetActive(condition);
        /// <summary>
        /// Activa unicamente el objeto indicado del arreglo
        /// <para>Por defecto el indice es el primero del arreglo</para>
        /// <para>Dependencia con <seealso cref="ObjOnOff(GameObject, bool)"/> </para>
        /// </summary>
        public static void ActiveObjectsExcept(in GameObject[] arr, int index = 0) { for (int x = 0; x < arr.Length; x++) ActiveObject(arr[x], x == index); }
        public static void ActiveObjectsExcept(int index, params GameObject[] arr) { for (int x = 0; x < arr.Length; x++) ActiveObject(arr[x], x == index); }
        /// <summary>
        /// Active o Disable all the objects
        /// </summary>
        public static void ActiveObjects<T>(this T[] arr, bool val) where T : Component { for (int x = 0; x < arr.Length; x++) ActiveObject(arr[x].gameObject, val); }
        public static void ActiveObjects(this GameObject[] objects, bool condition) { foreach (GameObject o in objects) o.SetActive(condition); } 
        /// <summary>
        /// Dependiendo de la condición determinamos si iniciar o apagar la animación
        /// <para>Dependencia con <seealso cref="ParticleSystem"/> </para>
        /// </summary>
        public static void ActiveParticle(this ParticleSystem particle, bool condition)
        {
            if (condition && particle.isStopped) particle.Play();
            else if (!condition && particle.isPlaying) particle.Stop();
        }

        public static void ActiveAudioSource(this AudioSource audio,bool condition)
        {
            //si está sonando y se quiera desactivar
            if (audio.isPlaying && !condition) audio.Stop();
            //sino si NO esta sonando y se quiere encender...
            else if (!audio.isPlaying && condition) audio.Play();

        }

                
        /// <summary>
        /// Cambia a un arreglo
        /// </summary>
        public static T[] ToArray<T>(params T[] t) => t;
        public static float[] ToArray(this Vector2 v) => new float[] {v[0], v[1]};
        public static float[] ToArray(this Vector3 v) => new float[] {v[0], v[1], v[2] };
        public static float[] ToArray(this Color c) => ToArray(c.r, c.g, c.b, c.a);

        /// <summary>
        /// Transform The string in keycode
        /// </summary>
        public static KeyCode ToKeyCode(this string key, bool ignoreCase =true) => (KeyCode)S.Enum.Parse(typeof(KeyCode), key, ignoreCase);
        public static KeyCode ToKeyCode(this Event e , out KeyCode k) => k = (e.isMouse ? $"mouse{Event.current.button}".ToKeyCode() : e.keyCode);

        /// <summary>
        ///  Set to color an array
        /// </summary>
        public static Color ToColor(this int[] t)
        {
            Color c = Color.white;
            for (int i = 0; i < t.Length; i++) c[i] = t[i];
            return c;
        }
        public static Color ToColor(this float[] t)
        {
            Color c = Color.white;
            for (int i = 0; i < t.Length; i++) c[i] = t[i];
            return c;
        }
        /// <summary>
        ///  Set a Vector with the float values
        /// </summary>
        public static Vector3 ToVector(this float[] vals)
        {
                Vector3 vector = new Vector3();
                for (int i = 0; i < vals.Length; i++) vector[i] = vals[i];
                return vector;
        }

        /// <summary>
        /// Returns a <see cref="Vector3"/> with the axis
        /// </summary>
        public static Vector3 ToAxis(this float value, int axis=0){
                Vector3 newAxist = new Vector3();
                newAxist[axis] = 1 * value;
                return newAxist;
            }
            /// <summary>
            /// Adjust the axis of a <see cref="Vector3"/>
            /// </summary>
            public static Vector3 Axis(this Vector3 vector, int axis=0, float newValue=0){
                vector[axis] = newValue;
            return vector;
            }
            /// <summary>
            /// Sets a bool
            /// </summary>
            public static bool ToBool<T>(this T t) => t.Equals(true);
            public static bool ToBool(this string t) => S.Boolean.Parse(t);
            public static bool ToBool(this int t) => t.Equals(1); // otherwise 0 == false
            public static float[] ToFloat(this Color c) => new float[4] { c.r, c.g, c.b, c.a };

            /// <summary>
            /// change the value to a binary result
            /// </summary>
            public static int ToInt(this int val) => val;
            public static int ToInt(this bool condition) => condition ? 1 : 0;
            public static int ToInt(this float val) => (int)val;
            public static int ToInt(this S.Enum @enum) => S.Convert.ToInt32(@enum);
            public static int[] ToInt<T>(this T[] t)
            {
                int[] ints = new int[t.Length]; //Set.Set.FillWith(-1,t.Length)

                for (int x = 0; x < t.Length; x++)
                {
                    ints[x] = t[x].ToInt();
                }

                return ints;
            }
            public static int ToInt<T>(this T t, bool isInt = false)
            {
                int result = -1;

                if (!isInt)
                {

                    if (Know.Know.IsEnum(t))
                    {
                        return (S.Enum.Parse(typeof(T), t.ToString()) as S.Enum).ToInt();
                    }
                    else "NO ES UN ENUM:::: dejo en -1".Print();
                }
                else
                {
                    result = S.Convert.ToInt32(t);
                }

                return result;
            }
            
        public static int[] ToInt(this float[] t)
        {
            int length = t.Length;
            int[] newInt = new int[length];
            for (int x = 0; x < length; x++) newInt[x] = (int)t[x];
            return newInt;
        }

    }
    #endregion
}
    namespace Know
    {
    #region Know
    /// <summary>
    /// Herramienta que devuelve valores booleanas o de indexación (hay excepciones..)
    /// </summary>
    public static class Know
    {

        /// <summary>
        /// Preguntamos si es nulo el valor indicado
        /// </summary>
        public static bool IsNull<T>(this T t ) => t == null || t.ToString() == "null";
        //,bool equal = true TODO a futuro podrás decir si el nulo es cierto o no como segundo argumento, por defecto true
        public static bool IsNull<T>(params T[] t)
            where T: struct
        {
            foreach (T item in t) 
            {
                if (IsNull(item)) return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the value is an Enumerator
        /// </summary>
        public static bool IsEnum<T>(this T t) => typeof(T).IsEnum;
        /// <summary>
        /// Revisa si el objeto está seleccionado
        /// </summary>
        public static bool Focus(GameObject obj) => obj.Equals(EventSystem.current.currentSelectedGameObject);
            
        /// <summary>
        /// Detecta si el indice está dentro del arreglo
        /// </summary>
        public static bool IsOnBounds(int i, int length) => i == Mathf.Clamp(i, 0, length - 1);
        public static bool IsOnBounds<T>(this int i, T[] length) => IsOnBounds(i, length.Length);
        public static bool IsOnBounds(int i, int length, bool direction) => i == Mathf.Clamp(i + (direction ? 1 : -1) , 0, length - 1);
            
        /// <summary>
        /// Check if one of the values from the array are equal
        /// </summary>
        public static bool IsEqualOf<T>(this T value, params T[] vals) { foreach (T val in vals) if (value.Equals(val)) return true; return false; }
        /// <summary>
        /// Check if exist a value same as the another array
        /// </summary>
        public static bool IsEqualOf<T>(this T[] values, params T[] vals)
        {
            bool finded = false;
            foreach (T v in values){
                if (!finded)
                {
                    finded = v.IsEqualOf(vals);
                }
            }

            return finded;
        }

        /// <summary>
        /// Counts how many slots of the same value exist in a array
        /// </summary>
        public static int CountOf<T>(this T[] collection, T valueToFind){
            int qty = 0;
            foreach (T c in collection){
                if (c.Equals(valueToFind)) qty++;
            }
            return qty;
        }

        /// <summary>
        /// Check if all the values exist in vals without checking the order
        /// Return true if vals size is 0 or is not assigned
        /// return false if <seealso cref="values"/> is 0 length
        /// </summary>
        public static bool Contains<T>(this T[] values, params T[] vals){
            bool keepInto = !values.Length.Equals(0);
            foreach (T o in values){
                if (keepInto) keepInto = o.IsEqualOf(vals);
                else break;
            }
            return keepInto;
        }

        /// <summary>
        /// Detecta el primer caracter de los buscados en el arreglo
        /// <para>Podemos tener un indice inicial</para>
        /// <para>Devuelve -1 si no encuentra</para>
        /// <para>Dependencia con <see cref="IsEqualOf(char, char[])"/> para hacer más de una busqueda</para>
        /// </summary>
        public static int IndexOf<T>( T[] ts, int startIndex, params T[] finder){for (int x = startIndex; x < ts.Length; x++) if (ts[x].IsEqualOf(finder)) return x; return -1;}
        public static int IndexOf(char[] chars, int startIndex, params char[] finder) { for (int x = startIndex; x < chars.Length; x++) if (chars[x].IsEqualOf(finder)) return x; return -1; }
        public static int IndexOf(string text, int startIndex, params char[] finder) { for (int x = startIndex; x < text.Length; x++) if (text[x].IsEqualOf(finder)) return x; return -1; }
        public static int IndexOf<T>(T t, int startIndex, params T[] finder) { for (int x = startIndex; x < finder.Length; x++) if (t.IsEqualOf(finder)) return x; return -1; }
        /// <summary>
        /// Busca en un arreglo y si encuentra, muestra donde
        /// <para> caso contrario devuelve -1 </para>
        /// </summary>
        public static int FocusIndex(params GameObject[] objs)
            {
                for (int x = 0; x < objs.Length; x++) if (objs.Equals(EventSystem.current.currentSelectedGameObject)) return x;
                return -1;
            }
            /// <summary>
            /// Busca cual es el valor del arreglo que supera al indicado
            /// <para>Retorna -1 si no encuentra alguno mayor que el mostrado</para>
            /// </summary>
            public static int FirstMajor(float val, float[] arr)
            {
                for (int x = 0; x < arr.Length; x++) if (val < arr[x]) return x;
                return -1;
            }
            /// <summary>
            /// Conoces el siguiente indice basado en la longitud del arreglo
            /// <para>Se le puede definir un inicio en caso de haber</para>
            /// </summary>
            public static int NextIndex(this bool goNext, int indexLength, int index = 0) => goNext ? (index == indexLength - 1 ? 0 : index + 1) : (index == 0 ? indexLength - 1 : index - 1);
            /// <summary>
            /// Retorna un valor distinto al ultimo suponiendo que la dimension es mayor a 1
            /// </summary>
            public static int DifferentIndex(this int max, int lastInt = -1)
            {
                int _newInt = lastInt;

                while (lastInt == _newInt && max > 1)
                {
                    _newInt = Get.Get.ZeroMax(max);
                }

                return _newInt;
            }
            /// <summary>
            /// Devolvemos en un arreglo los 4 puntos para tomar una etiqueta. 
            /// <para>Estas etiqutas SOLO manejan 1 nivel de profundidad</para>
            /// </summary>
            public static int[] IndexsOfTag(string text, int index_Start = -1){
                if (index_Start.Equals(-1)) index_Start = Know.IndexOf(text,0,'<');
                //creamos el espacio
                int[] tagIndex = { index_Start, -1,-1,-1 };
                int tagNameLength;

                //'>', si encuentra un '=' se tendrá que hacer un reajuste luego...
                tagIndex[1] = Know.IndexOf(text, index_Start, '=', '>');

                tagNameLength = Set.Set.Join(text, index_Start, tagIndex[1] - 1).Length;

                if (text[tagIndex[1]].Equals('='))
                {
                    // '>'
                    tagIndex[1] = Know.IndexOf(text, tagIndex[1], '>');

                }
                // '<'
                tagIndex[2] = Know.IndexOf(text, tagIndex[1], '<');

                // '>'
                tagIndex[3] = tagIndex[2] + tagNameLength + 1;

                return tagIndex;
            }


            /// <summary>
            /// Based on a cooldown who difference the next step and a time to store the qty
            /// <para>use the Timer</para>
            /// </summary>
            public static bool Timer(this float cooldown,ref float time)
            {
                bool condition = Time.time >= time;
                if (condition) time = Time.time + cooldown;
                return condition;
            }

            /// <summary>
            /// Based on a cooldown, updates the timer and returns true if pass the cooldown
            /// </summary>
            public static bool TimerIn(this float cooldown, ref float count)
            {
                count += Time.deltaTime;
                bool result = count > cooldown;
                if (result) count = 0;
                return result;
            }
            /// <summary>
            /// Permite activar el flag "can"___ para poder volver a usarlo, este se mide por tiempo
            /// </summary>
            public static bool TimerFlag(this float timer, ref bool flag, ref float count)
            {
                if (!flag &&  timer.TimerIn(ref count))
                {
                    flag = true;
                }
                return flag;
            }

        }
        #endregion
    }
    namespace Look
    {
        #region Debug
        /// <summary>
        /// Herramienta para facilitar a xavier su progreso en debug.
        ///  Tambien está para visualizar cosas mejor.
        ///  posee cosas esteticas....
        /// </summary>
        public static class Look
        {
            /// <summary>
            /// Gets the value with a concrete color 
            /// </summary>
            public static string InColor<T>(this T value, string color="red") => $"<color={color}>{value}</color>";

            /// <summary>
            /// Pintamos un mensaje con color
            /// </summary>
            public static void PrintColor<T>(T value,string color="green") => Debug.Log(InColor(value, color));

            /// <summary>
            /// Indicador decorativo de que andas debugeando algo, solo apoyo visual
            /// </summary>
            public static string Debugging(string color="null") =>  InColor("DEBUG: ", Get.Get.Default(color,RandomColor()));
               
            /// <summary>
            /// Selector aleatorio de color, pretenden para debug, no para manejos de otras cosas..
            /// </summary>
            public static string RandomColor() => Get.Get.Range("green", "red","blue", "brown", "purple","teal", "lime", "lightblue", "magenta", "white","yellow");
            /// <summary>
            /// Leemos en consola un arreglo de strings
            /// </summary>
            public static void Array<T>(params T[] strings) { foreach (T s in strings) Debug.Log($"{Debugging()} {s}");}

              
        }
        #endregion
    }
    namespace EditWindow
    {
        using static Change.Change;
        public static class EditWindow
        {

            /// <summary>
            /// Set an item of the selecte guiStyle
            /// perfect to generate the styling of lot of "styles", more ease to read
            /// </summary>
            /// <typeparam name="T">type of value</typeparam>
            /// <param name="keyName">key of the field, no mathers the level</param>
            /// <param name="value">to assign</param>
            public static GUIStyle In<T>(this GUIStyle style, string keyName, params T[] value)
            {
                switch (keyName)
                {
                    case "name":
                        style.name = value[0].ToString();
                        break;
                    case "fontSize":
                        style.fontSize = value[0].ToInt();
                        break;
                    case "wordWrap":
                        style.wordWrap = value[0].ToBool();
                        break;
                    case "textColor":
                        "No disponible".Print("red");
                        //if (typeof(T) == typeof(int))
                        //{
                        //try { style.normal.textColor = new Color(value[0], value[0], value[0] };
                        //catch { $"{nameof(XavHelpTo)}_{nameof(EditorWindow)}_{nameof(In)} => Error Asignando el color, ejemplo correcto => thatColor.ToArray()".Print("red"); }
                        break;
                    case "alignment":
                        style.alignment = (TextAnchor)value[0].ToInt();
                        break;
                    case "stretchWidth":
                        style.stretchWidth = value[0].ToBool();
                        break;
                    case "stretchHeight":
                        style.stretchHeight = value[0].ToBool();
                        break;
                    case "fixedHeight":
                        style.fixedHeight = value[0].ToInt(true);
                        break;
                    case "fixedWidth":
                        style.fixedWidth = value[0].ToInt(true);
                        break;
                    case "margin":
                        int[] ints = value.ToInt();
                        style.margin = new RectOffset(ints[0], ints[1], ints[2], ints[3]);
                        break;
                    case "background":
                        //style.normal.background TExture2D
                        "no disponible aun".Print("red");
                        break;

                    default:
                        $"Estilo desconocido => {keyName}, en {value}".Print("red");
                        break;
                }

                return style;
            }
            public static GUIStyle In(this GUIStyle style, Texture2D value)
            {
                style.normal.background = value;
                return style;
            }


            /// <summary>
            /// Set the title, icon of the window
            /// </summary>
            public static void TabWindow<T>(this T @this, string title, string iconPath = default)
                where T : EditorWindow
            {
                Texture2D myIcon = EditorGUIUtility.Load(iconPath) as Texture2D;
                @this.titleContent = new GUIContent(title, myIcon);

            }


            /// <summary> 
            /// Create a Standar Begin ToggleGroup
            /// </summary>
            public static bool ToggleGroup(this string msg, ref bool boolean) => boolean = EditorGUILayout.BeginToggleGroup(msg, boolean);

            public static Color ColorField(ref Color cRef) => cRef = EditorGUILayout.ColorField(cRef);
            public static Color ColorField(ref float[] cRef)
            {
                Color c = EditorGUILayout.ColorField(cRef.ToColor());
                cRef = c.ToFloat();
                return c;
            }
          

            public static string TextField(ref string txtRef) => txtRef = EditorGUILayout.TextField(txtRef);
            public static string TextArea(ref string txtRef) => txtRef = EditorGUILayout.TextArea(txtRef);
            public static bool Toggle(ref bool txtRef) => txtRef = EditorGUILayout.Toggle(txtRef);

            /// <summary>
            /// Create a Standar Button
            /// returns true if exist changes
            /// </summary>
            public static bool Button(this string msg, ref bool boolean)
            {
                bool ischanged = false;
                if (GUILayout.Button(msg + $" ({boolean})"))
                {
                    boolean = !boolean;
                    //EditorApplication.Beep();
                    ischanged = true;

                }
                return ischanged;
            }

            public static bool ButtonDisabled(this string msg, bool enabled)
            {
                GUI.enabled = enabled;
                bool ischanged = GUILayout.Button(msg);
                GUI.enabled = true;
                return ischanged;
            }

        }

    }
}

/// <summary>
/// Identificador de los colores
/// es solo un facilitador...
/// </summary>
public enum ColorType
{
    r,
    g,
    b,
    a,
    RGB = -1
}

#region Committed
//TEST
//public readonly struct Operate
//{
//    public static Operate operator ~(Operate i) => new Operate();
//}
/*
 //public static T operator +<T>(T a) => a;
        //public static float operator ^(float a) => aa;
        public static bool operator (Craft a, ItemContent i) => a.Equals(i);
 */
#endregion