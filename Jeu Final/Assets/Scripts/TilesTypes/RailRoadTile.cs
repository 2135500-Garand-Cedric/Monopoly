using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de g�rer le comportement d'une case Chemin de Fer
/// </summary>
public class RailRoadTile : OwnableTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le mod�le de l'ensemble de propri�t�
    /// (Toutes les cases dans le m�me ensemble ont le m�me mod�le de propri�t�)
    /// </summary>
    [SerializeField] private PropertyModel propertyModel;
    public override PropertyModel PropertyModel => propertyModel;
    /// <summary>
    /// Indique si la propri�t� est hypoth�qu�e ou non
    /// </summary>
    private bool isMortgaged = false;
    public override bool IsMortgaged { get => isMortgaged; set => isMortgaged = value; }
    /// <summary>
    /// L'image de la propri�t� avec les diff�rents loyers
    /// </summary>
    [SerializeField] private Sprite propertyImage;
    public override Sprite PropertyImage => propertyImage;
    /// <summary>
    /// Les diff�rents co�ts de loyers
    /// </summary>
    private int[] rentCosts = { 25, 50, 100, 200 };

    /// <summary>
    /// Calcule le loyer de la case
    /// Le loyer est calcul� en fonction du nombre de propri�t� que le propri�taire poss�de
    /// </summary>
    /// <param name="propertyOwner">le joueur qui poss�de la propri�t�</param>
    /// <returns>Le loyer � payer</returns>
    public override int CalculateRent(Player propertyOwner)
    {
        int rentLevel = GameController.Instance.NumberPropertiesOwnedInSet(propertyModel, propertyOwner) - 1;
        return rentCosts[rentLevel];
    }
}
