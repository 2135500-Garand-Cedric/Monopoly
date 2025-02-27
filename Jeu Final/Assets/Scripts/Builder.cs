using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Permet � une classe d'appeler l'�v�nement build
/// </summary>
public class Builder : MonoBehaviour
{
    /// <summary>
    /// L'�v�nement permet d'indiquer au controleur de construction
    /// quel joueur veut construire
    /// </summary>
    [SerializeField] protected UnityEvent<Player> build;
}
