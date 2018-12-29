using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    public GameObject primaryWeapon;
    public float speed;
    public float maxSpeed;

    private float lerpSpeed = 100f;
    private LaserDrawer laser;
    private Animator animator;                  //Used to store a reference to the Player's animator component.
    private bool primaryCreated = false;
    private enum weaponTypes
    {
        None,
        Primary,
        Secondary,
        Melee
    };
    private weaponTypes equippedWeaponType;

    private void Start()
    {
        laser = GetComponent<LaserDrawer>();
        equippedWeaponType = weaponTypes.Primary;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

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

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        movement = movement * speed;
        rb2d.velocity = new Vector2(Mathf.Clamp(movement.x, -maxSpeed, maxSpeed), Mathf.Clamp(movement.y, -maxSpeed, maxSpeed));

        if (!primaryCreated && primaryWeapon != null)
        {
            primaryWeapon = Instantiate(primaryWeapon, transform.position, Quaternion.identity);
            bool activate = (equippedWeaponType == weaponTypes.Primary) ? true : false;
            primaryWeapon.SetActive(activate);
            primaryCreated = true;
        }

        RenderWeapon();
    }

    void LateUpdate()
    {

    }

    void RenderWeapon()
    {
        if (equippedWeaponType == weaponTypes.None)
        {
            laser.enabled = false;
            return;
        }

        if(equippedWeaponType == weaponTypes.Primary && primaryCreated)
        {
            laser.enabled = true;
            //Gets the barrel subobject from the primary and then gets its transform->position
            Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currPlayerPos = transform.position;
            Vector2 diff = currMousePos - currPlayerPos;

            //
            float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            primaryWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);

            /* Player/projectiles are moved by adding a velocity so this doesn't apply. Whenever you move an object by directly changing its position, 
             * you have to account for the fact that update/fixed update don't happen in perfect intervals and will mess up with non-constant frame rate.
             * The laser didn't accurately track the shotgun's movement when I just updated coordinates exactly. The fix is to linearly interpolate the position by using
             * the time difference between previous frames (Time.deltaTime) and update the current position to the desired position via interpolation. 
             */
            primaryWeapon.transform.position = Vector3.Lerp(primaryWeapon.transform.position, transform.position, Time.deltaTime * lerpSpeed);
            laser.laserStartPos = Vector3.Lerp(laser.laserStartPos, primaryWeapon.transform.GetChild(0).transform.position, Time.deltaTime * lerpSpeed);
            return;
        }


    }
}
