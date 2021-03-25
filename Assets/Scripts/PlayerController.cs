using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 4f;
    public string username = "Alex";
    public string lastSavePoint = "BEDROOM";
    public Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    public GameManager gameManager;    

    private static bool playerExists = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!playerExists){
            playerExists = true;
            gameManager = FindObjectOfType<GameManager>();
            DontDestroyOnLoad(transform.gameObject);
        } else {
             Destroy(gameObject);
         }
    }

    // Update is called once per frame
    void Update()
    {
        InputManager inputManager = gameManager.getInputManager();
        movement = inputManager.GetMovement();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }
}
