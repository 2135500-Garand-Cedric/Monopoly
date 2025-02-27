using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de g�rer le comportement d'un paquet de cartes myst�res
/// </summary>
public class MysteryCardDeck: MonoBehaviour
{
    /// <summary>
    /// Le paquet de cartes myst�res
    /// </summary>
    [SerializeField] private MysteryCard[] deck;
    /// <summary>
    /// Permet d'obtenir une carte al�atoire dans le paquet de cartes
    /// </summary>
    /// <returns>La carte myst�re al�atoire</returns>
    public MysteryCard GetRandomCard()
    {
        int randomNumber = Random.Range(0, deck.Length);
        return deck[randomNumber];
    }
}
