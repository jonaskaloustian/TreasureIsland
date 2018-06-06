using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour {

    private List<Node> nodes = new List<Node>();
    public int startingNodeValue;
    private Node currentNode;
    public GlobalValues globalValues;
    public BackgroundImage backgroundImage;
    public ActionPanel actionPanel;
    public Inventory inventory;
    public RectTransform movingIcon;

    private void Awake()
    {
        InitializeNodes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentNode.MoveToNextStep();
        }
    }

    private void InitializeNodes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Node>() != null)
            {
                nodes.Add(transform.GetChild(i).GetComponent<Node>());
                if (i == startingNodeValue)
                {
                    MoveToNode(startingNodeValue);
                }
            }
        }
        if (currentNode == null)
        {
            Debug.Log("Attention startingNodeValue n'a pas été spécifié ou n'est pas compris entre 0 et 24. Le jeu ne peut se lancer");
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
        Debug.Log("Node actuelle : " + currentNode.name);
        currentNode.LaunchNode();
    }

    public void Answer(int answerNumber)
    {
        currentNode.animator.SetTrigger("answer"+answerNumber.ToString());
    }

 }
