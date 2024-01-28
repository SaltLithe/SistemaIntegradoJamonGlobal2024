using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static bool loadMainMenu;

    public TMP_Text nameText;
    public TMP_Text dialogueText;


    private Queue<string> sentences;
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Started conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }
        dialogueText.text = sentences.Dequeue();


    }


    void EndDialogue()
    {
        SceneManager.LoadScene(loadMainMenu ? "MainMenu" : "Game");
        Debug.Log("End of conversation");
    }
}
