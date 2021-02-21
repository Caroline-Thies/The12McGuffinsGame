using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Text titleText;
    public Text questionText;
    public Animator animator;
    public void promptQuestion(string title, string question){
        animator.SetBool("IsOpen", true);
        titleText.text = title;
        questionText.text = question;
    }

    public void answerQuestion(bool answer){
        animator.SetBool("IsOpen", false);
    }
}
