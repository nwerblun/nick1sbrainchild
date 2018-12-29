using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject primaryWeapon;            //Will be a weapon prefab
    public float speed;                         //Used to control player movement
    public float maxSpeed;                      //Absolute max player movement

    private Rigidbody2D rb2d;                   //Player's rigid body needed to add velocity
    private float lerpSpeed = 100f;             //Used to control the update frequency of the linear interpolation of movement
    private LaserDrawer laser;                  //Reference to the laser drawing class to enable/disable it
    private Animator animator;                  //Used to store a reference to the Player's animator component
    private bool primaryCreated = false;        //Used to determine if the primary weapon has been instantiated
    private enum weaponTypes                    //Enum to avoid having to use strings or integers
    {
        None,
        Primary,
        Secondary,
        Melee
    };
    private weaponTypes equippedWeaponType;     //Which type of weapon is currently equiped

    private void Start()
    {
        laser = GetComponent<LaserDrawer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        equippedWeaponType = weaponTypes.Primary;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //----------ANIMATION CONTROL TRIGGERS----------//
        //Determine based on input direction which way we're moving to set animation triggers.
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            animator.SetTrigger("StopInput");
        }
        
        if (moveHorizontal > 0)
        {
            animator.SetTrigger("RightWalk");
        } else if (moveHorizontal < 0) {
            animator.SetTrigger("LeftWalk");
        }

        if (moveVertical > 0)
        {
            animator.SetTrigger("ForwardWalk");
        }
        else if (moveVertical < 0) {
            animator.SetTrigger("BackWalk");
        }
        //----------ANIMATION CONTROL TRIGGERS----------//


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = movement * speed;
        rb2d.velocity = new Vector2(Mathf.Clamp(movement.x, -maxSpeed, maxSpeed), Mathf.Clamp(movement.y, -maxSpeed, maxSpeed));

        //Final step, draw the weapon
        CheckWeaponInstantiation();
        RenderWeapon();
    }


    void CheckWeaponInstantiation()
    {
        if (!primaryCreated && primaryWeapon != null)
        {
            primaryWeapon = Instantiate(primaryWeapon, transform.position, Quaternion.identity);
            primaryCreated = true;
        }
        //TODO: Add other cases here
    }

    void RenderWeapon()
    {
        GameObject wep = null;
        //Check if player doesn't have a weapon equipped, if so, do nothing and make sure all drawing is off.
        if (equippedWeaponType == weaponTypes.None)
        {
            laser.enabled = false;
            primaryWeapon.SetActive(false);
            //TODO: Add secondary + maybe melee here when done.
            return;
        } else if (equippedWeaponType == weaponTypes.Primary && primaryCreated) {
            laser.enabled = true;
            primaryWeapon.SetActive(true);
            //secondayWeapon.SetActive(false);
            //meleeWeapon.SetActive(false);
            wep = primaryWeapon;
        }
        else if (equippedWeaponType == weaponTypes.Secondary) { //Needs && secondaycreated like above
            laser.enabled = true;
            //SetActives for everything here
        } else { //Will always have melee I think, no need to check
            //Melee case?
            laser.enabled = false;
            wep = primaryWeapon;
        }

        //-------------------------------------------------------------------------//
        //By doing this I am assuming barrel is always the first. We can change it to get child by name or whatever later on.
        GameObject wepBarrel = wep.transform.GetChild(0).gameObject;

        /* Player/projectiles are moved by adding a velocity so this doesn't apply. Whenever you move an object by directly changing its position, 
            * you have to account for the fact that update/fixed update don't happen in perfect intervals and will mess up with non-constant frame rate.
            * The laser didn't accurately track the shotgun's movement when I just updated coordinates exactly. The fix is to linearly interpolate the position by using
            * the time difference between previous frames (Time.deltaTime) and update the current position to the desired position via interpolation. A possible other option that
            * didn't work for me is to use void LateUpdate() which happens after Update() and is for when things changing during update can cause other things to be affected.
        */
        //Get the space offset from the gun to its barrel
        Vector2 wepToBarrelOffset = wepBarrel.transform.position - wep.transform.position;
        //Linearly interpolate the weapon to the player position
        Vector2 lerpedWepPos = Vector3.Lerp(wep.transform.position, transform.position, Time.deltaTime * lerpSpeed);
        wep.transform.position = lerpedWepPos;
        //Choose the laser start point to be where the weapon will be placed -> barrel of weapon
        laser.laserStartPos = Vector3.Lerp(laser.laserStartPos, lerpedWepPos + wepToBarrelOffset, Time.deltaTime * lerpSpeed);


        //----------Draw Laser-----------//
        //Need to take vector difference to account for the fact that the player moving shifts the origin
        
        Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = currMousePos - (lerpedWepPos + wepToBarrelOffset);
        diff = CustomUtils.ClampMagnitude(diff, Mathf.Infinity, 1f);
        //Compute rotation angle from x-axis of the mouse relative to the gun barrel
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        wep.transform.rotation = Quaternion.Euler(0, 0, angle); //Quaternion.Lerp(wep.transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 10 * lerpSpeed);
        Debug.Log("Angle: " + angle.ToString());
        Debug.Log("X: " + diff.x.ToString() + "\nY: " + diff.y.ToString());
        Debug.Log("Mag: " + diff.magnitude.ToString());
        
    }
}
