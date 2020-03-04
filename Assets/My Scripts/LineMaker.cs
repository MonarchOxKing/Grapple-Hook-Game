using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
	public Transform position1; //Player
	public Transform position2; //hookpoint
	public LineRenderer lineR; //the line render
	public Color lineColour; //line colour 

	public Material myMaterial; //Texture of line 
	//"Unlit/Texture"
	//For a tiling texture, do this:
	//Right click, Create > Material. Give name. At top, it should say Shader then a drop down. Click the drop down, select "Unlit" the "Texture". The drag the sprite into the box to apply tiling image

	//"Sprite/Deafault"
	//For a flat colour, do this:
	//Right click, Create > Material. Give name. At top, it should say Shader then a drop down. Click the drop down, select "Sprite" the "Default

	public string hookPointTag; //Tag of hookpoints 

	public int tileAmount;

	private GameObject lastHookPoint;


	// Start is called before the first frame update
	void Start()
	{
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


		if (Input.GetKey(KeyCode.Space))
		{
			lineR.enabled = true;

			GameObject[] hookPointsObj = GameObject.FindGameObjectsWithTag(hookPointTag);
			Transform[] hookPointTrans = new Transform[hookPointsObj.Length];

			for (int i = 0; i < hookPointsObj.Length; i++)
			{
				hookPointTrans[i] = hookPointsObj[i].transform;
			}



			position2 = GetClosestPoint(hookPointTrans);
			
			lineR.SetPosition(0, position1.position);
			lineR.SetPosition(1, position2.position);

			float dist = Vector3.Distance(position1.position, position2.position);
			lineR.material.mainTextureScale = new Vector2 (dist * 1, 1);
		}


		if (Input.GetKeyUp(KeyCode.Space))
		{
			lineR.enabled = false;
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
	}
}
