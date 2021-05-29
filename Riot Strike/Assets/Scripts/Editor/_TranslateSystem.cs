#region Access
using UnityEngine;
using UnityEditor;
using XavHelpTo.EditWindow;
using XavHelpTo.Change;
#endregion
#region Editor
[CustomEditor(typeof(TranslateSystem))]
public class TranslateSystemEditor : Editor
{

    GUIStyle style;
    public override void OnInspectorGUI(){

        TranslateSystem tr = target as TranslateSystem;
        //GUILayout.Button(,tr.debug_mode);
        if (tr.debug_mode)  {
            style = new GUIStyle(EditorStyles.textField)
                .In("fontSize",16).In("name","").In("textColor", Color.green.ToArray());

            

            if (tr.debug_folder.Length.Equals(0)) tr.debug_folder = TranslateSystem.DEFAULT_LANG;
            //tr.debug_folder = AssignText(ref tr.debug_folder);

            style.normal.textColor = Color.magenta;
            if (tr.debug_key.Length.Equals(0)) tr.debug_key = "lang";
            //tr.debug_key = AssignText(ref tr.debug_key);


            //1. Cargamos el diccionario
            tr._InitLang(tr.debug_folder, "");

            string translated = (tr.dic_Lang is null)
                ? "Error => Archivo Incorrecto, coloca uno de los que existen en Resources/Lang/..."
                : TranslateSystem.Translate(tr.debug_key);
            ;
            translated = "Resultado => " + translated;

            style = style.In("fontSize", 12).In("textColor", Color.red.ToArray())
                .In("wordWrap", true);
            
            //translated = AssignText(ref translated);
        }

        //tr.debug_mode = GUILayout.Button($"Debug Mode: {tr.debug_mode}") ? !tr.debug_mode : tr.debug_mode;
        DrawDefaultInspector();

    }


}
#endregion
