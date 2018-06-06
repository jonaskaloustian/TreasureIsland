using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

    [HideInInspector] public NodeGrid grid;
    public Animator animator;

    private void Awake()
    {
        grid = transform.parent.GetComponent<NodeGrid>();
    }

    public void LaunchNode()
    {
        grid.movingIcon.SetParent(GetComponent<RectTransform>(), false);
        animator.SetBool(DialogParameters.activeString, true);
    }

    public void MoveToNextStep()
    {
        animator.SetTrigger(DialogParameters.nextString);
    }
}
