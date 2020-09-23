using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 4f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    private static bool playerExists = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!playerExists){
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else {
             Destroy(gameObject);
         }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
