using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRope : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer line;

    public Rigidbody2D origin;
    public float line_width = .1f;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        if (!line)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }
        line.startWidth = line_width;
        line.endWidth = line_width;

    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }
}
