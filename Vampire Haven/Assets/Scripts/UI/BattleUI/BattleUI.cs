using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour
{
    // Attack button(s), one for now
    [Header("Buttons")]
    public Button attackButton;
    // Escape/flee/run button
    public Button escapeButton;

    [Header("Health Bars")]
    public Slider playerHealthBar;
    // Health bar for enemy
    public Slider enemyHealthBar;

    // State of the battle as text
    [Header("Text")]
    public TextMeshProUGUI stateText;

    // Actors needed
    private BattleManager battleManager;
    private PlayerBattler player;
    private Enemy enemy;

    private void Start()
    {
        // Get references
        battleManager = FindObjectOfType<BattleManager>();
        player = battleManager.player;
        enemy = battleManager.enemy;

        // Setup buttons
        attackButton.onClick.AddListener(OnAttack);
        fleeButton.onClick.AddListener(OnFlee);

        // Initialize UI
        UpdateHealthBars();
        UpdateStateText();
    }

    private void Update()
    {
        // Update UI every frame (simple for now for alpha)
        UpdateHealthBars();
        UpdateStateText();

        // Disable buttons when it's not player's turn
        attackButton.interactable = (battleManager.state == BattleState.PLAYERTURN);
        escapeButton.interactable = (battleManager.state == BattleState.PLAYERTURN);
    }

    /// <summary>
    /// Hides the BattleUI
    /// </summary>
    private void HideMenu()
    {
        gameObject.SetActive(false);
    }

    private void OnAttack()
    {
        battleManager.Attack();
    }

    public void OnEscape()
    {
        battleManager.Escape();
    }

    private void UpdateHealthBars()
    {
        playerHealthBar.maxValue = player.maxHP;
        playerHealthBar.value = player.currentHP;

        enemyHealthBar.maxValue = enemy.maxHP;
        enemyHealthBar.value = enemy.currentHP;
    }

    private void UpdateStateText()
    {
        switch (battleManager.state)
        {
            case BattleState.PLAYERTURN:
                stateText.text = "Player Turn";
                break;
            case BattleState.ENEMYTURN:
                stateText.text = "Enemy Turn";
                break;
            case BattleState.WON:
                stateText.text = "You Win!";
                break;
            case BattleState.LOST:
                stateText.text = "You Lose!";
                break;
        }
    }   
}   