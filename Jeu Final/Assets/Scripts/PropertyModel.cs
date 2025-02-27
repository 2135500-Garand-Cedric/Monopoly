using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Un scriptable object que toutes les cases achetables ont
/// </summary>
[CreateAssetMenu(fileName = "Property Model", menuName = "game/Property Model")]
public class PropertyModel : ScriptableObject
{
    /// <summary>
    /// Le co�t de la case achetable
    /// </summary>
    [SerializeField]
    private int cost;
    public int Cost => cost;
    /// <summary>
    /// La valeur de l'hypoth�que de la case achetable
    /// </summary>
    [SerializeField]
    private int mortgage;
    public int Mortgage => mortgage;
    /// <summary>
    /// Le nombre de propri�t�s dans l'ensemble
    /// </summary>
    [SerializeField]
    private int propertiesInSet;
    public int PropertiesInSet => propertiesInSet;
}

