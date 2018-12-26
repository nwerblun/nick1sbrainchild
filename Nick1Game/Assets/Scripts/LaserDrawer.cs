using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrawer : MonoBehaviour
{
    private LineRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<LineRenderer>();
        renderer.startWidth = 0.033f;
        renderer.endWidth = 0.033f;
        renderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        renderer.positionCount = 2;
        Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currPlayerPos = transform.position;
        Vector3 clampedMousePos = new Vector3(100*currMousePos.x, 100*currMousePos.y, currMousePos.z);
        renderer.SetPosition(0, currPlayerPos);
        //Layers are a 32 bit bitmask. Layers with 1 are on for raycasting, 0 are off. 000001 would check only layer 0 for raycasts.
        //Layer 9 has the player so we want all layers EXCEPT PlayerLayer
        int layerMask = ~(1 << LayerMask.NameToLayer("PlayerLayer"));
        RaycastHit2D hit = Physics2D.Raycast((Vector2)currPlayerPos, (Vector2)currMousePos,
            Mathf.Infinity, layerMask, -Mathf.Infinity, Mathf.Infinity);
        if (hit.collider != null)
            renderer.SetPosition(1, hit.point);
        else
            renderer.SetPosition(1, currMousePos);
    }
}
