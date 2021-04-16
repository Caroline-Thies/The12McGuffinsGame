using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public DialogueManager dialogueManager;
   public InventoryManager inventoryManager;
   public UIManager uiManager;
   public PlayerController player;
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
		if(!gameManagerExists){
			this.gameManagerExists = true;
			DontDestroyOnLoad(transform.gameObject);
		} else {
			Destroy(gameObject);
		}
		
		
	}

	public InputManager getInputManager(){
		return uiManager.inputManager;
	}
}

