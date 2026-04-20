using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Triggers a dialogue interaction to start
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    // Dialogue to be triggered
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}