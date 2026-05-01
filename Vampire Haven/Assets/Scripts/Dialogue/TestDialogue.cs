using UnityEngine;
using UnityEngine.InputSystem;
//testing sample npc dialogue
public class TestDialogue : MonoBehaviour
{
    public DialogueTrigger trigger;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            trigger.TriggerDialogue();
        }
    }
}