using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// S'occupe de controler le fonctionnement du jeu
/// </summary>
public class GameController : Builder
{
    /// <summary>
    /// L'instance du controleur de jeu accessible partout
    /// </summary>
    public static GameController Instance { get; private set; }
    /// <summary>
    /// La liste de joueurs
    /// </summary>
    [SerializeField] private List<Player> players;
    /// <summary>
    /// La liste de cases de jeu en ordre
    /// </summary>
    [SerializeField] public GameTile[] tilesList;
    /// <summary>
    /// Les diff�rents feux d'artifices pour la fin de la partie
    /// </summary>
    [SerializeField] private ParticleSystem[] fireworks;
    /// <summary>
    /// L'image permettant au joueur de lancer les d�s
    /// </summary>
    [SerializeField] private Image rollDiceImage;
    /// <summary>
    /// L'image permettant au joueur de terminer son tour
    /// </summary>
    [SerializeField] private Image endTurnButton;
    /// <summary>
    /// L'image permettant au joueur de vendre automatiquement ses maisons
    /// et hypoth�quer ses propri�t�s lorsqu'il est dans le n�gatif
    /// </summary>
    [SerializeField] private Image autoSellButton;
    /// <summary>
    /// L'image permettant au joueur de sortir de prison
    /// </summary>
    [SerializeField] private Image escapeJail;
    /// <summary>
    /// L'image permettant au joueur d'ouvrir le didacticiel
    /// </summary>
    [SerializeField] private Image tutorialButton;
    /// <summary>
    /// Le texte indiquant l'argent du joueur actif
    /// </summary>
    [SerializeField] private TextMeshProUGUI moneyActivePlayer;
    /// <summary>
    /// Le texte permettant de voir les changements d'argents du joueur actif
    /// Utilis� dans l'animation d'argent
    /// </summary>
    [SerializeField] private TextMeshProUGUI moneyChangeActivePlayer;
    /// <summary>
    /// L'Animator qui contr�le l'animation de changement d'argent
    /// </summary>
    [SerializeField] private Animator moneyChangeAnimator;
    /// <summary>
    /// La zone qui affiche une propri�t� lorsque celle-ci peut �tre achet�e
    /// </summary>
    [SerializeField] public Image showPropertyBuy;
    /// <summary>
    /// L'image de la propri�t� avec les informations de loyers
    /// </summary>
    [SerializeField] private Image propertyImage;
    /// <summary>
    /// Le prefab du carr� indiquant qui poss�de chaque propri�t�
    /// </summary>
    [SerializeField] private GameObject propertyOwnerPrefab;
    /// <summary>
    /// La zone qui affiche une carte myst�re
    /// </summary>
    [SerializeField] private Image showMysteryCard;
    /// <summary>
    /// Le texte de la description de la carte myst�re
    /// </summary>
    [SerializeField] private TextMeshProUGUI mysteryCardText;
    /// <summary>
    /// Le texte du titre de la carte myst�re
    /// </summary>
    [SerializeField] private TextMeshProUGUI mysteryCardTitle;
    /// <summary>
    /// Le temps que la carte myst�re reste affich�e
    /// </summary>
    [SerializeField] private float mysteryCardTime;
    /// <summary>
    /// Le slider qui indique le temps restant pour l'affichage de la carte myst�re
    /// </summary>
    [SerializeField] private Slider timeRemainingMysteryCard;
    /// <summary>
    /// Les d�s
    /// </summary>
    [SerializeField] private Rigidbody[] dices;
    /// <summary>
    /// L'Animator qui s'occupe de l'animation de la prison
    /// </summary>
    [SerializeField] private Animator jailAnimator;
    /// <summary>
    /// La prison
    /// </summary>
    [SerializeField] private GameObject jail;
    /// <summary>
    /// La camera principale
    /// </summary>
    [SerializeField] private Camera camera;
    /// <summary>
    /// La touche pour simuler un lancer de d�s
    /// </summary>
    [SerializeField] private KeyCode quickRoll;
    /// <summary>
    /// La touche pour simuler la fin du tour
    /// </summary>
    [SerializeField] private KeyCode quickNext;
    /// <summary>
    /// La touche pour simuler la d�but de la construction
    /// </summary>
    [SerializeField] private KeyCode quickBuild;
    /// <summary>
    /// La touche pour simuler la vente des maisons et l'hypoth�que des propri�t�s
    /// </summary>
    [SerializeField] private KeyCode quickSell;
    /// <summary>
    /// L'�cran de construction
    /// </summary>
    [SerializeField] private Image buildScreen;
    /// <summary>
    /// Le bouton permettant d'acc�der � l'�cran de construction
    /// </summary>
    [SerializeField] private Image buildButton;
    /// <summary>
    /// La bordure d'une case normale
    /// </summary>
    [SerializeField] private Material normalBorder;
    /// <summary>
    /// La bordure d'une case hypoth�qu�e
    /// </summary>
    [SerializeField] private Material mortgagedBorder;
    /// <summary>
    /// Le controleur de didacticiel
    /// </summary>
    [SerializeField] private TutorialController tutorialController;
    /// <summary>
    /// L'audio de la sir�ne de police
    /// </summary>
    [SerializeField] private AudioSource policeSiren;
    /// <summary>
    /// L'audio de changement d'argent
    /// </summary>
    [SerializeField] private AudioSource money;
    /// <summary>
    /// L'audio lorsqu'une carte myst�re est retourn�e
    /// </summary>
    [SerializeField] private AudioSource flipCard;
    /// <summary>
    /// Le texte qui indique qui a gagn� la partie
    /// </summary>
    [SerializeField] private TextMeshProUGUI winnerText;
    /// <summary>
    /// Le bouton qui termine la partie
    /// </summary>
    [SerializeField] private Button endGameButton;
    /// <summary>
    /// Indique si une action est en train d'�tre r�alis�e
    /// </summary>
    private bool isAction = false;
    /// <summary>
    /// Indique si le compte bancaire du joueur actif est dans le n�gatif
    /// </summary>
    private bool isNegative = false;
    /// <summary>
    /// Le nombre de joueurs restants dans la partie
    /// </summary>
    private int numberOfPlayers = 4;
    /// <summary>
    /// L'index du joueur actif
    /// </summary>
    private int activePlayerIndex = 0;

    private void Awake()
    {
        // Pseudo-singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Cache tous les joueurs
        HidePlayers();
    }

    void Start()
    {
        // Le premier joueur commencer son tour
        activePlayerIndex = 0;
        rollDiceImage.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(false);
        buildScreen.gameObject.SetActive(false);
        autoSellButton.gameObject.SetActive(false);
        escapeJail.gameObject.SetActive(false);
        winnerText.gameObject.SetActive(false);
        endGameButton.gameObject.SetActive(false);
        RefreshMoney();
        players[activePlayerIndex].imageUI.color = UnityEngine.Color.yellow;
        camera.transform.position = new Vector3(-2.5f, 8f, -2.5f);
        camera.transform.eulerAngles = new Vector3(45, 45, 0);
    }

    private void Update()
    {
        // Les diff�rentes utilisations des touches rapides et leur effet
        if (!isAction)
        {
            if (!isNegative)
            {
                if (Input.GetKeyDown(quickRoll) && rollDiceImage.gameObject.activeSelf)
                {
                    StartCortoutineRollDice();
                }
                if (Input.GetKeyDown(quickNext) && endTurnButton.gameObject.activeSelf)
                {
                    ChangePlayer();
                }
            }
            if (Input.GetKeyDown(quickSell) && autoSellButton.gameObject.activeSelf)
            {
                AutoSell();
            }
            if (Input.GetKeyDown(quickBuild))
            {
                StartBuilding();
            }
        }
    }
    /// <summary>
    /// Permet de savoir quel joueur est en train de jouer
    /// </summary>
    /// <returns>Le joueur en train de jouer</returns>
    public Player GetActivePlayer()
    {
        return players[activePlayerIndex];
    }

    /// <summary>
    /// Obtenir le r�sultat de l'addition de la face des d�s qui pointe vers le haut
    /// </summary>
    /// <returns>Le r�sultat des d�s</returns>
    public int GetDiceResult()
    {
        int[] diceResults = new int[2];
        // Calcule la diff�rence d'angle entre le vecteur qui pointe vers le haut et le d�
        // L'angle le plus petit est la face qui pointe vers le haut
        for (int i = 0; i < dices.Length; i++)
        {
            List<Vector3> directions = new() {
                -dices[i].transform.right,
                -dices[i].transform.up,
                dices[i].transform.forward,
                -dices[i].transform.forward,
                dices[i].transform.up,
                dices[i].transform.right
            };
            List<float> angles = new();

            foreach (Vector3 direction in directions)
            {
                angles.Add(Vector3.Angle(direction, Vector3.up));
            }
            float minAngle = angles.Min();
            diceResults[i] = angles.IndexOf(minAngle) + 1;
        }
        return diceResults.Sum(); ;
    }

    /// <summary>
    /// Obtenir la case o� le joueur devrait se diriger apr�s avoir lanc� les d�s
    /// </summary>
    /// <param name="player">Le joueur qui veut se d�placer</param>
    /// <param name="movement">Le nombre de cases que le joueur se d�place</param>
    /// <returns>La case o� le joueur va s'arr�ter</returns>
    public GameTile GetDestinationTile(Player player, int movement)
    {
        int playerTileIndex = Array.FindIndex(tilesList, tile => tile == player.currentTileOn);
        int tileIndexToMoveTo = (playerTileIndex + movement + 40) % 40;
        return tilesList[tileIndexToMoveTo];
    }

    /// <summary>
    /// Permet de savoir qui poss�de la case achetable
    /// </summary>
    /// <param name="ownableTile">La case achetable</param>
    /// <returns>Le joueur qui poss�de cette case, si personne ne la poss�de, retourne null</returns>
    public Player WhoOwnsTile(OwnableTile ownableTile)
    {
        Player owner = null;
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < players[i].properties.Count; j++)
            {
                if (players[i].properties[j] == ownableTile)
                {
                    owner = players[i];
                }
            }
        }
        return owner;
    }

    /// <summary>
    /// Permet de savoir si le joueur en param�tre poss�de l'ensemble de propri�t�s en param�tres
    /// </summary>
    /// <param name="propertyModel">L'ensemble de propri�t�</param>
    /// <param name="player">Le joueur</param>
    /// <returns>Vrai si le joueur poss�de l'ensemble, faux sinon</returns>
    public bool OwnsSet(PropertyModel propertyModel, Player player)
    {
        int numberProperties = NumberPropertiesOwnedInSet(propertyModel, player);
        if (numberProperties == propertyModel.PropertiesInSet)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Permet de savoir le nombre de propri�t�s qu'un joueur poss�de dans un ensemble de propri�t�s
    /// </summary>
    /// <param name="propertyModel">L'ensemble de propri�t�s</param>
    /// <param name="player">Le joueur</param>
    /// <returns>Le nombre de propri�t�s poss�d�es par le joueur</returns>
    public int NumberPropertiesOwnedInSet(PropertyModel propertyModel, Player player)
    {
        int numberProperties = 0;
        for (int i = 0; i < player.properties.Count; i++)
        {
            if (player.properties[i].PropertyModel == propertyModel)
            {
                numberProperties++;
            }
        }
        return numberProperties;
    }

    /// <summary>
    /// Permet d'obtenir les cases achetables dans un ensemble de propri�t�s
    /// </summary>
    /// <param name="propertyModel">L'ensemble de propri�t�s</param>
    /// <returns>La liste des cases achetables dans l'ensemble de propri�t�s en param�tres</returns>
    public List<OwnableTile> GetOwnableTilesInSet(PropertyModel propertyModel)
    {
        List<OwnableTile> ownableTiles = new();
        for (int i = 0; i < tilesList.Length; i++)
        {
            if (tilesList[i] is OwnableTile)
            {
                OwnableTile property = tilesList[i] as OwnableTile;
                if (property.PropertyModel == propertyModel)
                {
                    ownableTiles.Add(property);
                }
            }
        }
        return ownableTiles;
    }

    /// <summary>
    /// Permet d'obtenir la liste de propri�t�s dans un ensemble de propri�t�s
    /// </summary>
    /// <param name="propertyModel">L'ensemble de propri�t�s</param>
    /// <returns>La liste de propri�t�s dans l'ensemble de propri�t�s en param�tre</returns>
    public List<Property> GetPropertiesInSet(PropertyModel propertyModel)
    {
        List<OwnableTile> ownableTiles = GetOwnableTilesInSet(propertyModel);
        List<Property> properties = new();
        foreach (OwnableTile property in ownableTiles) 
        {
            if (property is Property)
            {
                properties.Add(property as Property);
            }
        }
        return properties;
    }

    /// <summary>
    /// D�marre la coroutine pour le lancement des d�s
    /// </summary>
    public void StartCortoutineRollDice()
    {
        isAction = true;
        StartCoroutine(nameof(CoroutineRollDice));
    }

    /// <summary>
    /// La coroutine qui lance les d�s et attends que les deux d�s soient immobiles avant de continuer
    /// </summary>
    private IEnumerator CoroutineRollDice()
    {
        SetRandomDice();
        rollDiceImage.gameObject.SetActive(false);
        yield return new WaitUntil(() => dices[0].velocity == Vector3.zero && dices[1].velocity == Vector3.zero);

        // En prison
        if (players[activePlayerIndex].inJail)
        {
            StartCoroutineEscapeJail();
        } // Pas en prison
        else
        {
            int diceResult = GetDiceResult();
            MovePlayer(GetDestinationTile(players[activePlayerIndex], diceResult));
        }
    }

    /// <summary>
    /// Positionne les deux d�s al�atoirement et les lance contre la paroi invisible
    /// </summary>
    private void SetRandomDice()
    {
        for (int i = 0; i < dices.Length; i++)
        {
            Vector3 dicePosition = new(UnityEngine.Random.Range(3.0f, 4.0f) + (i * 1.5f), 4, UnityEngine.Random.Range(3.0f, 4.0f) + (i * 1.5f));
            Vector3 diceRotation = new(UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(0.0f, 360.0f), UnityEngine.Random.Range(0.0f, 360.0f));
            dices[i].transform.position = dicePosition;
            dices[i].transform.eulerAngles = diceRotation;
            dices[i].GetComponent<Rigidbody>().velocity = new Vector3(-5, -5, 0);
        }
    }

    /// <summary>
    /// Dire � un joueur de se d�placer vers une case
    /// </summary>
    /// <param name="tile">La case</param>
    public void MovePlayer(GameTile tile)
    {
        players[activePlayerIndex].MoveTo(tile);
    }

    /// <summary>
    /// Lorsque l'action du lancement de d�s est termin�
    /// </summary>
    public void ActionDone()
    {
        endTurnButton.gameObject.SetActive(true);
        isAction = false;
        if (players[activePlayerIndex].money <= 0)
        {
            isNegative = true;
            autoSellButton.gameObject.SetActive(true);
            rollDiceImage.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Les �tapes � faire lorsqu'un joueur termine son tour
    /// </summary>
    public void ChangePlayer()
    {
        players[activePlayerIndex].yourTurn = false;
        players[activePlayerIndex].imageUI.color = UnityEngine.Color.white;
        activePlayerIndex = (activePlayerIndex + 1) % numberOfPlayers;
        endTurnButton.gameObject.SetActive(false);
        if (players[activePlayerIndex].inJail)
        {
            escapeJail.gameObject.SetActive(true);
        }
        else
        {
            rollDiceImage.gameObject.SetActive(true);
        }
        RefreshMoney();
        players[activePlayerIndex].imageUI.color = UnityEngine.Color.yellow;
    }

    /// <summary>
    /// Affiche la zone qui permet d'acheter un propri�t�
    /// </summary>
    /// <param name="ownableTile">La case qui est affich�e</param>
    public void ShowBuyProperty(OwnableTile ownableTile)
    {
        showPropertyBuy.gameObject.SetActive(true);
        propertyImage.sprite = ownableTile.PropertyImage;
    }

    /// <summary>
    /// Les actions � faire lorsque le joueur d�cide d'acheter la propri�t�
    /// </summary>
    public void PropertyBought()
    {
        showPropertyBuy.gameObject.SetActive(false);
        players[activePlayerIndex].BuyProperty();
        RefreshMoney();
        ActionDone();
    }

    /// <summary>
    /// Les actions � faire lorsque le joueur d�cide de ne pas acheter la propri�t�
    /// </summary>
    public void PropertyNotBought()
    {
        showPropertyBuy.gameObject.SetActive(false);
        ActionDone();
    }

    /// <summary>
    /// Permet de changer l'argent du joueur actif et faire l'animation
    /// </summary>
    /// <param name="money">La quantit� d'argent gagn�e ou perdue</param>
    public void MoneyChange(int money)
    {
        players[activePlayerIndex].money += money;
        moneyChangeActivePlayer.text = money.ToString() + "$";
        if (money > 0)
        {
            moneyChangeAnimator.SetTrigger("MoneyGain");
        }
        else
        {
            moneyChangeAnimator.SetTrigger("MoneyLoss");
        }
        this.money.Play();
        RefreshMoney();
    }

    /// <summary>
    /// Rafraichi les textes qui indiquent l'argent des joueurs
    /// </summary>
    public void RefreshMoney()
    {
        moneyActivePlayer.text = "Argent: " + players[activePlayerIndex].money.ToString() + "$";
        for (int i = 0; i < players.Count; i++)
        {
            players[i].moneyUI.text = players[i].money.ToString() + "$";
        }
    }

    /// <summary>
    /// Commence la coroutine d'affichage d'une carte myst�re
    /// </summary>
    /// <param name="mysteryCard">La carte myst�re � afficher</param>
    /// <param name="cardType">Le type de carte (Chance ou Caisse Commune)</param>
    public void StartCoroutineMysteryCard(MysteryCard mysteryCard, string cardType)
    {
        flipCard.Play();
        IEnumerator coroutine = CoroutineMysteryCard(mysteryCard, cardType);
        StartCoroutine(coroutine);
    }

    /// <summary>
    /// La coroutine qui affiche une carte myst�re pendant un certain temps 
    /// puis ex�cute l'action inscrite sur la carte
    /// </summary>
    /// <param name="mysteryCard">La carte myst�re</param>
    /// <param name="cardType">Le type de carte (Chance ou Caisse Commune)</param>
    /// <returns></returns>
    private IEnumerator CoroutineMysteryCard(MysteryCard mysteryCard, string cardType)
    {
        // Affiche la carte
        float timeElapsed = 0.0f;
        showMysteryCard.gameObject.SetActive(true);
        mysteryCardText.text = mysteryCard.CardText;
        mysteryCardTitle.text = cardType;
        UpdateSlider(0);

        // Tant que le temps d'affichage n'est pas �coul�
        while (timeElapsed < mysteryCardTime)
        {
            timeElapsed += Time.deltaTime;
            float pourcentageAvancement = timeElapsed / mysteryCardTime * 100;
            UpdateSlider(pourcentageAvancement);
            yield return null;
        }
        // Ex�cute la carte
        showMysteryCard.gameObject.SetActive(false);
        mysteryCard.ExecuteCard();
    }

    /// <summary>
    /// Mets � jour le slider qui indique le temps restant � l'affichage de la carte myst�re
    /// </summary>
    /// <param name="percent">Le pourcentage d'avancement</param>
    private void UpdateSlider(float percent)
    {
        timeRemainingMysteryCard.value = percent;
    }

    /// <summary>
    /// D�marre la coroutine d'aller en prison
    /// </summary>
    public void StartCoroutineGoToJail()
    {
        StartCoroutine(nameof(CoroutineGoToJail));
    }

    /// <summary>
    /// La coroutine lorsqu'un joueur va en prison
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineGoToJail()
    {
        policeSiren.Play();
        yield return new WaitForSeconds(1f);
        // T�l�porte le joueur et d�place la cam�ra
        players[activePlayerIndex].GoToJail(tilesList[10]);
        camera.transform.position = new Vector3(2.25f, 1.65f, 10.8f);
        camera.transform.eulerAngles = new Vector3(30, -90, 0);
        yield return new WaitForSeconds(0.5f);
        // Ex�cute l'animation de prison
        jail.SetActive(true);
        jailAnimator.SetTrigger("GoToJail");
        yield return new WaitForSeconds(4f);
        // Remet la cam�ra comme elle est normalement
        camera.transform.position = new Vector3(-2.5f, 8f, -2.5f);
        camera.transform.eulerAngles = new Vector3(45, 45, 0);
        jail.SetActive(false);
        endTurnButton.gameObject.SetActive(true);
        isAction = false;
        policeSiren.Stop();
    }

    /// <summary>
    /// Indique au controleur de construction de commencer les �tapes pour la construction
    /// </summary>
    public void StartBuilding()
    {
        if (!isAction)
        {
            isAction = true;
            buildScreen.gameObject.SetActive(true);
            build?.Invoke(players[activePlayerIndex]);
        }
    }

    /// <summary>
    /// Les actions � faire lorsque la construction se termine
    /// </summary>
    public void EndBuilding()
    {
        isAction = false;
        buildScreen.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public void AutoSell()
    {
        bool lose = false;
        while (players[activePlayerIndex].money <= 0)
        {
            if (!SellHouse() && !MortgageProperty())
            {
                lose = true;
                players[activePlayerIndex].money = 1;
            }
        }
        if (lose)
        {
            PlayerLost();
        }
        else
        {
            endTurnButton.gameObject.SetActive(true);
        }
        autoSellButton.gameObject.SetActive(false);
        isNegative = false;
    }

    /// <summary>
    /// Vends une maison sur une des propri�t�s du joueur (si possible)
    /// </summary>
    /// <returns>Vrai si une maison a �t� vendu, faux sinon</returns>
    private bool SellHouse()
    {
        int money = 0;
        foreach (OwnableTile asset in players[activePlayerIndex].properties)
        {
            if (asset is Property)
            {
                Property property = asset as Property;
                if (property.numberOfHouses > 0)
                {
                    property.RemoveHouse();
                    money = property.propertySetModel.BuildingCost / 2;
                    break;
                }
            }
        }
        if (money > 0)
        {
            MoneyChange(money);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Hypoth�que une propri�t� du joueur (si possible)
    /// </summary>
    /// <returns>Vrai si une propri�t� a �t� hypoth�qu�, faux sinon</returns>
    private bool MortgageProperty()
    {
        int money = 0;
        foreach (OwnableTile asset in players[activePlayerIndex].properties)
        {
            if (!asset.IsMortgaged)
            {
                money = asset.ChangeMortgage(mortgagedBorder);
                break;
            }
        }
        if (money > 0)
        {
            MoneyChange(money);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Lorsque le joueur appuie sur le bouton pour sortir de prison
    /// </summary>
    public void EscapeJail()
    {
        MoneyChange(-50);
        escapeJail.gameObject.SetActive(false);
        rollDiceImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// Commence la coroutine de la sortie de prison
    /// </summary>
    public void StartCoroutineEscapeJail()
    {
        StartCoroutine(nameof(CoroutineEscapeJail));
    }

    /// <summary>
    /// La coroutine pour l'animation de sortie de prison
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoroutineEscapeJail()
    {
        players[activePlayerIndex].inJail = false;
        // Affiche la prison et ex�cute l'animation
        jail.SetActive(true);
        jailAnimator.SetTrigger("EscapeJail");
        yield return new WaitForSeconds(0.5f);
        // D�place le joueur
        int diceResult = GetDiceResult();
        MovePlayer(GetDestinationTile(players[activePlayerIndex], diceResult));
        yield return new WaitForSeconds(3.0f);
        jail.SetActive(false);
    }

    /// <summary>
    /// Lorsque le script s'initialize, prend le nombre de joueurs enregistr�
    /// </summary>
    private void OnEnable()
    {
        numberOfPlayers = PlayerPrefs.GetInt("numberOfPlayers");
        ShowActivePlayers();
    }

    /// <summary>
    /// Affiche les images selon le nombre de joueurs actifs
    /// </summary>
    private void ShowActivePlayers()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i].gameObject.SetActive(true);
            players[i].imageUI.gameObject.SetActive(true);
            players[i].moneyUI.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Cache toutes les images des joueurs
    /// </summary>
    private void HidePlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.SetActive(false);
            players[i].imageUI.gameObject.SetActive(false);
            players[i].moneyUI.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Les actions � faire si un joueur fait faillite
    /// </summary>
    private void PlayerLost()
    {
        HidePlayers();
        // Ses propri�t�s n'ont plus de propri�taires
        foreach (OwnableTile property in players[activePlayerIndex].properties)
        {
            property.transform.Find("PropertyOwner").gameObject.SetActive(false);
            property.ChangeMortgage(normalBorder);
        }
        players[activePlayerIndex].properties.Clear();
        players[activePlayerIndex].yourTurn = false;
        players.RemoveAt(activePlayerIndex);
        numberOfPlayers--;
        activePlayerIndex = (activePlayerIndex + numberOfPlayers - 1) % numberOfPlayers;
        ChangePlayer();
        ShowActivePlayers();
        // S'il reste juste un joueur
        if (numberOfPlayers == 1)
        {
            StartCoroutineEndGame();
        }
    }
    /// <summary>
    /// Commencer la coroutine de fin de partie
    /// </summary>
    public void StartCoroutineEndGame()
    {
        StartCoroutine(nameof(CoroutineEndGame));
    }

    /// <summary>
    /// La coroutine de fin de partie
    /// Affiche des feux d'artifices
    /// </summary>
    private IEnumerator CoroutineEndGame()
    {
        isAction = true;
        rollDiceImage.gameObject.SetActive(false);
        endTurnButton.gameObject.SetActive(false);
        autoSellButton.gameObject.SetActive(false);
        escapeJail.gameObject.SetActive(false);
        buildButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
        endGameButton.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);
        winnerText.text = players[0].frenchName + " a gagn�!!";
        camera.transform.position = new Vector3(-12.5f, 10.0f, -12.5f);
        camera.transform.eulerAngles = new Vector3(0, 45, 0);
        yield return new WaitForSeconds(0.5f);
        // Tant que le bouton de fin n'a pas �t� appuy�, affiche les feux d'artifices
        while (true)
        {
            for (int i = 0; i < fireworks.Length; i++)
            {
                float time = UnityEngine.Random.Range(0.0f, 1.0f);
                fireworks[i].Play();
                yield return new WaitForSeconds(time);
            }
            yield return null;
        }
    }

    /// <summary>
    /// Lorsque la partie est termin�e, 
    /// le joueur peut appuyer sur un bouton pour revenir au menu
    /// </summary>
    public void EndGame()
    {
        SceneManager.LoadScene("HomePage");
    }

    /// <summary>
    /// D�marre le didacticiel
    /// </summary>
    public void StartTurorial()
    {
        if (!isAction)
        {
            tutorialController.StartTutorial();
            isAction = true;
        }
    }

    /// <summary>
    /// Termine le didacticiel
    /// </summary>
    public void EndTutorial()
    {
        isAction = false;
    }
}
