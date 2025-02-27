using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// S'occupe de gérer le changement de scene
/// </summary>
public class SceneController: MonoBehaviour
{
    /// <summary>
    /// Changer de scène
    /// </summary>
    /// <param name="newScene">La nouvelle scène à afficher</param>
    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
