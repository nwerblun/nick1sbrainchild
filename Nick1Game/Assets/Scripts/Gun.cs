using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public float bulletSpeed;               //How fast bullets will exit the gun on fire
    public float accuracy;                  //A number that will control how bullets are spread around the fire direction
    public int bulletsPerShot;              //Per fire button, how many bullets should exit the gun 
    public float reloadSpeed;               //Time in seconds for how long reloading should take
    public int clipSize;                    //How many bullets can be fired before reloading
    public float damage;                    //How much damage a single shot does
    public float knockBack;                 //How many units backwards a unit should be displaced when hit

    //dir controls where the projectile goes off to, rot is how much to rotate the sprite by.
    public abstract void Fire(Vector3 dir, Quaternion rot);
    public abstract void Reload();

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currGunPos = transform.position;
            Quaternion rotation = transform.rotation;
            Fire(currMousePos - currGunPos, rotation);
        }
    }


}
