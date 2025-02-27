using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de gérer le comportement d'une case propriété
/// </summary>
public class Property : OwnableTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le modèle de l'ensemble de propriété
    /// (Toutes les cases dans le même ensemble ont le même modèle de propriété)
    /// </summary>
    [SerializeField] private PropertyModel propertyModel;
    public override PropertyModel PropertyModel => propertyModel;
    /// <summary>
    /// Indique si la propriété est hypothèquée ou non
    /// </summary>
    private bool isMortgaged = false;
    public override bool IsMortgaged { get => isMortgaged; set => isMortgaged = value; }
    /// <summary>
    /// Le modèle de l'ensemble de propriété de couleur
    /// </summary>
    [SerializeField] public PropertySetModel propertySetModel;
    /// <summary>
    /// L'image de la propriété avec les différents loyers
    /// </summary>
    [SerializeField] private Sprite propertyImage;
    public override Sprite PropertyImage => propertyImage;
    /// <summary>
    /// Le nombre de maisons sur la propriété
    /// </summary>
    public int numberOfHouses = 0;
    /// <summary>
    /// Le prefab de maison
    /// </summary>
    [SerializeField] private GameObject housePrefab;
    /// <summary>
    /// Le prefab d'hotel
    /// </summary>
    [SerializeField] private GameObject hotelPrefab;
    /// <summary>
    /// Calcule le loyer d'une propriété
    /// Le loyer est calculé avec le nombre de maisons sur la propriété
    /// </summary>
    /// <param name="propertyOwner">le joueur qui possède la propriété</param>
    /// <returns>le loyer à payer</returns>
    public override int CalculateRent(Player propertyOwner)
    {
        int rentLevel = 0;
        if (GameController.Instance.OwnsSet(propertyModel, propertyOwner))
        {
            rentLevel = 1 + numberOfHouses;
        }
        return propertySetModel.RentCost[rentLevel];
    }

    /// <summary>
    /// Construire une maison sur la propriété
    /// </summary>
    public void BuildHouse()
    {
        numberOfHouses++;
        if (numberOfHouses == 5)
        {
            // Construire un hotel
            for (int i = 0; i < 4; i++)
            {
                Destroy(transform.GetChild(transform.childCount - (i + 1)).gameObject);
            }
            GameObject newInstance = Instantiate(hotelPrefab);
            newInstance.transform.parent = transform;
            newInstance.transform.localPosition = new Vector3(0.75f, 0.1f, 0.0f);
        } 
        else
        {
            // Ajouter une maison
            GameObject newInstance = Instantiate(housePrefab);
            newInstance.transform.parent = transform;
            newInstance.transform.localPosition = new Vector3(0.75f, 0.1f, 0.575f - (numberOfHouses * 0.23f));
        }
    }

    /// <summary>
    /// Retirer une maison sur la propriété
    /// </summary>
    public void RemoveHouse()
    {
        numberOfHouses--;
        if (numberOfHouses == 4)
        {
            // Détruire l'hotel et mettre 4 maisons
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            numberOfHouses = 0;
            for (int i = 0; i < 4; i++)
            {
                BuildHouse();
            }
        }
        else
        {
            // Retirer une maison
            Transform house = transform.GetChild(transform.childCount - 1);
            house.transform.parent = null;
            Destroy(house.gameObject);
        }
        
    }
}
