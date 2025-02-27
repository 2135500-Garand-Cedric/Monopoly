using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// S'occupe de g�rer le comportement d'une case myst�re
/// Toutes le cases myst�res h�ritent de cette classe
/// Contient les informations communes � toutes les cartes myst�res
/// </summary>
public abstract class MysteryCard : MonoBehaviour
{
    /// <summary>
    /// La description de la carte
    /// </summary>
    public abstract string CardText { get; }
    /// <summary>
    /// L'action � ex�cuter lorsque la carte est pig�e
    /// </summary>
    public abstract void ExecuteCard();
}
