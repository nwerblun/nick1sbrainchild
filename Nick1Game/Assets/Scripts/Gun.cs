using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed;
    public float accuracy;
    public int bulletsPerShot;
    public float reloadSpeed;
    public int clipSize;
    public float damage;
    public float knockBack;

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
