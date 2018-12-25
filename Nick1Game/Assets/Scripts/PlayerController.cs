using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float speed;
    public float maxSpeed;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
       
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = movement * speed;
        rb2d.velocity = new Vector2(Mathf.Clamp(movement.x, -maxSpeed, maxSpeed), Mathf.Clamp(movement.y, -maxSpeed, maxSpeed));

    }
}
