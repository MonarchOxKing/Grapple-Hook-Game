using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject targetObject;

	private Vector3 followPos;

	public float yMaxLock;
	public float yMinLock;
    // Start is called before the first frame update
    void Start()
    {
		targetObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

		if (targetObject != null)
		{

			if (targetObject.transform.position.y < yMinLock || targetObject.transform.position.y > yMaxLock)
			{
				//followPos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);
				followPos = new Vector3(targetObject.transform.position.x, transform.position.y, transform.position.z);

				transform.position = followPos;

			}
			else
			{
				followPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, transform.position.z);
				transform.position = followPos;
			}

		}
		else if (FindObjectOfType<PlayerMaster>())
		{
			targetObject = FindObjectOfType<PlayerMaster>().gameObject;
		}
	}
}
