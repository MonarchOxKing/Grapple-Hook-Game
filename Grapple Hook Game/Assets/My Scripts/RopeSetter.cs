using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSetter : MonoBehaviour
{
    // Start is called before the first frame update

    public SpiderRope rope;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rope.setStart(worldPos);
        }
    }
}
