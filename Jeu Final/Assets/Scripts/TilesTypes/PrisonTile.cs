using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de g�rer le comportement de la case Prison
/// </summary>
public class PrisonTile : GameTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// L'action � ex�cuter lorsqu'un joueur s'arr�te sur la case prison
    /// Action: Commencer la coroutine de prison
    /// </summary>
    public override void ExecuteAction()
    {
        GameController.Instance.StartCoroutineGoToJail();
    }
}
