using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour
{
    [Header("Vitae")]
    public Slider vitaeBar; 

    [Header("Status Icons")]
    public Transform playerStatusContainer;  // panel for status icon prefabs
    public Transform enemyStatusContainer;
    public GameObject statusIconPrefab;

    [Header("Attacks")]
    [Tooltip("Container that holds dynamically-created attack option buttons.")]
    public GameObject attackListPanel;
    public Transform attackButtonContainer;
    public GameObject attackButtonPrefab;


    [Header("Health Bars")]
    public Slider playerHealthBar;
    // Health bar for enemy
    public Slider enemyHealthBar;

    // Actors needed
    private BattleManager battleManager;
    private PlayerBattler player;
    private Enemy enemy;

    private void Start()
    {
        battleManager = FindAnyObjectByType<BattleManager>();
        // Hide if not in a battle
        if (battleManager == null)
        {
            gameObject.SetActive(false);
            return;
        }

        // Get references
        player = battleManager.player;
        enemy = battleManager.enemy;

        // Setup buttons
        // attackButton.onClick.AddListener(OnAttack);
        // escapeButton.onClick.AddListener(OnEscape);

        // Initialize UI
        UpdateHealthBars();
    }

    private void Update()
    {
        // If we are not in the BattleEncounterScene, don't update
        if (battleManager == null)
        {
            return;
        }

        // Update UI every frame
        UpdateHealthBars();

        // // Disable buttons when it's not player's turn
        // attackButton.interactable = (battleManager.currentState == BattleState.PLAYERTURN);
        // escapeButton.interactable = (battleManager.currentState == BattleState.PLAYERTURN);
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
        // Open the attack list and rebuild it from the player's unlocked abilities
        battleManager.Attack();
        if (attackListPanel == null) { 
            battleManager.Attack(); 
            return; 
        }
         attackListPanel.SetActive(true);
        BuildAttackList();
    }

    public void OnEscape()
    {
        battleManager.Escape();
    }

    private void UpdateHealthBars()
    {
        playerHealthBar.maxValue = player.data.maxHP;
        playerHealthBar.value = player.currentHP;

        enemyHealthBar.maxValue = enemy.maxHP;
        enemyHealthBar.value = enemy.currentHP;
    }

    /// <summary>
    /// Called by BattleManager whenever a status effect is applied or removed.
    /// Rebuilds the icon strip from scratch — simpler than tracking adds/removes,
    /// and these lists are tiny (max ~5 statuses).
    /// </summary>
    public void RefreshStatusIcons()
    {
        // Clear old icons. Always destroy children before re-spawning to avoid
        // leaving stale ones from the previous turn.
        foreach (Transform t in playerStatusContainer)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in enemyStatusContainer){
            Destroy(t.gameObject);
        }

        // Spawn a small icon for each active status
        foreach (StatusEffect s in player.activeStatuses)
        {
            Instantiate(statusIconPrefab, playerStatusContainer).GetComponent<UnityEngine.UI.Image>().sprite = s.icon;
        }
        foreach (StatusEffect s in enemy.activeStatuses)
        {
            Instantiate(statusIconPrefab, enemyStatusContainer).GetComponent<UnityEngine.UI.Image>().sprite = s.icon;
        }
    }

    private void BuildAttackList()
    {
        foreach (Transform t in attackButtonContainer) Destroy(t.gameObject);

        // Always include a basic attack option.
        GameObject basic = Instantiate(attackButtonPrefab, attackButtonContainer);
        basic.GetComponentInChildren<TextMeshProUGUI>().text = "Basic Attack";
        basic.GetComponent<Button>().onClick.AddListener(() =>
        {
            attackListPanel.SetActive(false);
            battleManager.Attack();
        });

        // Then list every unlocked ability the player can afford right now.
        foreach (Ability ab in player.data.unlockedAbilities)
        {
            GameObject btn = Instantiate(attackButtonPrefab, attackButtonContainer);
            btn.GetComponentInChildren<TextMeshProUGUI>().text =
                $"{ab.abilityName}  ({ab.vitaeCost} V)";
            Ability captured = ab;       // closure capture
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                attackListPanel.SetActive(false);
                battleManager.UseAbility(captured);
            });
        }
    }
}   