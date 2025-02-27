using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// S'occupe de gérer le comportement d'une case mystére
/// Toutes le cases mystéres héritent de cette classe
/// Contient les informations communes à toutes les cartes mystéres
/// </summary>
public abstract class MysteryCard : MonoBehaviour
{
    /// <summary>
    /// La description de la carte
    /// </summary>
    public abstract string CardText { get; }
    /// <summary>
    /// L'action à exécuter lorsque la carte est pigée
    /// </summary>
    public abstract void ExecuteCard();
}
