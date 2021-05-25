#region Access
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XavHelpTo;
#endregion

/// <summary>
/// Helper to actions in this game and their own structure
/// </summary>
public static class Utils 
{
    #region Utils Methods
        /// <summary>
        /// Allows to translate and set the information
        /// can create a <seealso cref="TranslateController"/> in the obj
        /// <seealso cref="TranslateSystem"/>
        /// </summary>
        public static void Translate<T>(this RefreshController refresh, T r, string key)
        {
            refresh
                .GetText(r)
                .Component(out TranslateController transC, true);
            transC.key = key;
            transC.Translate();
        }
    #endregion
}
