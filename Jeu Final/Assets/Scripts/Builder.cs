using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Permet à une classe d'appeler l'évènement build
/// </summary>
public class Builder : MonoBehaviour
{
    /// <summary>
    /// L'évènement permet d'indiquer au controleur de construction
    /// quel joueur veut construire
    /// </summary>
    [SerializeField] protected UnityEvent<Player> build;
}
