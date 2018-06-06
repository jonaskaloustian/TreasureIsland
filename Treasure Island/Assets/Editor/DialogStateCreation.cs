using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class DialogStateCreation : EditorWindow
{

    private AnimatorState originState;
    public AnimatorController targetController;
    private AnimatorStateMachine targetControllerSM;
    public enum StateType
    {
        Unique,
        ItemCondition,
        ValueCondition,
        OneAnswer,
        TwoAnswers,
        ThreeAnswers,
        FourAnswers,
    }
    public StateType stateType;

    [MenuItem("Window/Dialog State Creation")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DialogStateCreation));
    }

    void OnGUI()
    {
        targetController = EditorGUILayout.ObjectField("Target Controller", targetController, typeof(AnimatorController), true) as AnimatorController;
        stateType = (StateType)EditorGUILayout.EnumPopup("State Type", stateType);
        if (GUILayout.Button("Generate State Children and Transitions"))
        {
            CreateChildrenStates();
        }
        if (GUILayout.Button("Correct All Transitions"))
        {
            CorrectAllTransitions(targetController.layers[0].stateMachine);
        }
    }
    /* Cette fonction crée les états enfants et les transitions correspondantes, à partir de l'AnimatorState
	 * sélectionné en fonction du choix fait dans la fenête sur l'enum State Type.
	 * Les transitions sont automatiquement crées de la bonne forme et avec les bonnes conditions. */
    void CreateChildrenStates()
    {
        targetControllerSM = targetController.layers[0].stateMachine;
        for (int i = 0; i < targetControllerSM.states.Length; i++)
        {
            if (targetControllerSM.states[i].state == Selection.activeObject)
            {
                originState = targetControllerSM.states[i].state;
                switch (stateType)
                {
                    case StateType.ItemCondition:
                        {
                            string[] triggers1 = new string[2];
                            triggers1[0] = DialogParameters.nextString;
                            triggers1[1] = DialogParameters.itemConditionMetString;
                            string[] triggers2 = new string[2];
                            triggers2[0] = DialogParameters.nextString;
                            triggers2[1] = DialogParameters.itemConditionNotMetString;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers1);
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers2);
                            CheckBehaviourListAndCreateBehaviourOnState(originState, typeof(TestItemReactionStep));
                            break;
                        }
                    case StateType.ValueCondition:
                        {
                            string[] triggers1 = new string[2];
                            triggers1[0] = DialogParameters.nextString;
                            triggers1[1] = DialogParameters.valueConditionMetString;
                            string[] triggers2 = new string[2];
                            triggers2[0] = DialogParameters.nextString;
                            triggers2[1] = DialogParameters.valueConditionNotMetString;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers1);
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers2);
                            CheckBehaviourListAndCreateBehaviourOnState(originState, typeof(TestValueReactionStep));
                            break;
                        }
                    case StateType.Unique:
                        {
                            string[] triggers = new string[1];
                            triggers[0] = DialogParameters.nextString;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers);
                            break;
                        }
                    case StateType.OneAnswer:
                        {
                            string[] triggers = new string[1];
                            triggers[0] = DialogParameters.answer0String;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers);
                            CheckBehaviourListAndCreateBehaviourOnState(originState, typeof(AnswerReactionStep));
                            break;
                        }
                    case StateType.TwoAnswers:
                        {
                            string[] triggers1 = new string[1];
                            triggers1[0] = DialogParameters.answer0String;
                            string[] triggers2 = new string[1];
                            triggers2[0] = DialogParameters.answer1String;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers1);
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers2);
                            CheckBehaviourListAndCreateBehaviourOnState(originState, typeof(AnswerReactionStep));
                            break;
                        }
                    case StateType.ThreeAnswers:
                        {
                            string[] triggers1 = new string[1];
                            triggers1[0] = DialogParameters.answer0String;
                            string[] triggers2 = new string[1];
                            triggers2[0] = DialogParameters.answer1String;
                            string[] triggers3 = new string[1];
                            triggers3[0] = DialogParameters.answer2String;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers1);
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers2);
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers3);
                            CheckBehaviourListAndCreateBehaviourOnState(originState, typeof(AnswerReactionStep));
                            break;
                        }
                    case StateType.FourAnswers:
                        {
                            string[] triggers1 = new string[1];
                            triggers1[0] = DialogParameters.answer0String;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers1);
                            string[] triggers2 = new string[1];
                            triggers2[0] = DialogParameters.answer1String;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers2);
                            string[] triggers3 = new string[1];
                            triggers3[0] = DialogParameters.answer2String;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers3);
                            string[] triggers4 = new string[1];
                            triggers4[0] = DialogParameters.answer3String;
                            CreateStatesAndTriggerTransitions(targetControllerSM, originState, triggers4);
                            CheckBehaviourListAndCreateBehaviourOnState(originState, typeof(AnswerReactionStep));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                return;
            }
        }
        Debug.Log("Error. Please select a state on Target Controller before creating children states.");
    }

    //Cette fonction crée un état avec les transitions spécifiées (durée, exit time, et conditions) par le choix dans CreateChildrenStateAndTransitions.
    void CreateStatesAndTriggerTransitions(AnimatorStateMachine stateMachine, AnimatorState origin, string[] triggers)
    {
        string newStateName = stateMachine.MakeUniqueStateName("newState");
        AnimatorState newState = stateMachine.AddState(newStateName) as AnimatorState;
        AnimatorStateTransition newStateTransition = originState.AddTransition(newState) as AnimatorStateTransition;
        newStateTransition.hasExitTime = false;
        newStateTransition.duration = 0f;
        foreach (string trigger in triggers)
        {
            newStateTransition.AddCondition(AnimatorConditionMode.If, 0, trigger);
        }
    }

    //Cette fonction vérifie que la Behaviour spécifique à créer lorsqu'on utilise CreateChildrenStateAndTransitions ne soit pas déjà présente à l'origine. Si elle n'est pas présente, elle la crée.
    void CheckBehaviourListAndCreateBehaviourOnState(AnimatorState state, System.Type type)
    {
        for (int i = 0; i < state.behaviours.Length; i++)
        {
            if (state.behaviours[i].GetType() == type)
            {
                Debug.Log("Origin State did not create a new Behaviour. He already had one with needed Type.");
                return;
            }
        }
        state.AddStateMachineBehaviour(type);
    }


    //Cette fonction fait en sorte que toutes les transitions de l'animator, y compris celles crées à la main (sur la State Machine de base, mais pourrait être étendu sans difficulté)
    //soient automatiquement fixées sans Exit Time et avec une durée de transition égale à 0, ce qui est optimal pour une StateMachine.
    void CorrectAllTransitions(AnimatorStateMachine sM)
    {
        AnimatorState[] stateArray = new AnimatorState[sM.states.Length];
        List<AnimatorStateTransition> transitionList = new List<AnimatorStateTransition>();
        for (int i = 0; i < sM.states.Length; i++)
        {
            stateArray[i] = sM.states[i].state;
        }
        for (int i = 0; i < stateArray.Length; i++)
        {
            transitionList.AddRange(stateArray[i].transitions);
        }
        for (int i = 0; i < transitionList.Count; i++)
        {
            transitionList[i].duration = 0f;
            transitionList[i].hasExitTime = false;
        }
    }

}
