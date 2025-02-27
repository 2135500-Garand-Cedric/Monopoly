using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de gérer le comportement de la case de taxe
/// </summary>
public class TaxTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le coût de la taxe
    /// </summary>
    [SerializeField] private int taxCost;
    /// <summary>
    /// L'action à exécuter lorsqu'un joueur s'arrête sur un case de taxe
    /// Action: Le joueur paye la taxe
    /// </summary>
    public override void ExecuteAction()
    {
        GameController.Instance.MoneyChange(-taxCost);
        GameController.Instance.ActionDone();
    }
}
