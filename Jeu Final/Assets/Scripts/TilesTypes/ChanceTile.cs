using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de gèrer le comportement d'une case Chance
/// </summary>
public class ChanceTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le paquet de cartes chances
    /// </summary>
    [SerializeField] private MysteryCardDeck chanceDeck;

    /// <summary>
    /// L'action à exécuter lorsqu'un joueur s'arrête sur la case
    /// Action: Pige une carte aléatoire du paquet et exécute la carte
    /// </summary>
    public override void ExecuteAction()
    {
        MysteryCard chanceCard = chanceDeck.GetRandomCard();
        GameController.Instance.StartCoroutineMysteryCard(chanceCard, "Chance");
    }
}
