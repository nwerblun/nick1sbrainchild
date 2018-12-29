using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrawer : MonoBehaviour
{
    public GameObject laserImpactLight;
    private LineRenderer renderer;
    private float distance = 100;
    public Vector2 laserStartPos;
    public bool enabled;
    int layerMask;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        laserImpactLight = Instantiate(laserImpactLight, new Vector3(0, 0, 0), Quaternion.identity);
        laserStartPos = (Vector2)transform.position;
        renderer = GetComponent<LineRenderer>();
        renderer.startWidth = 0.033f;
        renderer.endWidth = 0.033f;
        renderer.positionCount = 2;
        layerMask = ~((1 << LayerMask.NameToLayer("PlayerLayer")) + (1 << LayerMask.NameToLayer("ProjectileLayer")));

        laserImpactLight.SetActive(false);
    }

    void LateUpdate()
    {
        if (!enabled)
        {
            renderer.enabled = false;
            laserImpactLight.SetActive(false);
            return;
        } else {
            renderer.enabled = true;
            laserImpactLight.SetActive(true);
        }

        Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos2D = (Vector2)transform.position;
        Ray2D ray = new Ray2D(transform.position, currMousePos - playerPos2D);
        RaycastHit2D hit = Physics2D.Raycast(playerPos2D, currMousePos - playerPos2D, Mathf.Infinity, layerMask);
        renderer.SetPosition(0, laserStartPos);
        if (hit.collider != null)
        {
            renderer.SetPosition(1, hit.point);
            laserImpactLight.transform.position = hit.point;
            laserImpactLight.SetActive(true);
        }
        else
        {
            renderer.SetPosition(1, ray.GetPoint(distance));
            laserImpactLight.SetActive(false);
        }

    }
}
