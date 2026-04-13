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
    public const string overworldScene = "AlphaScene";
    // Current state of the battle
    public BattleState currentState;
    // References to be initialized in Unity Editor
    [Header("References")]
    public PlayerBattler player;
    public Enemy enemy;

    private void Start()
    {
        currentState = BattleState.START;
        StartBattle();
    }

    void StartBattle()
    {
        Debug.Log("Battle started!");

        currentState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        Debug.Log("Player Turn");
    }

    public void Attack()
    {
        if (currentState != BattleState.PLAYERTURN) return;

        int damage = player.data.attackDamage;;
        enemy.TakeDamage(damage);

        Debug.Log("Player attacks for " + damage);

        if (enemy.IsDead())
        {
            currentState = BattleState.WON;
            EndBattle();
            return;
        }

        currentState = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    public void Escape()
    {
        if (currentState != BattleState.PLAYERTURN) return;

        Debug.Log("Player escaped!");
        currentState = BattleState.ESCAPED;

        SceneManager.LoadScene(overworldScene);
    }

    void EnemyTurn()
    {
        Debug.Log("Enemy Turn");

        int damage = enemy.attackPower;
        player.TakeDamage(damage);

        Debug.Log("Enemy attacks for " + damage);

        if (player.IsDead())
        {
            currentState = BattleState.LOST;
            EndBattle();
            return;
        }

        currentState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void EndBattle()
    {
        if (currentState == BattleState.WON)
        {
            Debug.Log("You won!");
        }
        else if (currentState == BattleState.LOST)
        {
            Debug.Log("You lost...");
        }

        SceneManager.LoadScene(overworldScene);
    }
}
