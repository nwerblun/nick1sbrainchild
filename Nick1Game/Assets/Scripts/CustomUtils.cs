using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUtils
{
//    public static Vector2 ClampMagnitude(Vector2 v, float max, float min)
//    {
//        double sm = v.sqrMagnitude;
//        if (sm > (double)max * (double)max) return v.normalized * max;
//        else if (sm < (double)min * (double)min) return v.normalized * min;
//        return v;
//    }
//}


////temp storage
//void CheckWeaponInstantiation()
//{
//    if (!primaryCreated && primaryWeapon != null)
//    {
//        primaryWeapon = Instantiate(primaryWeapon, transform.position, Quaternion.identity);
//        primaryCreated = true;
//    }
//    //TODO: Add other cases here
//}
///*
//   void RenderLaser()
//   {
//       //----------Draw Laser-----------//
//       //Need to take vector difference to account for the fact that the player moving shifts the origin
//       //Choose the laser start point to be where the weapon will be placed -> barrel of weapon
//       laser.laserStartPos = Vector2.Lerp(laser.laserStartPos, lerpedWepPos + wepToBarrelOffset, Time.deltaTime * lerpSpeed);
//       Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//       Vector2 diff = currMousePos - (Vector2)wepBarrel.transform.position;
//       //Vector2 diff = currMousePos - (lerpedWepPos + wepToBarrelOffset);

//       diff = CustomUtils.ClampMagnitude(diff, Mathf.Infinity, 1f);
//       //Compute rotation angle from x-axis of the mouse relative to the gun barrel
//       float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
//       wep.transform.rotation = Quaternion.Euler(0, 0, angle); //Quaternion.Lerp(wep.transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 10 * lerpSpeed);
//       Debug.Log("Angle: " + angle.ToString());
//       Debug.Log("X: " + diff.x.ToString() + "\nY: " + diff.y.ToString());
//       Debug.Log("Mag: " + diff.magnitude.ToString());

//   }
//   */

//void RenderWeapon()
//{
//    GameObject wep = null;
//    //Check if player doesn't have a weapon equipped, if so, do nothing and make sure all drawing is off.
//    if (equippedWeaponType == weaponTypes.None)
//    {
//        laser.enabled = false;
//        primaryWeapon.SetActive(false);
//        //TODO: Add secondary + maybe melee here when done.
//        return;
//    }
//    else if (equippedWeaponType == weaponTypes.Primary && primaryCreated)
//    {
//        laser.enabled = true;
//        primaryWeapon.SetActive(true);
//        //secondayWeapon.SetActive(false);
//        //meleeWeapon.SetActive(false);
//        wep = primaryWeapon;
//    }
//    else if (equippedWeaponType == weaponTypes.Secondary)
//    { //Needs && secondaycreated like above
//        laser.enabled = true;
//        //SetActives for everything here
//    }
//    else if (equippedWeaponType == weaponTypes.Melee)
//    { //Will always have melee I think, no need to check
//      //Melee case?
//        laser.enabled = false;
//    }
//    else
//    {
//        laser.enabled = false;
//        return;
//    }

//    //-------------------------------------------------------------------------//
//    //By doing this I am assuming barrel is always the first. We can change it to get child by name or whatever later on.
//    GameObject wepBarrel = wep.transform.GetChild(0).gameObject;

//    /* Player/projectiles are moved by adding a velocity so this doesn't apply. Whenever you move an object by directly changing its position, 
//        * you have to account for the fact that update/fixed update don't happen in perfect intervals and will mess up with non-constant frame rate.
//        * The laser didn't accurately track the shotgun's movement when I just updated coordinates exactly. The fix is to linearly interpolate the position by using
//        * the time difference between previous frames (Time.deltaTime) and update the current position to the desired position via interpolation. A possible other option that
//        * didn't work for me is to use void LateUpdate() which happens after Update() and is for when things changing during update can cause other things to be affected.
//    */

//    //Get the space offset from the gun to its barrel
//    Vector2 wepToBarrelOffset = ((Vector2)wepBarrel.transform.position) - ((Vector2)wep.transform.position);
//    //Linearly interpolate the weapon to the player position
//    Vector2 lerpedWepPos = Vector2.Lerp(wep.transform.position, transform.position, Time.deltaTime * lerpSpeed);
//    wep.transform.position = lerpedWepPos;
}