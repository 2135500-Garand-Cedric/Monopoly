using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de g�rer le comportement d'une case de Service
/// </summary>
public class ServiceTile : OwnableTile
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
    /// Le diff�rents niveau de loyers
    /// </summary>
    private int[] rentCosts = { 4, 10 };
    /// <summary>
    /// Calcule le loyer du Service
    /// Le loyer est calcul� avec le nombre de propri�t�s que le propri�taire poss�de
    /// le nombre de propri�t�s poss�d�s donne un multiplicateur 
    /// qui est multipli� avec le resultat des d�s pour d�terminer le loyer
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
