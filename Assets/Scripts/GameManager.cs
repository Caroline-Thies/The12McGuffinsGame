using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
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
	InventoryManager invManager = FindObjectOfType<InventoryManager>();

	invManager.Hide();

	// I have no clue about C# GC, so I'm just gonna assume that it's gonna
	// clean this up properly and we don't have to do it manually.
	namePrompt.onSubmit.AddListener(name => {
		playerController.username = name;
		Debug.Log("The player has changed their name to '" + playerController.username + "'");

		invManager.GiveItem("wayback-machine");
		invManager.Show();
	});
}
}

