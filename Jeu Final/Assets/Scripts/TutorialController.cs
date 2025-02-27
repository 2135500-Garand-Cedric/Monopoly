using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// S'occupe de gérer le fonctionnement du didacticiel
/// </summary>
public class TutorialController : MonoBehaviour
{
    /// <summary>
    /// Les pages du didacticiel
    /// </summary>
    [SerializeField] private Image[] pages;
    /// <summary>
    /// La page présentement affichée
    /// </summary>
    private int activePage = 0;

    /// <summary>
    /// Lorsque l'object est chargé, cache toutes les pages du didacticiel
    /// </summary>
    private void Start()
    {
        foreach (Image page in pages) 
        { 
            page.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Commencer le didacticiel
    /// </summary>
    public void StartTutorial()
    {
        pages[0].gameObject.SetActive(true);
    }
    /// <summary>
    /// Afficher la prochaine page du didicaticiel
    /// </summary>
    public void ChangePage()
    {
        pages[activePage].gameObject.SetActive(false);
        if (activePage != pages.Length - 1)
        {
            activePage++;
            pages[activePage].gameObject.SetActive(true);
        }
    }
}
