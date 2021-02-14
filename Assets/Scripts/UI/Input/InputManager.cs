using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private static bool inputManagerExists = false;

    public InputField inputField;
    public Button submitButton;

    private UserPrompt activePrompt = null;

    void Start() {
        if (!inputManagerExists) {
            inputManagerExists = true;
            DontDestroyOnLoad(transform.gameObject);
            Setup();
        } else {
            Destroy(gameObject);
        }
    }

    void Setup() {
        submitButton.onClick.AddListener(() => {
            if (activePrompt != null) {
                string inputText = inputField.text;
                
                inputField.text = "";
                inputField.placeholder.GetComponent<Text>().text = "";

                activePrompt.onSubmit.Invoke(inputText);
                activePrompt = null;
            }
        });
    }

    void Update() {
        if (activePrompt != null && Input.GetKeyDown(KeyCode.Return)) {
            submitButton.onClick.Invoke();
        }
    }

    public UserPrompt prompt(string placeholder) {
        if (activePrompt != null) {
            return null;
        }

        activePrompt = new UserPrompt();

        inputField.placeholder.GetComponent<Text>().text = placeholder;

        return activePrompt;
    }

    public UserPrompt getActivePrompt() {
        return activePrompt;
    }

}