using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// S'occupe de controler les actions dans la page d'accueil
/// </summary>
public class HomePageController : SceneController
{
    /// <summary>
    /// Le texte qui indique le nombre de joueurs pour la partie
    /// </summary>
    [SerializeField] private TextMeshProUGUI numberOfPlayersText;
    /// <summary>
    /// Le nombre de joueurs dans la partie
    /// </summary>
    private int numberOfPlayers = 4;
    /// <summary>
    /// Changer le nombre de joueurs pour la partie
    /// </summary>
    /// <param name="newNumberOfPlayers">le nouveau nombre de joueurs</param>
    public void ChangeNumberOfPlayers(float newNumberOfPlayers)
    {
        numberOfPlayers = (int)newNumberOfPlayers;
        numberOfPlayersText.text = numberOfPlayers.ToString() + " Joueurs";
    }
    /// <summary>
    /// Lorsque le script se fait fermer, enregistre le nombre de joueurs
    /// </summary>
    void OnDisable()
    {
        PlayerPrefs.SetInt("numberOfPlayers", numberOfPlayers);
    }
}
