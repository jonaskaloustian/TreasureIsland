using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class DialogControllerCreation : EditorWindow
{

    public string controllerName;

    [MenuItem("Window/Dialog Controller Creation")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DialogControllerCreation));
    }

    void OnGUI()
    {
        controllerName = EditorGUILayout.TextField("Controller Name", controllerName);
        if (GUILayout.Button("Create Controller"))
        {
            AnimatorController assetTest = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/DialogControllers/" + controllerName + ".controller");
            if (assetTest == null)
            {
                CreateController();
            }
            else
            {
                Debug.Log("There is already an Animator Controller with this name in the DialogControllers folder.");
            }
        }
    }

    //Crée un Dialog Controller (Animator Controller) qui a les caractéristiques suivantes :
    //Il possède tous les paramètres Triggers et Booléens nécessaires pour être utilisé.
    //Un état de départ et des transitions vers 2 états supplémentaires Visited et NotVisited.
    //Une transition de retour depuis AnyState permet de revenir à l'état initial dès que le booléen active redevient faux.
    //Les transitions sont configurées correctement (pas de durée de transition).

    void CreateController()
    {

        var controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/DialogControllers/" + controllerName + ".controller");
        var rootStateMachine = controller.layers[0].stateMachine;

        controller.AddParameter(DialogParameters.nextString, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.answer0String, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.answer1String, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.answer2String, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.answer3String, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.itemConditionMetString, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.itemConditionNotMetString, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.valueConditionMetString, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.valueConditionNotMetString, AnimatorControllerParameterType.Trigger);
        controller.AddParameter(DialogParameters.visitedString, AnimatorControllerParameterType.Bool);
        controller.AddParameter(DialogParameters.activeString, AnimatorControllerParameterType.Bool);

        AnimatorState startState = rootStateMachine.AddState("Start") as AnimatorState;
        rootStateMachine.AddEntryTransition(startState);

        AnimatorState notVisitedState = rootStateMachine.AddState("Not Visited") as AnimatorState;
        AnimatorStateTransition notVisitedTransition =  startState.AddTransition(notVisitedState) as AnimatorStateTransition ;
        notVisitedTransition.duration = 0f;
        notVisitedTransition.AddCondition(AnimatorConditionMode.IfNot, 0, DialogParameters.visitedString);
        notVisitedTransition.AddCondition(AnimatorConditionMode.If, 0, DialogParameters.activeString);

        AnimatorState visitedState = rootStateMachine.AddState("Visited") as AnimatorState;
        AnimatorStateTransition visitedTransition = startState.AddTransition(visitedState) as AnimatorStateTransition;
        visitedTransition.duration = 0f;
        visitedTransition.AddCondition(AnimatorConditionMode.If, 0, DialogParameters.visitedString);
        visitedTransition.AddCondition(AnimatorConditionMode.If, 0, DialogParameters.activeString);

        AnimatorStateTransition inactiveTransition = rootStateMachine.AddAnyStateTransition(startState) as AnimatorStateTransition;
        inactiveTransition.duration = 0f;
        inactiveTransition.AddCondition(AnimatorConditionMode.IfNot, 0, DialogParameters.activeString);

    }
}