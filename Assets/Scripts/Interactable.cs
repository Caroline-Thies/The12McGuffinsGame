using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    void Update(){
        if(isInRange){
            if (Input.GetKeyDown(interactKey)){
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            isInRange = true;
            Debug.Log("Player now in range");

        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            isInRange = false;
            Debug.Log("Player now out of range");

        }
    }
}
