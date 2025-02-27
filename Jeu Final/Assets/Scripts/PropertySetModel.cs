using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Un scriptable object que les propri�t�s de couleur ont
/// </summary>
[CreateAssetMenu(fileName = "Property Set Model", menuName = "game/Property Set Model")]
public class PropertySetModel : ScriptableObject
{
    /// <summary>
    /// Les diff�rents niveau de loyer
    /// </summary>
    [SerializeField]
    private int[] rentCost;
    public int[] RentCost => rentCost;
    /// <summary>
    /// Le co�t pour construire une maison sur cette propri�t�
    /// </summary>
    [SerializeField]
    private int buildingCost;
    public int BuildingCost => buildingCost;
}
