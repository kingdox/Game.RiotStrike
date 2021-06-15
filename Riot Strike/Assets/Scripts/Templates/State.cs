#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
///<summary>
/// State
///</summary>
[CreateAssetMenu(menuName = "Template/State")]
public class State : ScriptableObject
{
    #region Variables
    public StateAction[] actions;
    public StateController controller;
    public Transition[] transition;

    #endregion
    #region Events
    #endregion
    #region Methods

    /// <summary>
    /// code to update frecuently, as a update in <seealso cref="MonoBehaviour"/>
    /// </summary>
    public void UpdateState(StateController controller) {
        DoAction(controller);


        CheckTransition(controller);
    }


    /// <summary>
    /// Do all the actions in this state
    /// </summary>
    private void DoAction(StateController controller) {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].Act(controller);
        }
    }


    /// <summary>
    /// Recorre todas las transiciones y verifica a que estado se debe cambiar
    /// </summary>
    private void CheckTransition(StateController controller) {
        for (int i = 0; i < transition.Length; i++) {
            bool desicionSucceded = transition[i].decision.Decide(controller);

            if (desicionSucceded) {
                controller.TransitionToState(transition[i].trueState);
            } else {
                controller.TransitionToState(transition[i].falseState);
            }

        }
    }
    #endregion
}
