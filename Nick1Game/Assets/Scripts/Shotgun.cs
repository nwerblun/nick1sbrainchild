﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public GameObject bulletPrefab;             //A prefab for the bullet that will be ejected

    public Shotgun ()
    {
        bulletSpeed = 5f;
    }

    public override void Fire(Vector3 dir, Quaternion rot)
    {
        //Children are always in order so barrel is always 0. Should set up an enum for this.
        GameObject barrel = transform.GetChild(0).gameObject; 
        //rot is a rotation passed in externally controlled by mouse movement.
        GameObject projectile = Instantiate(bulletPrefab, barrel.transform.position, rot);
        //Bullet comes out verical, need it horizontal in addition to the chosen rotation.
        projectile.transform.Rotate(0, 0, -90);
        //Dir is a direction vector passed in externally and controlled by the mouse.
        projectile.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        //Alternate option is to add force
        //projectile.GetComponent<Rigidbody2D>().AddForce(dir * someForceValue);
    }

    public override void Reload()
    {
        //Not used, garbage implementation to get rid of compiler warnings.
        int x = 0;
        x++;
    }
}
