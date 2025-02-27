using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// S'occupe de g�rer le changement de scene
/// </summary>
public class SceneController: MonoBehaviour
{
    /// <summary>
    /// Changer de sc�ne
    /// </summary>
    /// <param name="newScene">La nouvelle sc�ne � afficher</param>
    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
