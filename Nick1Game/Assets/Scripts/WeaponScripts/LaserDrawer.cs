using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrawer : MonoBehaviour
{
    public GameObject laserImpactLight;         //A point light used to give a faint glow when hitting a surface
    private LineRenderer renderer;              //The line renderer used to draw the laser
    private float distance = 100;               //How far to render the laser
    public Vector2 laserStartPos;               //Where to start the laser
    public bool enabled;                        //Whether or not to draw the laser + point light source
    int layerMask;                              //Which layers to check laser collision on
    RaycastHit hit;                             //Used for determining if laser collides with a body

    public void Awake()
    {
        enabled = false;
        //This had to be its own class because you can't enable draw halo through code for some reason. Had to make a prefab point light source.
        laserImpactLight = Instantiate(laserImpactLight, new Vector3(0, 0, 0), Quaternion.identity);
        laserImpactLight.SetActive(false);

        laserStartPos = (Vector2)transform.position;

        renderer = GetComponent<LineRenderer>();
        renderer.startWidth = 0.033f;
        renderer.endWidth = 0.033f;
        renderer.positionCount = 2;
        renderer.enabled = false;
        /* Layermasks tell the collision detection enging what layers to check for collision on. There are 32 layers in total
         * and this is stored as a single 32-bit integer. By flipping making bit 0 = 1 then you can check collision on layer 0, etc.
         * We don't want collision on playerlayer or on projectilelayer for the laser. So we want 1111...0...0..1111 where the two 0s are only on
         * those layers and the rest are 1s. The code below bit shifts a 1 into both those locations, then inverts the number so we have 0s on only those layers.
        */
        layerMask = ~((1 << LayerMask.NameToLayer("PlayerLayer")) + (1 << LayerMask.NameToLayer("ProjectileLayer")));
    }

    public void Draw(Vector2 start, Vector2 end)
    {
        //External setting which can disable drawing the line + point source.
        if (!enabled)
        {
            renderer.enabled = false;
            laserImpactLight.SetActive(false);
            return;
        } else {
            renderer.enabled = true;
            laserImpactLight.SetActive(true);
        }

        //Cast a ray from the start position in the direction of end
        Vector2 diff = end - start;

        Ray2D ray = new Ray2D(transform.position, diff);
        RaycastHit2D hit = Physics2D.Raycast(laserStartPos, diff, Mathf.Infinity, layerMask);
        //Set the first of two points of the line renderer's draw points.
        renderer.SetPosition(0, laserStartPos);
        //Check if the ray hits anything.
        if (hit.collider != null)
        {
            //If it hits, then set the end draw point of the line to where it collided
            renderer.SetPosition(1, hit.point);
            //Set the point light to the same collision point and turn it on
            laserImpactLight.transform.position = hit.point;
            laserImpactLight.SetActive(true);
        }
        else
        {
            //No hit. Take the ray that was cast out, go along it by distance and get that point.
            renderer.SetPosition(1, ray.GetPoint(distance));
            laserImpactLight.SetActive(false);
        }

    }
}
