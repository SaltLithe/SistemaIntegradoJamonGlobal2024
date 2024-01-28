using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private static int _currentDialogue;
    public List<Dialogue> dialogues;

    private void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        if (_currentDialogue > dialogues.Count)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogues[0]);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogues[_currentDialogue]);
            _currentDialogue++;
        }
    }
}
