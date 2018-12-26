﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrawer : MonoBehaviour
{
    private LineRenderer renderer;
    private float distance = 100;
    int layerMask;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<LineRenderer>();
        renderer.startWidth = 0.033f;
        renderer.endWidth = 0.033f;
        renderer.positionCount = 2;
        layerMask = ~(1 << LayerMask.NameToLayer("PlayerLayer"));
    }

// Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //renderer.SetPosition(0, transform.position);
        //if (Physics.Raycast(ray, out hit, distance))
        //{
        //    Debug.Log("Collide");
        //    renderer.SetPosition(1, hit.point + hit.normal);
        //}
        //else
        //{
        //    Debug.Log("True");
        //    Debug.Log(ray.GetPoint(distance));
        //    renderer.SetPosition(1, ray.GetPoint(distance));
        //}

        Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(transform.position, currMousePos);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currMousePos, Mathf.Infinity, layerMask);
        renderer.SetPosition(0, transform.position);
        if (hit.collider != null)
        {
            renderer.SetPosition(1, hit.point);
        } else
        {
            renderer.SetPosition(1, ray.GetPoint(distance));
        }
        Debug.Log(hit.point);

        //Vector2 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Ray2D ray = new Ray2D(transform.position, currMousePos);
        //RaycastHit2D hit = Physics2D.Raycast(ray.origin, currMousePos, Mathf.Infinity, layerMask);
        //renderer.SetPosition(0, transform.position);
        //if (hit.collider != null)
        //    renderer.SetPosition(1, hit.point);
        //else
        //    renderer.SetPosition(1, ray.GetPoint(distance));

    }
}
