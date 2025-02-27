using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toutes les cases du jeu h�ritent de cette classe
/// Contient les informations communes � toutes les cases
/// </summary>
public abstract class GameTile : MonoBehaviour
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    public abstract GameTile NextTile { get; }
    /// <summary>
    /// L'action � ex�cuter lorsqu'un joueur s'arr�te sur la case
    /// </summary>
    public abstract void ExecuteAction();
}
