using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NodeGrid : MonoBehaviour {

    [HideInInspector]public List<Node> nodes = new List<Node>();

    [Range(0,24)]
    public int startingNodeValue;

    [HideInInspector]public Node currentNode;
    public GlobalValues globalValues;
    public BackgroundImage backgroundImage;
    public ActionPanel actionPanel;
    public Inventory inventory;
    public RectTransform movingIcon;

    private GameControl gameControl;

    private bool ending = false;

    private void Start()
    {
        InitializeNodes();
        gameControl = FindObjectOfType<GameControl>();
        gameControl.Load(this);
        MoveToNode(startingNodeValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ending)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                currentNode.MoveToNextStep();
            }
        }
    }

    private void InitializeNodes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Node>() != null)
            {
                nodes.Add(transform.GetChild(i).GetComponent<Node>());
            }
        }
    }

        

    public void MoveToNode(int nodeNumber)
    {
        //D'abord, en quittant la node actuelle, on précise à son animateur qu'elle a désormais été visitée (au cas où l'on reviendrait dessus), mais aussi qu'elle est inactive (pour qu'elle se réinitialise).
        if (currentNode != null)
        {
            currentNode.animator.SetBool(DialogParameters.activeString, false);
            currentNode.animator.SetBool(DialogParameters.visitedString, true);
        }


        //Maintenant on en profite pour mettre à jour la node actuelle, on l'envoie à la console pour Debug et on la lance.
        currentNode = nodes[nodeNumber];
        FindObjectOfType<GameControl>().Save(this);
        Debug.Log("Node actuelle : " + currentNode.name);
        currentNode.LaunchNode();
    }

    public void Answer(int answerNumber)
    {
        currentNode.animator.SetTrigger("answer"+answerNumber.ToString());
    }

    public void LaunchEnding(string endText)
    {
        actionPanel.DisplayDescription(endText);
        ending = true;
    }

 }
