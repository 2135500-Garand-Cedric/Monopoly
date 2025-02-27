using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toutes les cases du jeu héritent de cette classe
/// Contient les informations communes à toutes les cases
/// </summary>
public abstract class GameTile : MonoBehaviour
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    public abstract GameTile NextTile { get; }
    /// <summary>
    /// L'action à exécuter lorsqu'un joueur s'arrête sur la case
    /// </summary>
    public abstract void ExecuteAction();
}
