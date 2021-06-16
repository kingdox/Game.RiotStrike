#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// State
///</summary>
[CreateAssetMenu(menuName = "IA/State")]
public class State : ScriptableObject
{
    #region Variables
    [TextArea(3,20)]
    [SerializeField]private string description;
    [Space(20)]
    public StateAction[] actions;
    [Space(20)]
    public Transition[] transition;

    #endregion
    #region Methods

    /// <summary>
    /// code to update frecuently, as a update in <seealso cref="MonoBehaviour"/>
    /// </summary>
    public void UpdateState(IABody ia) {
        DoAction(ia);
        CheckTransition(ia);
    }


    /// <summary>
    /// Do all the actions in this state
    /// </summary>
    private void DoAction(IABody ia) {

        for (int i = 0; i < actions.Length; i++) {
            actions[i].Act(ia);
        }
    }

    /// <summary>
    /// Recorre todas las transiciones y verifica a que estado se debe cambiar
    /// </summary>
    private void CheckTransition(IABody ia) {
        for (int i = 0; i < transition.Length; i++) {

            // Check if the condition is 0 or 1 os aw
            bool desicionSucceded =
                transition[i]
                .decision
                .Decide(ia)
            ;

            if (desicionSucceded) {
                ia.TransitionToState(transition[i]
                    .trueState
                );
            } else {
                ia.TransitionToState(transition[i]
                    .falseState
                );
            }

        }
    }
    #endregion
}
