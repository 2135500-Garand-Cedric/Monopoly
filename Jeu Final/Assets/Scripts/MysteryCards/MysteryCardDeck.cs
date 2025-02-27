using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de gérer le comportement d'un paquet de cartes mystères
/// </summary>
public class MysteryCardDeck: MonoBehaviour
{
    /// <summary>
    /// Le paquet de cartes mystères
    /// </summary>
    [SerializeField] private MysteryCard[] deck;
    /// <summary>
    /// Permet d'obtenir une carte aléatoire dans le paquet de cartes
    /// </summary>
    /// <returns>La carte mystère aléatoire</returns>
    public MysteryCard GetRandomCard()
    {
        int randomNumber = Random.Range(0, deck.Length);
        return deck[randomNumber];
    }
}
