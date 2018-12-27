using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

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
            //Destroy(gameObject);
            // commented destroy so that Shoot can be called
            // Weapon should not be destroyed?
            // Lets have an Item class that is picked up instead of having weapons being picked up.
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("FIRE!");
    }
}
