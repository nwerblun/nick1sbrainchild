using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Sprite itemSprite;
    public GameObject obj;

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = itemSprite;
    }

    void OnTriggerStay2D(Collider2D c)
    {
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
