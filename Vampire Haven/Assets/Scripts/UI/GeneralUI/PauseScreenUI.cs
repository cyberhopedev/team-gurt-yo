using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Pause/inventory menu opened with Esc (or Y, which jumps directly
/// to the player tab). Owns the root panel, tab switching is delegated to
/// the tabController component on the same GameObject.
/// </summary>
public class PauseScreenUI : MonoBehaviour
{
    public static PauseScreenUI Instance;

    [Tooltip("The Menu GameObject that contains Tabs + Pages. Toggled on/off.")]
    [SerializeField] private GameObject menuRoot;

    [Tooltip("Drag the Tabs child here (the GameObject with tabController).")]
    [SerializeField] private tabController tabs;
    private bool _isOpen;

    private void Awake()
    {
        Instance = this;
        if(menuRoot != null)
        {
            menuRoot.SetActive(false);    
        }
        
    }

    private void Update()
    {
        // Pause menu doesn't belong in the battle scene — battle has its own UI.
        if (SceneManager.GetActiveScene().name == GameConstants.Scenes.BATTLE_SCENE)
        {
            return;
        }

        // Esc toggles the whole menu. Closing the menu = "resume".
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            Toggle();
        }

        // Y is a shortcut: open the menu directly to the player/inventory tab.
        if (Keyboard.current != null && Keyboard.current.yKey.wasPressedThisFrame)
        {
            OpenToInventoryTab();
        }
    }

    /// <summary>
    /// Opens and closes the menu, pauses time while open
    /// </summary>
    public void Toggle()
    {
        _isOpen = !_isOpen;
        menuRoot.SetActive(_isOpen);

        Time.timeScale = _isOpen ? 0f : 1f;

        // Default to the inventory tab whenever the menu opens
        if(_isOpen) {
            tabs.ActivateTab(0);
        }
    }

    /// <summary>
    /// Y-key shortcut: open the menu (if closed) and force the inventory tab.
    /// Called from Update() when the player presses Y.
    /// </summary>
    public void OpenToInventoryTab()
    {
        if (!_isOpen)
        {
            Toggle();
        }
        tabs.ActivateTab(0);
    }
}