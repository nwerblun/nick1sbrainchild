using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Inside");
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (Input.GetButton("Interact") && c.tag == "Player")
        {
            PlayerController pc = c.gameObject.GetComponent<PlayerController>();
            pc.currWeapon = PlayerController.weapons.Shotgun;
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
