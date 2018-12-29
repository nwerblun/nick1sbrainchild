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

    }
}

//Idea
/*Make this class have a sprite, a type and an amount. Type will be an enum with options like
 * "Health"
 * "RifleAmmo"
 * "ShotgunAmmo"
 * The amount will only matter for some options. In the player class when you collide with a pickup item
 * You can then get the pickup item's type + amount and do whatever you need to in player. Like if type == health then playerHP += item.amount
 */