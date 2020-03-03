using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
	public enum PlayerState
	{
		Swinging,
		Running,
		Flying

	}
	public PlayerState myState;

	public KeyCode hookButton;

	public bool doMovement;

	public float speedMod;
	public int jumpForce;
	public int hookForce;
	public int verticalHookForce;

	private DistanceJoint2D dJoint;
	private Rigidbody2D rb;

	private SpriteRenderer sRenderer;
	public Sprite sprRightSwing;
	public Sprite sprLeftSwing;
	public Sprite sprIdleAir;

	public GameObject deathObj;

	private bool unHook;
	//Line Renderer
	#region
	[Header("Line Rendering")]
	//public Transform position1; //Player
	private Transform position2; //hookpoint
	private LineRenderer lineR; //the line render
	public Color lineColour; //line colour 
	public Material myMaterial; 
	private string hookPointTag; //Tag of hookpoints 
	public int tileAmount;
	private GameObject lastHookPoint;
	#endregion

	// Start is called before the first frame update
	void Start()
    {

		rb = GetComponent<Rigidbody2D>();
		dJoint = GetComponent<DistanceJoint2D>();
		lineR = GetComponent<LineRenderer>();
		hookPointTag = "Grapple Point";
		sRenderer = GetComponent<SpriteRenderer>();
		sRenderer.sprite = sprRightSwing;
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

		switch (myState)
		{
			case PlayerState.Swinging:
				transform.Translate(Vector3.right * speedMod * Time.deltaTime, Space.World);

				break;

			case PlayerState.Running:
				transform.Translate(Vector3.right * speedMod * Time.deltaTime, Space.World);
				break;

			case PlayerState.Flying:
				transform.Translate(Vector3.right * speedMod * Time.deltaTime, Space.World); //<---- this could be affecting smoothness
				break;
		}

		

		if (Input.GetKeyDown(hookButton))
		{

			if (myState == PlayerState.Running)
			{
				Jump();
			}

			if (myState == PlayerState.Flying)
			{
				rb.AddForce(Vector2.up * verticalHookForce);
			}

			myState = PlayerState.Swinging;

			dJoint.enabled = true;
			SelectHook();

			//tangent force
			Vector2 dir = (transform.position - position2.transform.position).normalized;
			dir = Vector2.Perpendicular(dir);
			rb.AddForce(dir * hookForce);

			unHook = false;
			//right force
			//rb.AddForce(Vector2.right * hookForce);
		}

		if (Input.GetKey(hookButton))
		{
			if (!unHook)
			{
				LineRendering();
				SpriteSetter();

			}
		}

		if (Input.GetKeyUp(hookButton))
		{
			UnHook();
		}
	}

	void UnHook()
	{
		lineR.enabled = false;
		dJoint.enabled = false;
		sRenderer.sprite = sprIdleAir;
		//myState = PlayerState.Running;
		myState = PlayerState.Flying;
		unHook = true;
	}

	void SpriteSetter()
	{
		//if (transform.position.x < position2.position.x && sRenderer.sprite != sprRightSwing)
		if (transform.position.x < position2.position.x && sRenderer.sprite != sprRightSwing)
		{
			print("moving right");
			sRenderer.sprite = sprRightSwing;
		}
		else if (transform.position.x >= position2.position.x && sRenderer.sprite != sprLeftSwing)
		{
			print("moving left");
			sRenderer.sprite = sprLeftSwing;

		}
		else
		{
			print("Swing Sprite Error");
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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("GROUND"))
		{
			if (myState == PlayerState.Swinging)
			{
				UnHook();
			}
			
				myState = PlayerState.Running;

			


		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("GROUND"))
		{
			if (myState == PlayerState.Running)
			{
				myState = PlayerState.Flying;
			}

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

		//lineR.SetPosition(0, position1.position);
		lineR.SetPosition(0, transform.position);
		lineR.SetPosition(1, position2.position);

		dJoint.connectedAnchor = position2.position;

		//float dist = Vector3.Distance(position1.position, position2.position);
		float dist = Vector3.Distance(transform.position, position2.position);
		lineR.material.mainTextureScale = new Vector2(dist * 1, 1);
		
	}
	Transform GetClosestPoint(Transform[] points)
	{
		Transform tMin = null;
		float minDist = Mathf.Infinity;
		//Vector3 currentPos = position1.position;
		Vector3 currentPos = transform.position;
		foreach (Transform t in points)
		{
			//if (t.position.x > position1.position.x) //make it not do ones behind it
			if (t.position.x > transform.position.x) //make it not do ones behind it
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

