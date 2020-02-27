using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGenerator : MonoBehaviour
{

	public GameObject hookPointObj;

	public int hookGap;

	private GameObject playerObj;
	private Vector3 startPos;

	public float verticalVar;
    // Start is called before the first frame update
    void Start()
    {
		startPos = transform.position;

		playerObj = GameObject.FindGameObjectWithTag("Player");

		GenerateHooks(50);
    }

    // Update is called once per frame
    void Update()
    {
		float dist = Vector2.Distance(playerObj.transform.position, transform.position);
		if (dist < 50)
		{
			GenerateHooks(50);
		}
    }

	void GenerateHooks(int hookAmount)
	{
		for (int i = 0; i < hookAmount; i++)
		{

			Vector3 nextHookPos = new Vector3(transform.position.x + hookGap, transform.position.y + Random.Range(-verticalVar, verticalVar), transform.position.z);
			Instantiate(hookPointObj, nextHookPos, transform.rotation);
			transform.position = new Vector3 (nextHookPos.x, transform.position.y, transform.position.z);

		}
	}

}
