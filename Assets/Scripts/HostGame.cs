using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {
	[SerializeField]
	private uint numPlayers = 5;

	private string roomName;

	private NetworkManager networkManager;


	// Use this for initialization
	void Start () {
	
		networkManager = NetworkManager.singleton;

		if (networkManager.matchMaker == null) {
			networkManager.StartMatchMaker ();
		
		}
	}

	public void SetRoomName(string name){

		roomName = name;
		Debug.Log (roomName);
	}

	public void CreateRoom(){

		if (roomName != null && roomName != "") {
			
			networkManager.matchMaker.CreateMatch(roomName,numPlayers,true,"","","",0,0,networkManager.OnMatchCreate);
			Debug.Log ("good");
		}
	}
	

}
