using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        inputManager = FindObjectOfType<InputManager>();
    }

    public void StartDialogue(Dialogue dialogue){
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string inputSentence in dialogue.sentences){
            sentences.Enqueue(inputSentence);
        }
        DisplayNextSenctence();
        inputManager.SetExplicitlyDisabled(true);
    }

    public void DisplayNextSenctence(){
        Debug.Log("Displaying next sentence!");
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        string nextSentence = sentences.Dequeue();
        dialogueText.text = nextSentence;
    }

    private void EndDialogue(){
        animator.SetBool("IsOpen", false);
        inputManager.SetExplicitlyDisabled(false);
    }
}
