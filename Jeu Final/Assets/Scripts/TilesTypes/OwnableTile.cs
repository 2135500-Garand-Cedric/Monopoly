using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Toutes les cases qui peuvent être achetées héritent de cette classe
/// Contient toutes les informations communes aux cases pouvant être achetées
/// </summary>
public abstract class OwnableTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    public abstract override GameTile NextTile { get; }

    /// <summary>
    /// L'action à exécuter lorsqu'un joueur s'arrète sur une propriété achetable
    /// </summary>
    public override void ExecuteAction()
    {
        Player propertyOwner = GameController.Instance.WhoOwnsTile(this);
        // Si personne ne la possède, propose au joueur de l'acheter
        if (propertyOwner == null)
        {
            GameController.Instance.ShowBuyProperty(this);
        }
        else
        {
            // Si un joueur la possède, faire payer le loyer
            Player activePlayer = GameController.Instance.GetActivePlayer();
            if (!IsMortgaged && activePlayer != propertyOwner)
            {
                GameController.Instance.MoneyChange(-CalculateRent(propertyOwner));
                propertyOwner.money += CalculateRent(propertyOwner);
            }
            GameController.Instance.ActionDone();
        }
    }
    /// <summary>
    /// Le modèle de propriété 
    /// (Toutes les cases dans le même ensemble ont le même modèle de propriété)
    /// </summary>
    public abstract PropertyModel PropertyModel { get; }
    /// <summary>
    /// Un booléen qui détermine si la case est hypothèquée ou non
    /// Une case hypothèquée possède une bordure rouge 
    /// et une case non hypothèquée possède une bordure grise
    /// </summary>
    public abstract bool IsMortgaged { get; set; }
    /// <summary>
    /// L'image de la propriété avec les différents loyers
    /// </summary>
    public abstract Sprite PropertyImage{ get; } 
    /// <summary>
    /// Calcule le loyer à payer selon les différentes cases achetable
    /// </summary>
    /// <param name="propertyOwner">La personne qui possède la case</param>
    /// <returns>Le loyer à payer</returns>
    public abstract int CalculateRent(Player propertyOwner);
    /// <summary>
    /// Change l'hypothèque de la case
    /// </summary>
    /// <param name="border">La nouvelle couleur de bordure de la case</param>
    /// <returns>La valeur de l'hypothèque</returns>
    public virtual int ChangeMortgage(Material border)
    {
        IsMortgaged = !IsMortgaged;
        GetComponent<MeshRenderer>().material = border;
        return PropertyModel.Mortgage;
    }
}
