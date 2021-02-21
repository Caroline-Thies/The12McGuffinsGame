using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private static bool inputManagerExists = false;

    public Canvas canvas;
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
        canvas.enabled = false;

        submitButton.onClick.AddListener(() => {
            if (activePrompt != null) {
                string inputText = inputField.text;
                
                canvas.enabled = false;
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

    public Vector2 GetMovement() {
        if (activePrompt != null) {
            return new Vector2(0, 0);
        }

        return new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    public bool GetKeyDown(UnityEngine.KeyCode name) {
        if (activePrompt == null) {
            return Input.GetKeyDown(name);
        } else {
            return false;
        }
    }

    public UserPrompt prompt(string placeholder) {
        if (activePrompt != null) {
            return null;
        }

        canvas.enabled = true;

        activePrompt = new UserPrompt();

        inputField.placeholder.GetComponent<Text>().text = placeholder;

        return activePrompt;
    }

    public UserPrompt getActivePrompt() {
        return activePrompt;
    }

}