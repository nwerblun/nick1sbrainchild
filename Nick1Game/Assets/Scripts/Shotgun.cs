using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public GameObject bulletPrefab;

    public Shotgun ()
    {
        bulletSpeed = 5f;
    }

    public override void Fire(Vector3 dir, Quaternion rot)
    {
        GameObject barrel = transform.GetChild(0).gameObject; 
        GameObject projectile = Instantiate(bulletPrefab, barrel.transform.position, rot);
        projectile.transform.Rotate(0, 0, -90);
        projectile.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        projectile.GetComponent<Rigidbody2D>().AddForce(dir * 100);
    }

    public override void Reload()
    {
        int x = 0;
        x++;
    }
}
