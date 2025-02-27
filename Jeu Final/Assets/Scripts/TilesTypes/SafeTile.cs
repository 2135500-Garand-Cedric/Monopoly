using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de gérer le comportement d'une case Sûre
/// </summary>
public class SafeTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// L'action à exécuter lorsqu'un joueur s'arrête sur cette case
    /// Action: Ne rien faire
    /// </summary>
    public override void ExecuteAction()
    {
        GameController.Instance.ActionDone();
    }
}
