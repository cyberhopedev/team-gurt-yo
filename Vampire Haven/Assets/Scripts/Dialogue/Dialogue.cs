using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Hosts all the information that the game needs for a single dialogue
/// </summary>
[System.Serializable]
public class Dialogue
{
    // Name of the NPC/Enemy/etc. that the dialogue comes from
    public string name;
    // Sentences that will load into the queue managed by DialogueManager
    [TextArea(3, 10)]
    public string[] sentences;
}