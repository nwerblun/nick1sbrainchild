using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum weapons
    {
        None,
        Shotgun
    };
    private Rigidbody2D rb2d;
    public weapons currWeapon = weapons.None;
    public float speed;
    public float maxSpeed;
    private Animator animator;                  //Used to store a reference to the Player's animator component.

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
       
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            animator.SetTrigger("StopInput");
        }
        
        if (moveHorizontal > 0)
        {
            animator.SetTrigger("RightWalk");
        } else if (moveHorizontal < 0) {
            animator.SetTrigger("LeftWalk");
        }

        if (moveVertical > 0)
        {
            animator.SetTrigger("ForwardWalk");
        }
        else if (moveVertical < 0) {
            animator.SetTrigger("BackWalk");
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = movement * speed;
        rb2d.velocity = new Vector2(Mathf.Clamp(movement.x, -maxSpeed, maxSpeed), Mathf.Clamp(movement.y, -maxSpeed, maxSpeed));

    }
}
