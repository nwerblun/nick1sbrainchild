using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;                         //Used to control player movement
    public float maxSpeed;                      //Absolute max player movement

    private ItemManager items;
    private Rigidbody2D rb2d;                   //Player's rigid body needed to add velocity
    private Animator animator;                  //Used to store a reference to the Player's animator component

    private void Awake()
    {
        items = gameObject.GetComponent<ItemManager>(); ;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //----------ANIMATION CONTROL TRIGGERS----------//
        //Determine based on input direction which way we're moving to set animation triggers.
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
        //----------ANIMATION CONTROL TRIGGERS----------//


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = movement * speed;
        rb2d.velocity = new Vector2(Mathf.Clamp(movement.x, -maxSpeed, maxSpeed), Mathf.Clamp(movement.y, -maxSpeed, maxSpeed));
        items.RenderEquippedWeapon();
    }
}
