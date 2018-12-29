using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Sprite itemSprite;           //Sprite to be shown on the floor
    public GameObject obj;              //What object the item on the floor represents

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = itemSprite;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        //TODO: Add text at the bottom of the screen telling you to "Press [interact] to pick up"
        if (Input.GetButton("Interact") && c.tag == "Player")
        {
            PlayerController pc = c.gameObject.GetComponent<PlayerController>();
            if (obj.tag == "Weapon")
            {
                pc.primaryWeapon = obj;
            }
            Destroy(gameObject);
        }
    }
}
