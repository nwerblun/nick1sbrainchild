using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    private LaserDrawer laser;

    public void Awake()
    {
        laser = gameObject.GetComponent<LaserDrawer>();
    }

    public void DrawWeapon()
    {
        laser.enabled = true;
        Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        laser.Draw(transform.position, currMousePos);
    }
}
