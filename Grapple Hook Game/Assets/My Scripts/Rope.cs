using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform RopeHit;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position);
        Debug.DrawLine(transform.position, hit.point);
        RopeHit.position = hit.point;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, RopeHit.position);
       // if(Input.GetKey(KeyCode.Space))
        //{
        //    lineRenderer.enabled = true;
        //}
       // else
        //{
       //     lineRenderer.enabled = false;
       // }
    }
}
