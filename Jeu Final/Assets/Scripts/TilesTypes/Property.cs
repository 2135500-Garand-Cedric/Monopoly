using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de g�rer le comportement d'une case propri�t�
/// </summary>
public class Property : OwnableTile
{
    /// <summary>
    /// La prochaine case
    /// </summary>
    [SerializeField] private GameTile nextTile;
    public override GameTile NextTile { get => nextTile; }
    /// <summary>
    /// Le mod�le de l'ensemble de propri�t�
    /// (Toutes les cases dans le m�me ensemble ont le m�me mod�le de propri�t�)
    /// </summary>
    [SerializeField] private PropertyModel propertyModel;
    public override PropertyModel PropertyModel => propertyModel;
    /// <summary>
    /// Indique si la propri�t� est hypoth�qu�e ou non
    /// </summary>
    private bool isMortgaged = false;
    public override bool IsMortgaged { get => isMortgaged; set => isMortgaged = value; }
    /// <summary>
    /// Le mod�le de l'ensemble de propri�t� de couleur
    /// </summary>
    [SerializeField] public PropertySetModel propertySetModel;
    /// <summary>
    /// L'image de la propri�t� avec les diff�rents loyers
    /// </summary>
    [SerializeField] private Sprite propertyImage;
    public override Sprite PropertyImage => propertyImage;
    /// <summary>
    /// Le nombre de maisons sur la propri�t�
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
    /// Calcule le loyer d'une propri�t�
    /// Le loyer est calcul� avec le nombre de maisons sur la propri�t�
    /// </summary>
    /// <param name="propertyOwner">le joueur qui poss�de la propri�t�</param>
    /// <returns>le loyer � payer</returns>
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
    /// Construire une maison sur la propri�t�
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
    /// Retirer une maison sur la propri�t�
    /// </summary>
    public void RemoveHouse()
    {
        numberOfHouses--;
        if (numberOfHouses == 4)
        {
            // D�truire l'hotel et mettre 4 maisons
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
