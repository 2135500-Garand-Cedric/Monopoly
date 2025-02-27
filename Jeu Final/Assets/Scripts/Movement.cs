using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Permet à une classe d'appeler l'évènement OnDestinationReached
/// </summary>
public class Movement : MonoBehaviour
{
    /// <summary>
    /// L'évènement permet d'indiquer qu'une case été atteinte
    /// </summary>
    [SerializeField] protected UnityEvent<GameTile> OnDestinationReached;
}
