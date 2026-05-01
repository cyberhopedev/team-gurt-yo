using UnityEngine;
using UnityEngine.SceneManagement;
using static GameConstants.Scenes;

/// <summary>
/// Class select screen shown right after New Game. Sets the chosen class
/// on PlayerData, then loads the intro/overworld scene.
/// </summary>
public class ClassSelectUI : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private string nextScene = MAIN_SCENE;
    private void Choose(PlayerClass pClass)
    {
        // Get the class from the player data
        playerData.playerClass = pClass;

        // Starting stats for each class
        switch(pClass)
        {
            case PlayerClass.Tank:    
                playerData.maxHP = 130; 
                playerData.attackDamage = 7;  
                playerData.armor = 5; 
                playerData.speedStat = 3; 
                break;
            case PlayerClass.Rogue:   
                playerData.maxHP = 80;  
                playerData.attackDamage = 14; 
                playerData.armor = 0; 
                playerData.speedStat = 8; 
                break;
            case PlayerClass.Archer:  
                playerData.maxHP = 90;  
                playerData.attackDamage = 11; 
                playerData.armor = 0; 
                playerData.speedStat = 7; 
                break;
            case PlayerClass.Fighter: 
                playerData.maxHP = 100; 
                playerData.attackDamage = 10; 
                playerData.armor = 2; 
                playerData.speedStat = 5; 
                break;
        }
        playerData.currentHP = playerData.maxHP;
        SceneManager.LoadScene(nextScene);
    }

    public void PickTank()
    {
        Choose(PlayerClass.Tank);
    }
    public void PickRogue()
    {
        Choose(PlayerClass.Rogue);
    }
    public void PickArcher()
    {
        Choose(PlayerClass.Archer);
    }

    public void PickFighter()
    {
        Choose(PlayerClass.Fighter);
    }
}