using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    private List<Node> nodes = new List<Node>();
    public int startingNodeValue;
    private Node currentNode;
    public GlobalValues globalValues;
    public BackgroundImage backgroundImage;
    public ActionPanel actionPanel;
    public Inventory inventory;

    private void Awake()
    {
        InitializeNodes();
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
            Debug.Log("Attention currentNode n'a pas été initialisé. Le jeu ne peut se lancer");
        }
    }

    public void MoveToNode (int nodeNumber)
    {
        currentNode = nodes[nodeNumber];
        Debug.Log("Node actuelle : " + currentNode.name );
        currentNode.LaunchNode();
    }

    

}
