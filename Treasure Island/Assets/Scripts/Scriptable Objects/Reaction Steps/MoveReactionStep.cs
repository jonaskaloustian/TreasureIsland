using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReactionStep : ReactionStep
{
    public int nodeValue;

    //En sortant, on indique à l'animateur qu'il est désormais inactif
    //On autosave les données.
    //On demande au panneau d'action d'arrêter d'afficher le texte
    //On bascule vers la node suivante
    protected override void React()
    {
        node.animator.SetBool(DialogParameters.activeString, false);
        node.grid.actionPanel.DisplayNone();
        node.grid.MoveToNode(nodeValue);
    }
}
