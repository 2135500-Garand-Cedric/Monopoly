using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de gérer le comportement d'une carte mystère 
/// qui indique au joueur de se déplacer vers une nouvelle case
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
    /// Exécute ce que la carte indique de faire
    /// Action: Déplace le joueur vers la case déterminée par la carte
    /// </summary>
    public override void ExecuteCard()
    {
        GameController.Instance.MovePlayer(goToTile);
    }
}
