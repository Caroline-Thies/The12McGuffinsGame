using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public bool isPaused;
   
 public List<Item> items = new List<Item>();
 public List<int> itemNumbers = new List<int>();
  public GameObject[] slots;
  
  
   
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
	DisplayItems();
}

private void DisplayItems()
{
	for(int i = 0; i < items.Count; i++)
	{
		slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
		slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;
		
		slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 0.5f);
		slots[i].transform.GetChild(1).GetComponent<Text>().text = itemNumbers[i].ToString();
		
		slots[i].transform.GetChild(2).gameObject.SetActive(true);
		Debug.Log(items[i].itemName);
	}
}
}

