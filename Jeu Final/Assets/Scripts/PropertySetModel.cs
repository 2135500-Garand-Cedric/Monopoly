using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Un scriptable object que les propriétés de couleur ont
/// </summary>
[CreateAssetMenu(fileName = "Property Set Model", menuName = "game/Property Set Model")]
public class PropertySetModel : ScriptableObject
{
    /// <summary>
    /// Les différents niveau de loyer
    /// </summary>
    [SerializeField]
    private int[] rentCost;
    public int[] RentCost => rentCost;
    /// <summary>
    /// Le coût pour construire une maison sur cette propriété
    /// </summary>
    [SerializeField]
    private int buildingCost;
    public int BuildingCost => buildingCost;
}
