﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRope : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer line;

    public Material mat;
    public Rigidbody2D origin;
    public float line_width = .1f;
    public float speed = 75;
    public float pull_force = 50;

    private Vector3 velocity;

    private bool update = false;
    private bool pull = false;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        if (!line)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }
        line.startWidth = line_width;
        line.endWidth = line_width;
        line.material = mat;

    }

    public void setStart(Vector2 targetPos)
    {
        Vector2 dir = targetPos - origin.position;
        dir = dir.normalized;
        velocity = dir * speed;
        transform.position = origin.position + dir;
        pull = false;
        update = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!update)
        {
            return;
        }

        if (pull)
        {
            Vector2 dir = (Vector2)transform.position - origin.position;
            origin.AddForce(dir * pull_force);
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, origin.position);
            if (distance > 50)
            {
                update = false;
                line.SetPosition(0, Vector2.zero);
                line.SetPosition(1, Vector2.zero);
                return;
            }
        }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        velocity = Vector2.zero;
        pull = true;
    }
}
