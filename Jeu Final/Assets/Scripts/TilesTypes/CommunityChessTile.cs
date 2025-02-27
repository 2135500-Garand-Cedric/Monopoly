using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de g�rer le comportement d'une case Caisse Commune
/// </summary>
public class CommunityChessTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le paquet de cartes caisse communes
    /// </summary>
    [SerializeField] private MysteryCardDeck communityChestDeck;
    /// <summary>
    /// L'action � ex�cuter lorsqu'un joueur s'arr�te sur la case
    /// Action: Pige une carte al�atoire du paquet et ex�cute la carte
    /// </summary>
    public override void ExecuteAction()
    {
        MysteryCard communityChestCard = communityChestDeck.GetRandomCard();
        GameController.Instance.StartCoroutineMysteryCard(communityChestCard, "Caisse Commune");
    }
}
