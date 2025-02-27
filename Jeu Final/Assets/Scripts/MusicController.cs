using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de controler la musique
/// Faire en sorte que la musique ne coupe pas entre les scènes
/// </summary>
public class MusicController : MonoBehaviour
{
    /// <summary>
    /// L'instance unique de musique
    /// </summary>
    public static MusicController Instance;

    /// <summary>
    /// Ne pas détruire le controleur lorsqu'on change de scène
    /// </summary>
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
