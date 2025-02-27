using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de gérer le comportement d'une case de Service
/// </summary>
public class ServiceTile : OwnableTile
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
    /// Le différents niveau de loyers
    /// </summary>
    private int[] rentCosts = { 4, 10 };
    /// <summary>
    /// Calcule le loyer du Service
    /// Le loyer est calculé avec le nombre de propriétés que le propriétaire possède
    /// le nombre de propriétés possèdés donne un multiplicateur 
    /// qui est multiplié avec le resultat des dès pour déterminer le loyer
    /// </summary>
    /// <param name="propertyOwner"></param>
    /// <returns></returns>
    public override int CalculateRent(Player propertyOwner)
    {
        int rentLevel = GameController.Instance.NumberPropertiesOwnedInSet(propertyModel, propertyOwner) - 1;
        int diceRoll = GameController.Instance.GetDiceResult();
        return rentCosts[rentLevel] * diceRoll;
    }
}
