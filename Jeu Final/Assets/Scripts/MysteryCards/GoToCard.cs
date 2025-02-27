using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de g�rer le comportement d'une carte myst�re 
/// qui indique au joueur de se d�placer vers une nouvelle case
/// </summary>
public class GoToCard : MysteryCard
{
    /// <summary>
    /// La description de la carte
    /// </summary>
    [SerializeField] private string cardText;
    public override string CardText => cardText;
    /// <summary>
    /// La case ou envoyer le joueur
    /// </summary>
    [SerializeField] private GameTile goToTile;
    /// <summary>
    /// Ex�cute ce que la carte indique de faire
    /// Action: D�place le joueur vers la case d�termin�e par la carte
    /// </summary>
    public override void ExecuteCard()
    {
        GameController.Instance.MovePlayer(goToTile);
    }
}
