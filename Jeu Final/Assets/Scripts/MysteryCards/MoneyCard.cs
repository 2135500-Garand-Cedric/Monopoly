using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de gérer le comportement d'une carte mystére
/// qui donne ou retire de l'argent au joueur
/// </summary>
public class MoneyCard : MysteryCard
{
    /// <summary>
    /// La description de la carte
    /// </summary>
    [SerializeField] private string cardText;
    public override string CardText => cardText;
    /// <summary>
    /// La qauntitée d'argent ajouté ou soustrait du joueur
    /// </summary>
    [SerializeField] private int moneyChange;
    /// <summary>
    /// Exécute ce que la carte indique de faire
    /// Action: Donne ou retire de l'argent au joueur
    /// </summary>
    public override void ExecuteCard()
    {
        GameController.Instance.MoneyChange(moneyChange);
        GameController.Instance.ActionDone();
    }
}
