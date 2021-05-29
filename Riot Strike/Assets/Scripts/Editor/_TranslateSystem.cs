#region Access
using UnityEngine;
using UnityEditor;
using XavHelpTo.EditWindow;
using XavHelpTo;
using XavHelpTo.Change;
using Dat = Environment.Data;
#endregion
#region Editor
[CustomEditor(typeof(TranslateSystem))]
public class TranslateSystemEditor : Editor
{

    GUIStyle style;
    public override void OnInspectorGUI(){

        TranslateSystem tr = target as TranslateSystem;
        style = new GUIStyle();
        $"Enable Debug Mode ".Button(ref tr.debug_mode);


        if (tr.debug_mode)  {

            GUILayout.Space(20);
            //Listamos los lenguajes admitidos
            foreach (string L in Dat.LANGUAGES)
            {
                if (GUILayout.Button($"Set {L}")) tr.debug_folder = L;
            }

            //Resetea el lenguaje al asignado por defecto
            if (tr.debug_folder.Length.Equals(0)) tr.debug_folder = TranslateSystem.DEFAULT_LANG;
            GUILayout.Label(tr.debug_folder);
            //EditWindow.TextField(ref tr.debug_folder);

            //resetea el key al lang por defecto
            if (tr.debug_key.Length.Equals(0)) tr.debug_key = "lang";
            EditWindow.TextField(ref tr.debug_key);


            // 0. Colocamos la instancia
            tr._EditorSetInstance();

            //1. Cargamos el diccionario
            tr._InitLang(tr.debug_folder, "");

            //2. Revisamos si Se cargó el diccionario 
            string translated = (tr.dic_Lang is null)
                ? $"Error => Archivo Incorrecto, coloca uno de los que existen en Resources/{TranslateSystem.PATH_LANG}/..."
                : TranslateSystem.Translate(in tr.debug_key);
            ;

            translated = "Resultado => " + translated;

            //style = style
            //    .In("fontSize", 12)
            //    .In("wordWrap", true);

            EditorGUILayout.LabelField(translated);
        }

        DrawDefaultInspector();

    }


}
#endregion
