using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Reads PlayerData each time the Inventory tab opens and writes it into
/// the character preview labels. Lives on InventoryPage/CharacterPanel.
/// </summary>
public class CharacterStatsPanelUI : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [Header("Display")]
    [SerializeField] private Image            portraitImage;
    [SerializeField] private TextMeshProUGUI  classText;
    [SerializeField] private TextMeshProUGUI  levelText;
    [SerializeField] private TextMeshProUGUI  hpText;
    [SerializeField] private TextMeshProUGUI  attackText;
    [SerializeField] private TextMeshProUGUI  defenseText;
    [SerializeField] private TextMeshProUGUI  speedText;

    [Header("Class Portraits — same order as PlayerClass enum")]
    [SerializeField] private Sprite[] classPortraits;

    /// <summary>
    /// OnEnable runs every time the GameObject becomes active — i.e. every
    /// time the player switches to the Inventory tab. Refreshing here means
    /// we always show fresh stats without polling each frame.
    /// </summary>
    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (playerData == null) return;

        // (int) cast on an enum gives its underlying integer value — used here
        // as an index into the portrait array. Tank=0, Rogue=1, Archer=2, Fighter=3.
        int classIdx = (int)playerData.playerClass;
        if (portraitImage != null && classPortraits != null && classIdx < classPortraits.Length)
            portraitImage.sprite = classPortraits[classIdx];

        // ToString() on an enum gives the name — "Tank", "Rogue", etc.
        if (classText   != null) classText.text   = $"Class: {playerData.playerClass}";
        if (levelText   != null) levelText.text   = $"Level {playerData.level}  ({playerData.xp} XP)";
        if (hpText      != null) hpText.text      = $"HP: {playerData.currentHP} / {playerData.maxHP}";
        if (attackText  != null) attackText.text  = $"Attack: {playerData.attackDamage}";
        if (defenseText != null) defenseText.text = $"Defense: {playerData.armor}";
        if (speedText   != null) speedText.text   = $"Speed: {playerData.speedStat}";
    }
}