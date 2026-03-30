using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

/// <summary>
/// Manages the flow of a battle between a player and an enemy in a game scene.
/// </summary>
public class BattleManager : MonoBehaviour
{
    // Public instance of BattleManager that can be called to other classes
    public static BattleManager Instance { get; private set; }
    // Scene where player has movement to explore
    public const string overworldScene = "SampleScene";
    // Current state of the battle
    public BattleState currentState;
    // References to be initialized in Unity Editor
    [Header("References")]
    public PlayerBattler player;
    public Enemy enemy;

    private void Start()
    {
        
    }

    private void StartBattle()
    {
        
    }

    private void PlayerTurn()
    {
        
    }

    public void Attack()
    {
        
    }

    public void Escape()
    {
        
    }

    private void EnemyTurn()
    {
        
    }

    private void EndBattle()
    {
        
    }
}