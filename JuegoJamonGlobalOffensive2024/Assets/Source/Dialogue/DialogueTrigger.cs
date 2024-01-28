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
        FindObjectOfType<DialogueManager>().StartDialogue(dialogues[_currentDialogue]);
        _currentDialogue++;
    }
}
