using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
	public KeyCode hookButton;

	public bool doMovement;

	public float speedMod;
	public int jumpForce;
	public int hookForce;

	private DistanceJoint2D dJoint;
	private Rigidbody2D rb;

	public GameObject deathObj;
	//Line Renderer
	#region
	[Header("Line Rendering")]
	public Transform position1; //Player
	public Transform position2; //hookpoint
	public LineRenderer lineR; //the line render
	public Color lineColour; //line colour 
	public Material myMaterial; 
	public string hookPointTag; //Tag of hookpoints 
	public int tileAmount;
	private GameObject lastHookPoint;
	#endregion

	// Start is called before the first frame update
	void Start()
    {

		rb = GetComponent<Rigidbody2D>();
		dJoint = GetComponent<DistanceJoint2D>();


		//Line Rendering
		lineR.material.color = lineColour;
		lineR.material = myMaterial;
		lineR.SetColors(lineColour, lineColour);

		if (tileAmount == 0)
		{
			tileAmount = 1;
		}

	}

    // Update is called once per frame
    void Update()
    {
		//Movement
		if (doMovement) //make false if not touching ground tag or something
		{
			transform.Translate(Vector3.right * speedMod * Time.deltaTime, Space.World);
			
		}

		if (Input.GetKeyDown(hookButton))
		{
			Jump();
			dJoint.enabled = true;
			SelectHook();
			rb.AddForce(Vector2.right * hookForce);
		}





		if (Input.GetKey(KeyCode.Space))
		{
			LineRendering();

		}


		if (Input.GetKeyUp(KeyCode.Space))
		{
			lineR.enabled = false;
			dJoint.enabled = false;
		}

		
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Destroy(gameObject);

			GameObject myDeathObj = Instantiate(deathObj, transform.position, transform.rotation);

			//myDeathObj.GetComponent
		}

		if (other.CompareTag("FallZone"))
		{
			Destroy(gameObject);

			GameObject myDeathObj = Instantiate(deathObj, transform.position, transform.rotation);

		}
	}

	void Jump() //make more for time held
	{
		rb.AddForce(Vector2.up * jumpForce);
	}


	//HOOK BEHAVIOUR
	#region 
	void SelectHook()
	{
		lineR.enabled = true;

		GameObject[] hookPointsObj = GameObject.FindGameObjectsWithTag(hookPointTag);
		Transform[] hookPointTrans = new Transform[hookPointsObj.Length];

		for (int i = 0; i < hookPointsObj.Length; i++)
		{
			hookPointTrans[i] = hookPointsObj[i].transform;
		}



		position2 = GetClosestPoint(hookPointTrans);
	}
	void LineRendering()
	{		

		lineR.SetPosition(0, position1.position);
		lineR.SetPosition(1, position2.position);

		dJoint.connectedAnchor = position2.position;

		float dist = Vector3.Distance(position1.position, position2.position);
		lineR.material.mainTextureScale = new Vector2(dist * 1, 1);
		
	}
	Transform GetClosestPoint(Transform[] points)
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = position1.position;
		foreach (Transform t in points)
		{
			if (t.position.x > position1.position.x) //make it not do ones behind it
			{
				float dist = Vector3.Distance(t.position, currentPos);
				if (dist < minDist)
				{
					tMin = t;
					minDist = dist;
				}
			}
		}
		return tMin;
	}
	#endregion


	void FallDeath()
	{
		Destroy(gameObject);
	}

	void PlayerDeath()
	{
		Destroy(gameObject);

	}

}

