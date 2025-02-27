using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de contr�ler le fonctionnement de la construction
/// </summary>
public class BuildController : MonoBehaviour
{
    /// <summary>
    /// Les ensembles de propri�t�s qui peuvent avoir des maisons construites dessus
    /// </summary>
    [SerializeField] private PropertyModel[] propertyModels;
    /// <summary>
    /// Les boutons des diff�rents ensembles de propri�t�s
    /// </summary>
    [SerializeField] private Button[] propertyButtons;
    /// <summary>
    /// La zone pour la construction de maisons
    /// Cette zone est seulement accessible si le joueur poss�de un ensemble de propri�t�
    /// </summary>
    [SerializeField] private Image buildHouseZone;
    /// <summary>
    /// Le texte qui indique le nombre de propri�t�s dans l'ensemble
    /// </summary>
    [SerializeField] private TextMeshProUGUI buildPropertiesInSet;
    /// <summary>
    /// Le texte qui indique le nombre de maison pr�sentement construites sur l'ensemble
    /// </summary>
    [SerializeField] private TextMeshProUGUI buildCurrentNumberHouses;
    /// <summary>
    /// Le texte qui indique le nombre maximal de maison pouvant �tre construite sur l'ensemble
    /// </summary>
    [SerializeField] private TextMeshProUGUI buildMaxNumberHouses;
    /// <summary>
    /// Le tetxe qui indique le co�t de construction
    /// </summary>
    [SerializeField] private TextMeshProUGUI buildMoneyChange;
    /// <summary>
    /// Le texte qui indique le co�t par maison
    /// </summary>
    [SerializeField] private TextMeshProUGUI costPerHouse;
    /// <summary>
    /// Le textInput o� le joueur peut indiquer combien de maisons il veut construire
    /// </summary>
    [SerializeField] private TMP_InputField buildNumberHouseInput;
    /// <summary>
    /// L'audio de construction
    /// </summary>
    [SerializeField] private AudioSource building;
    /// <summary>
    /// Le nombre de maisons � construire
    /// </summary>
    private int numberHousesToBuild = 0;
    /// <summary>
    /// Le nombre de maisons pr�sentement construites sur l'ensemble
    /// </summary>
    private int currentNumberOfHouses = 0;
    /// <summary>
    /// Le nombre maximal de maisons pouvant �tre construites sur l'ensemble
    /// </summary>
    private int maxNumberHouses = 0;
    /// <summary>
    /// Le co�t de construction
    /// </summary>
    private int moneyChange = 0;
    /// <summary>
    /// L'ensemble de propri�t� actif pour la construction de maisons
    /// </summary>
    private PropertyModel activeModel;
    /// <summary>
    /// L'ensemble de propri�t�s pour l'ensemble de couleur actif pour la construction de maisons
    /// </summary>
    private PropertySetModel activeSetModel;

    private void Start()
    {
        buildHouseZone.gameObject.SetActive(false);
    }

    /// <summary>
    /// Affiche l'�cran de construction et active seulement les boutons d'ensemble de
    /// propri�t�s que le joueur poss�de
    /// </summary>
    /// <param name="player"></param>
    public void ShowBuildScreen(Player player)
    {
        for (int i = 0; i < propertyModels.Length; i++)
        {
            if (GameController.Instance.OwnsSet(propertyModels[i], player))
            {
                propertyButtons[i].interactable = true;
            }
            else
            {
                propertyButtons[i].interactable = false;
            }
        }
    }

    /// <summary>
    /// Affiche la zone de construction lorsque le joueur appuie sur
    /// un bouton d'ensemble de propri�t�
    /// Affiche les informations necessaires dans la zone
    /// </summary>
    /// <param name="propertyModel">L'ensemble de propri�t� qui a �t� appuy�</param>
    public void ShowBuildHousesZone(PropertyModel propertyModel)
    {
        currentNumberOfHouses = 0;
        activeModel = propertyModel;
        buildHouseZone.gameObject.SetActive(true);
        buildPropertiesInSet.text = "Nombre de propri�t�s dans la couleur: " + propertyModel.PropertiesInSet.ToString();
        List<Property> properties = GameController.Instance.GetPropertiesInSet(propertyModel);
        activeSetModel = properties[0].propertySetModel;
        foreach (Property property in properties)
        {
            currentNumberOfHouses += property.numberOfHouses;
        }
        maxNumberHouses = propertyModel.PropertiesInSet * 5;
        buildCurrentNumberHouses.text = "Nombre de maisons actuel: " + currentNumberOfHouses.ToString();
        buildMaxNumberHouses.text = "Nombre de maisons max: " + maxNumberHouses.ToString();
        buildMoneyChange.text = "Co�t: 0$";
        costPerHouse.text = "Co�t par maison: " + activeSetModel.BuildingCost.ToString() + "$";
    }

    /// <summary>
    /// Lorsque le textField se fait d�selectionner,
    /// valide le nombre entr� dans le textField
    /// </summary>
    /// <param name="value">la valeur dans le textField</param>
    public void OnInputDeselect(string value)
    {
        int.TryParse(value, out int intValue);
        numberHousesToBuild = intValue;
        if (intValue + currentNumberOfHouses >= maxNumberHouses)
        {
            numberHousesToBuild = maxNumberHouses - currentNumberOfHouses;
        }
        if (intValue + currentNumberOfHouses < 0)
        {
            numberHousesToBuild = -currentNumberOfHouses;
            
        }
        if (numberHousesToBuild > 0)
        {
            moneyChange = activeSetModel.BuildingCost * numberHousesToBuild;
        }
        else
        {
            moneyChange = (int)(activeSetModel.BuildingCost * numberHousesToBuild * 0.5f);
        }
        buildNumberHouseInput.text = numberHousesToBuild.ToString();
        buildMoneyChange.text = "Co�t: " + moneyChange.ToString() + "$";
    }

    /// <summary>
    /// Lorsque le joueur soumet le formulaire de construction
    /// Si le nombre dans le textField est positif, constuit les maisons
    /// Si le nombre dans le textField est n�gatif, vends des maisons
    /// </summary>
    public void BuildHouses()
    {
        GameController.Instance.EndBuilding();
        buildHouseZone.gameObject.SetActive(false);
        GameController.Instance.MoneyChange(-moneyChange);
        buildNumberHouseInput.text = "";
        List<Property> properties = GameController.Instance.GetPropertiesInSet(activeModel);
        if (numberHousesToBuild > 0)
        {
            // Fait en sorte qu'il n'y aie pas deux maisons de diff�rence
            // entre les propri�t�s d'un ensemble lors de la construction
            for (int i = 0; i < numberHousesToBuild; i++)
            {
                if (properties[i % properties.Count].numberOfHouses <= properties[(i + 1) % properties.Count].numberOfHouses &&
                    properties[i % properties.Count].numberOfHouses <= properties[(i + 2) % properties.Count].numberOfHouses)
                {
                    properties[i % properties.Count].BuildHouse();
                }
                else if (properties[(i + 1) % properties.Count].numberOfHouses <= properties[(i + 2) % properties.Count].numberOfHouses)
                {
                    properties[(i + 1) % properties.Count].BuildHouse();
                }
                else
                {
                    properties[(i + 2) % properties.Count].BuildHouse();
                }
            }
            building.Play();
        }
        else
        {
            numberHousesToBuild *= -1;
            // Fait en sorte qu'il n'y aie pas deux maisons de diff�rence
            // entre les propri�t�s d'un ensemble lors de la vente de maisons
            for (int i = 0; i < numberHousesToBuild; i++)
            {
                if (properties[i % properties.Count].numberOfHouses >= properties[(i + 1) % properties.Count].numberOfHouses &&
                    properties[i % properties.Count].numberOfHouses >= properties[(i + 2) % properties.Count].numberOfHouses)
                {
                    properties[i % properties.Count].RemoveHouse();
                }
                else if (properties[(i + 1) % properties.Count].numberOfHouses >= properties[(i + 2) % properties.Count].numberOfHouses)
                {
                    properties[(i + 1) % properties.Count].RemoveHouse();
                }
                else
                {
                    properties[(i + 2) % properties.Count].RemoveHouse();
                }
            }
        }
        
    }
}
