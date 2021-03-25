using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public DialogueManager dialogueManager;
   public InputManager inputManager;
   public InventoryManager inventoryManager;
   public UIManager uiManager;
   private bool gameManagerExists = false;

   public bool isPaused;
   
   public void Awake()
   {
	   
	   if(instance==null)
	   {
		   instance=this;
	   }
	   else
	   {
		   if(instance != this)
		   {
			   Destroy(gameObject);
		   }
		   DontDestroyOnLoad(gameObject);
	   }
	}
	   
private void Start()
{
	UserPrompt namePrompt = FindObjectOfType<InputManager>().prompt("Username");
	PlayerController playerController = FindObjectOfType<PlayerController>();

	if(!gameManagerExists){
		this.gameManagerExists = true;
		DontDestroyOnLoad(transform.gameObject);
	} else {
		Destroy(gameObject);
	}
	namePrompt.onSubmit.AddListener(name => {
		playerController.username = name;
		Debug.Log("The player has changed their name to '" + playerController.username + "'");
	});
}
}

