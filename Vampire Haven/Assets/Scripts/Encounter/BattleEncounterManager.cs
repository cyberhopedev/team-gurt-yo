using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BattleEncounterManager : MonoBehaviour
{
    // Scene where the battle occurs
    public const string battleScene = "BattleEncounterScene";
    // Prevents the encounter from triggering more than once
    private bool _triggered = false;

    /// <summary> 
    /// Based on the given collider, if the player triggers it then switch to PlayerBattler
    /// and start the battle with the encountered overworld enemies
    /// </summary>
    /// <param name="c">The collider the overworld player triggers</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_triggered || !collision.CompareTag("Player"))
        {
            return;
        }

        _triggered = true;
        Debug.Log("Encounter triggered!");
        Debug.Log("Loading battle scene...");
        SceneManager.LoadScene(battleScene);
    }
}