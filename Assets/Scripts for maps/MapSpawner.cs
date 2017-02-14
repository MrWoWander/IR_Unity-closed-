using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MapSpawner : NetworkBehaviour {
	public GameObject ChinaPrefab;
	public GameObject USAPrefab;
	public GameObject RussiaPrefab;
	GameObject MapPrefab;
    GameObject[] allmaps;
    GameObject[] allpoints;
    public Transform spawnpoint;

	void Start(){

		GameObject.Find("Canvasmap").GetComponent<SpawnHandler> ().spawner = this;

	}

	[Command]
	public void CmdMapSpawn(string prefab) {
			if ((GameObject.FindGameObjectsWithTag ("Map").Length > 0)) {
				allmaps = GameObject.FindGameObjectsWithTag ("Map");
				for (int i = 0; i <= GameObject.FindGameObjectsWithTag ("Map").Length - 1; i++)
					Destroy (allmaps [i]);
			}
			if ((GameObject.FindGameObjectsWithTag ("point").Length != 0)) {
				allpoints = GameObject.FindGameObjectsWithTag ("point");
				for (int j = 0; j <= GameObject.FindGameObjectsWithTag ("point").Length - 1; j++)
					Destroy (allpoints [j]);
			}

		switch (prefab) {

		case "USA":
			MapPrefab = USAPrefab;
			break;
		case "Russia":
			MapPrefab = RussiaPrefab;
			break;
		case "China":
			MapPrefab = ChinaPrefab;
			break;
		}

		GameObject spot =  Instantiate (MapPrefab, spawnpoint.transform.position, Quaternion.Euler (90f, 180f, 270f)) as GameObject;
		NetworkServer.SpawnWithClientAuthority(spot, this.gameObject);

	}

		
}
