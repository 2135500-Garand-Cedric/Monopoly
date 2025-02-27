using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de gérer le comportement d'une case Chemin de Fer
/// </summary>
public class RailRoadTile : OwnableTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le modèle de l'ensemble de propriété
    /// (Toutes les cases dans le même ensemble ont le même modèle de propriété)
    /// </summary>
    [SerializeField] private PropertyModel propertyModel;
    public override PropertyModel PropertyModel => propertyModel;
    /// <summary>
    /// Indique si la propriété est hypothèquée ou non
    /// </summary>
    private bool isMortgaged = false;
    public override bool IsMortgaged { get => isMortgaged; set => isMortgaged = value; }
    /// <summary>
    /// L'image de la propriété avec les différents loyers
    /// </summary>
    [SerializeField] private Sprite propertyImage;
    public override Sprite PropertyImage => propertyImage;
    /// <summary>
    /// Les différents coûts de loyers
    /// </summary>
    private int[] rentCosts = { 25, 50, 100, 200 };

    /// <summary>
    /// Calcule le loyer de la case
    /// Le loyer est calculé en fonction du nombre de propriété que le propriétaire possède
    /// </summary>
    /// <param name="propertyOwner">le joueur qui possède la propriété</param>
    /// <returns>Le loyer à payer</returns>
    public override int CalculateRent(Player propertyOwner)
    {
        int rentLevel = GameController.Instance.NumberPropertiesOwnedInSet(propertyModel, propertyOwner) - 1;
        return rentCosts[rentLevel];
    }
}
