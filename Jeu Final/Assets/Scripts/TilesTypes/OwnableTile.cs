using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Toutes les cases qui peuvent �tre achet�es h�ritent de cette classe
/// Contient toutes les informations communes aux cases pouvant �tre achet�es
/// </summary>
public abstract class OwnableTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    public abstract override GameTile NextTile { get; }

    /// <summary>
    /// L'action � ex�cuter lorsqu'un joueur s'arr�te sur une propri�t� achetable
    /// </summary>
    public override void ExecuteAction()
    {
        Player propertyOwner = GameController.Instance.WhoOwnsTile(this);
        // Si personne ne la poss�de, propose au joueur de l'acheter
        if (propertyOwner == null)
        {
            GameController.Instance.ShowBuyProperty(this);
        }
        else
        {
            // Si un joueur la poss�de, faire payer le loyer
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
    /// Le mod�le de propri�t� 
    /// (Toutes les cases dans le m�me ensemble ont le m�me mod�le de propri�t�)
    /// </summary>
    public abstract PropertyModel PropertyModel { get; }
    /// <summary>
    /// Un bool�en qui d�termine si la case est hypoth�qu�e ou non
    /// Une case hypoth�qu�e poss�de une bordure rouge 
    /// et une case non hypoth�qu�e poss�de une bordure grise
    /// </summary>
    public abstract bool IsMortgaged { get; set; }
    /// <summary>
    /// L'image de la propri�t� avec les diff�rents loyers
    /// </summary>
    public abstract Sprite PropertyImage{ get; } 
    /// <summary>
    /// Calcule le loyer � payer selon les diff�rentes cases achetable
    /// </summary>
    /// <param name="propertyOwner">La personne qui poss�de la case</param>
    /// <returns>Le loyer � payer</returns>
    public abstract int CalculateRent(Player propertyOwner);
    /// <summary>
    /// Change l'hypoth�que de la case
    /// </summary>
    /// <param name="border">La nouvelle couleur de bordure de la case</param>
    /// <returns>La valeur de l'hypoth�que</returns>
    public virtual int ChangeMortgage(Material border)
    {
        IsMortgaged = !IsMortgaged;
        GetComponent<MeshRenderer>().material = border;
        return PropertyModel.Mortgage;
    }
}
