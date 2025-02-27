using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de g�rer le comportement d'une case S�re
/// </summary>
public class SafeTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// L'action � ex�cuter lorsqu'un joueur s'arr�te sur cette case
    /// Action: Ne rien faire
    /// </summary>
    public override void ExecuteAction()
    {
        GameController.Instance.ActionDone();
    }
}
