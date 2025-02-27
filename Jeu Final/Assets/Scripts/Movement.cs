using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Permet � une classe d'appeler l'�v�nement OnDestinationReached
/// </summary>
public class Movement : MonoBehaviour
{
    /// <summary>
    /// L'�v�nement permet d'indiquer qu'une case �t� atteinte
    /// </summary>
    [SerializeField] protected UnityEvent<GameTile> OnDestinationReached;
}
