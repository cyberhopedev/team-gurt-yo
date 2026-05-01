using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
// 10 minutes into brackeys video
/// <summary>
/// Handles the dialogue for the game
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
   public GameObject dialoguebox; //added this to make dialogue pop up and go away?
    public static DialogueManager Instance {get; private set;}

    // Create a queue which hold the string of sentences within the dialogue
    private Queue<string> sentences;

    /// <summary>
    /// At start, initialize a queue to hold the sentences in the dialogue
    /// </summary>
    void Start()
    {
        sentences = new Queue<string>();
    }

    /// <summary>
    /// Starts the dialogue
    /// </summary>
    /// <param name="dialogue">The specific dialogue to start</param>
    public void StartDialogue(Dialogue dialogue)
    {
        dialoguebox.SetActive(true); 
        nameText.text = dialogue.name; 

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence in the dialogue
    /// </summary>
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        dialoguebox.SetActive(false);
    }
}