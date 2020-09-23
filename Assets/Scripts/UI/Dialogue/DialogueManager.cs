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
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string inputSentence in dialogue.sentences){
            sentences.Enqueue(inputSentence);
        }
        DisplayNextSenctence();
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
    }
}
