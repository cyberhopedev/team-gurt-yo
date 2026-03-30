using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour
{
    // Attack button(s), one for now
    public Button attack1;
    // Escape/flee/run button
    public Button escape;

    // Fighter stats
    public TextMeshProUGUI PlayerSpeed;
    public TextMeshProUGUI EnemySpeed;
    [SerializeField] private TextMeshProUGUI tooSlowMessage; 

    // Health bar for player
    public Slider playerHealthBar;
    // Health bar for enemy
    public Slider enemyHealthBar;

    private void Start()
    {
        
    }

    private void ShowMenu()
    {
        
    }

    private void HideMenu()
    {
        
    }

    private void OnBattleStateChanged(BattleState state)
    {
        
    }

    private void OnAttackChosen(int choice)
    {
        
    }

    public void Escape()
    {
        
    }
}