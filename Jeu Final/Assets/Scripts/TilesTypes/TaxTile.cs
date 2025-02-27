using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de g�rer le comportement de la case de taxe
/// </summary>
public class TaxTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le co�t de la taxe
    /// </summary>
    [SerializeField] private int taxCost;
    /// <summary>
    /// L'action � ex�cuter lorsqu'un joueur s'arr�te sur un case de taxe
    /// Action: Le joueur paye la taxe
    /// </summary>
    public override void ExecuteAction()
    {
        GameController.Instance.MoneyChange(-taxCost);
        GameController.Instance.ActionDone();
    }
}
