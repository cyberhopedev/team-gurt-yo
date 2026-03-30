using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

// State of the battle
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, FLED }

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
        state = BattleState.START;
        enemy.Awake();
        player.Awake();
        StartBattle();
    }

        void StartBattle()
        {
            Debug.Log("Battle started!");

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

        void PlayerTurn()
        {
            Debug.Log("Player Turn");
        }

        public void Attack()
        {
            if (state != BattleState.PLAYERTURN) return;

            int damage = player.attackPower;
            enemy.TakeDamage(damage);

            Debug.Log("Player attacks for " + damage);

            if (enemy.IsDead())
            {
                state = BattleState.WON;
                EndBattle();
                return;
            }

            state = BattleState.ENEMYTURN;
            EnemyTurn();
        }

    public void Escape()
    {
        if (state != BattleState.PLAYERTURN) return;

        Debug.Log("Player fled!");
        state = BattleState.FLED;

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
            state = BattleState.LOST;
            EndBattle();
            return;
        }

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("You won!");
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("You lost...");
        }

        SceneManager.LoadScene(overworldScene);
    }
}
