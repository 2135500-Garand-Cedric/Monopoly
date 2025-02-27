using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// S'occupe de g�rer le comportement d'un joueur
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// La liste de propri�t�s que le joueur poss�de
    /// </summary>
    public List<OwnableTile> properties = new();
    /// <summary>
    /// Indique si c'est � ce joueur de jouer ou non
    /// </summary>
    public bool yourTurn = false;
    /// <summary>
    /// La case sur laquelle le joueur se trouve pr�sentement
    /// </summary>
    [SerializeField] public GameTile currentTileOn;
    /// <summary>
    /// La case o� le joueur doit se d�placer
    /// </summary>
    [SerializeField] public GameTile goToTile;
    /// <summary>
    /// Le texte de la quantit� d'argent poss�d� par le joueur
    /// </summary>
    [SerializeField] public TextMeshProUGUI moneyUI;
    /// <summary>
    /// L'image du joueur dans la barre de droite
    /// Une image en jaune indique que c'est ce joueur qui est en train de jouer
    /// </summary>
    [SerializeField] public Image imageUI;
    /// <summary>
    /// La quantit� d'argent que le joueur poss�de
    /// </summary>
    public int money = 1500;
    /// <summary>
    /// La couleur qui repr�sente le joueur
    /// Cette couleur est utilis� pour marquer qui poss�de quelle propri�t�
    /// </summary>
    [SerializeField] public Material color;
    /// <summary>
    /// La vitesse de d�placement du joueur
    /// </summary>
    [SerializeField] private float movementSpeed = 2;
    /// <summary>
    /// Indique si le joueur est en prison ou non
    /// </summary>
    public bool inJail = false;
    /// <summary>
    /// Le shader pour un ensemble de propri�t� complet
    /// </summary>
    [SerializeField] private Material completeSet;
    /// <summary>
    /// Le nom du joueur en francais pour l'affichage
    /// </summary>
    [SerializeField] public string frenchName;

    void FixedUpdate()
    {
        if (yourTurn)
        {
            if (currentTileOn == goToTile)
            {
                // D�place le joueur vers le point qui lui est attribu�
                Collider tilePosition;
                if (inJail)
                {
                    tilePosition = currentTileOn.transform.Find(gameObject.name + "Jail").gameObject.GetComponent<Collider>();
                }
                else
                {
                    tilePosition = currentTileOn.transform.Find(gameObject.name).gameObject.GetComponent<Collider>();
                }
                float step = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, tilePosition.transform.position + new Vector3(0, 0.1f, 0), step);
            }
        }
    }

    /// <summary>
    /// Commence la coroutine de mouvement du joueur
    /// </summary>
    public void StartCoroutineMovement()
    {
        StartCoroutine(nameof(CoroutineMovement));
    }

    /// <summary>
    /// Tant que le joueur n'est pas � la case de destination, il se d�place vers la prochaine case
    /// </summary>
    private IEnumerator CoroutineMovement()
    {
        while (currentTileOn != goToTile)
        {
            Collider point = currentTileOn.NextTile.transform.Find("MainPath").gameObject.GetComponent<Collider>();
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, point.transform.position + new Vector3(0, 0.1f, 0), step);
            yield return null;
        }
    }

    /// <summary>
    /// Indique au joueur de se d�placer vers une case
    /// </summary>
    /// <param name="goTo">La case o� le joueur doit se d�placer</param>
    public void MoveTo(GameTile goTo)
    {
        yourTurn = true;
        goToTile = goTo;
        StartCoroutineMovement();
    }

    /// <summary>
    /// Cette m�thode se fait appeler lorsque le joueur atteint le MainPath de la prochaine case
    /// </summary>
    /// <param name="tileCurrentlyOn">Indique la case o� le joueur est rendu</param>
    public void OnDestinationReached(GameTile tileCurrentlyOn)
    {
        if (yourTurn)
        {
            currentTileOn = tileCurrentlyOn;
            if (currentTileOn == goToTile)
            {
                ExecuteTurn();
            }
            // Passer Go
            if (currentTileOn.gameObject.name == "Go")
            {
                GameController.Instance.MoneyChange(200);
            }
        }
    }

    /// <summary>
    /// Ex�cute l'action de la case o� le joueur s'arr�te
    /// </summary>
    private void ExecuteTurn()
    {
        currentTileOn.ExecuteAction();
        GameController.Instance.RefreshMoney();
    }

    /// <summary>
    /// Ex�cute les actions lorsque le joueur d�cide d'acheter une propri�t�
    /// </summary>
    public void BuyProperty()
    {
        OwnableTile boughtProperty = currentTileOn as OwnableTile;
        properties.Add(boughtProperty);
        GameObject propertyOwner = currentTileOn.transform.Find("PropertyOwner").gameObject;
        propertyOwner.SetActive(true);
        propertyOwner.GetComponent<MeshRenderer>().material = color;
        CheckCompleteSet(boughtProperty);
        GameController.Instance.MoneyChange(-boughtProperty.PropertyModel.Cost);
    }

    /// <summary>
    /// T�l�porte le joueur en prison
    /// </summary>
    /// <param name="jail">La case de prison</param>
    public void GoToJail(GameTile jail)
    {
        transform.position = new Vector3(0.4f, 0.1f, 10.6f);
        currentTileOn = jail;
        goToTile = jail;
        inJail = true;
    }

    /// <summary>
    /// Regarde si le joueur poss�de l'ensemble de propri�t�s au complet
    /// lorssqu'il ach�te une nouvelle propri�t�
    /// Si le joueur poss�de l'ensemble, applique le shader sur les propri�t�s de l'ensemble
    /// </summary>
    /// <param name="boughtProperty">La propri�t� qui vient d'�tre achet�e</param>
    private void CheckCompleteSet(OwnableTile boughtProperty)
    {
        if (GameController.Instance.OwnsSet(boughtProperty.PropertyModel, this))
        {
            List<OwnableTile> ownableTiles = GameController.Instance.GetOwnableTilesInSet(boughtProperty.PropertyModel);
            foreach (OwnableTile ownableTile in ownableTiles)
            {
                GameObject propertyOwner = ownableTile.transform.Find("PropertyOwner").gameObject;
                propertyOwner.GetComponent<MeshRenderer>().material = completeSet;
                propertyOwner.GetComponent<MeshRenderer>().material.SetColor("_Color", color.color);
            }
        }
    }
}
