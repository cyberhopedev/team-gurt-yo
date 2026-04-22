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

    /// <summary>
    /// Starts the battle and recognizes it as a start state
    /// </summary>
    private void Start()
    {
        currentState = BattleState.START;
        StartBattle();
    }

    /// <summary>
    /// Starts the battle with the player attacking first
    /// </summary>
    void StartBattle()
    {
        Debug.Log("Battle started!");

        currentState = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    /// <summary>
    /// The Player's turn
    /// </summary>
    void PlayerTurn()
    {
        Debug.Log("Player Turn");
    }

    /// <summary>
    /// Attacks the target with the amount of damage stored in the
    /// PlayerData, updating the states/turns as needed
    /// </summary>
    public void Attack()
    {
        // If it is not the player's turn, then don't let them attack
        if(currentState != BattleState.PLAYERTURN)
        {
            return;
        }
        // BETA VERSION OF ATTACKING
        int damage = player.data.attackDamage;;
        enemy.TakeDamage(damage);
        Debug.Log("Player attacks for " + damage);

        // If the enemy is dead, ensure that it is considered dead and record it in a HashSet
        if(enemy.IsDead())
        {
            EnemyTracker.defeatedEnemies.Add(enemy.enemyID);
            currentState = BattleState.WON;
            EndBattle();
            return;
        }
        // Otherwise, the Enemy is not dead, so make it the Enemy's turn
        currentState = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    /// <summary>
    /// Allows the enemy to escape if it is their turn, returning to the level
    /// </summary>
    public void Escape()
    {
        // If it is not the player's turn, then don't let them escape
        if(currentState != BattleState.PLAYERTURN)
        {
            return;
        }

        Debug.Log("Player escaped!");
        currentState = BattleState.ESCAPED;
        SceneManager.LoadScene(overworldScene);
    }

    /// <summary>
    /// Handles the enemy turn by utilizing the attack power stored
    /// </summary>
    void EnemyTurn()
    {

        Debug.Log("Enemy Turn");
        // Call enemy chooseAttack method to pick randomAttack and return dmg
        int damage = enemy.chooseAttack();
        player.TakeDamage(damage);

        //Call all possible enemy status effects, (buffs to give player back stats after turn with debuff is over)
        player.giveArmor(enemy.armorAddition());
        player.loseArmor(enemy.armorReduction());
        player.gainGold(enemy.goldGive());
        player.loseGold(enemy.goldSteal());
        player.giveDamage(enemy.damageBuff());
        player.loseDamage(enemy.damageDebuff());

        //Not really sure if flee mechanics will work here. subject to change
        if(enemy.attemptFlee())
        {
            int randChance = UnityEngine.Random.Range(0, 4);
            if (randChance == 0)
            {
                currentState = BattleState.ESCAPED;
                SceneManager.LoadScene(overworldScene);
            }
        }

        if (enemy.goldDamage())
        {
            player.TakeDamage(player.gold);
        }

        //def more to come

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

    /// <summary>
    /// Ends the battle and returns to the overworld scene
    /// </summary>
    void EndBattle()
    {
        if (currentState == BattleState.WON)
        {
            Debug.Log("You won!");
            // TODO - Add feedback/impacts for winning
        }
        else if (currentState == BattleState.LOST)
        {
            Debug.Log("You lost...");
            // TODO - Add feedback/impacts for losing
        }

        SceneManager.LoadScene(overworldScene);
    }
}
