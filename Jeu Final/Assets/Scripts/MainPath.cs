using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// S'occupe de g�rer le chemin principal
/// </summary>
public class MainPath : Movement
{
    /// <summary>
    /// Envoye un message � tous les joueurs pour dire que quelqu'un est arriv� sur la prochaine case
    /// </summary>
    /// <param name="other">Le joueur qui est entr� dans la zone du trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        OnDestinationReached?.Invoke(transform.parent.gameObject.GetComponent<GameTile>());
    }
}
