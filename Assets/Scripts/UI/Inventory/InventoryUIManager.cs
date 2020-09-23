using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryMenu;
	
	private void Start()
	{
		inventoryMenu.gameObject.SetActive(false);
	}
	
	private void Update()
	{
		InventoryControl();
	}
	
	private void InventoryControl()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//if Game paused, press esc, resume game
			if(GameManager.instance.isPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
			//if game resumed, press esc, pause the game
		}
	}
	
	private void Resume()
	{
		inventoryMenu.gameObject.SetActive(false);
		Time.timeScale = 1.0f;
		GameManager.instance.isPaused=false;
	}
	private void Pause()
	{
		inventoryMenu.gameObject.SetActive(true);
		Time.timeScale = 0.0f;
		GameManager.instance.isPaused=true;
	}
}
