using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

	private GameObject playerObj;

	public GameObject playerSpawnedObj;
	private GameObject spawnPoint;
	private CameraController cameraCon;
    // Start is called before the first frame update
    void Start()
    {
		playerObj = FindObjectOfType<PlayerMaster>().gameObject;
		cameraCon = FindObjectOfType<CameraController>();
		spawnPoint = GameObject.Find("EGO SpawnPoint");

	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void PlayerDeath()
	{
		StartCoroutine(DelayedPlayerDeath(2f));

	}

	IEnumerator DelayedPlayerDeath(float delay)
	{
		yield return new WaitForSeconds(delay);
		GameObject spawnedPlayer = Instantiate(playerSpawnedObj, spawnPoint.transform.position, transform.rotation);
		cameraCon.targetObject = spawnedPlayer;

	}


}
