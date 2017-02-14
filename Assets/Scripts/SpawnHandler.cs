using UnityEngine;



public class SpawnHandler : MonoBehaviour {

	[HideInInspector]
	public MapSpawner spawner;

	public void ServerSpawn(string prefab){

		spawner.GetComponent<MapSpawner> ().CmdMapSpawn (prefab);

	}

}
